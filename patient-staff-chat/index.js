const express = require('express');
const http = require('http');
const socketIO = require('socket.io');
require('dotenv').config();
const axios = require('axios');
const mongoose = require('mongoose');
const cors = require('cors');
const bodyParser = require('body-parser');
const app = express();
const server = http.createServer(app);

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));

app.use(function (req, res, next) {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With, Content-Type, Origin, Authorization');
    res.setHeader('Access-Control-Allow-Credentials', true);

    next();
});


app.use(cors());

// Start the server
const port = 5000; // Replace with your desired port number
server.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});

const io = socketIO(server,{
    cors: {
        origin: ['http://localhost:8080', 'http://localhost:3000'],
        methods: ["GET","PUT", "POST"],
    }
});


const mongoURL = `mongodb+srv://${process.env.DB_USERNAME}:${process.env.DB_PASSWORD}@chat-cluster.adyxral.mongodb.net/`;

// Connect to MongoDB
mongoose.connect(mongoURL, { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => {
    console.log('Connected to MongoDB');

    // API endpoint to fetch chat history for a patient-doctor pair
    app.get('/api/chat/:senderId/:recipientId', async (req, res) => {
        const { senderId, recipientId } = req.params;

        try {
          const chatCollection = mongoose.connection.db.collection('chats');
          const chatHistory = await chatCollection
            .find({
              $or: [
                { senderId, recipientId },
                { senderId: recipientId, recipientId: senderId },
              ],
            })
            .sort({ timestamp: 1 })
            .toArray();
      
          res.json(chatHistory);
        } catch (error) {
          console.error('Failed to fetch chat history:', error);
          res.status(500).json({ error: 'Failed to fetch chat history' });
        }
      });
      
  
  // API endpoint to send a message
    app.post('/api/messages', async (req, res) => {
        try {
            const { senderId, recipientId, messageBody, senderType } = req.body;
        
            // Create a new message object
            const message = {
                senderId,
                recipientId,
                messageBody,
                senderType,
            };
        
            // Save the message to the database
            const chatCollection = mongoose.connection.db.collection('chats');
            await chatCollection.insertOne(message);
        
            // Emit the message to the intended recipient
            io.to(recipientId).emit('receiveMessage');
        
            res.json({ success: true });
        } catch (error) {
            console.error('Failed to send message:', error);
            res.status(500).json({ error: 'Failed to send message' });
        }
    });
  
    // Mapping object to store user socket connections
    const userSocketMapping = {};

    // Socket.IO connection handler
    io.on('connection', (socket) => {
        console.log('User connected:', socket.id);

        // Handle disconnection
        socket.on('disconnect', () => {
            console.log('User disconnected:', socket.id);

            // Remove the user's socket ID from the mapping
            for (const [userId, socketId] of Object.entries(userSocketMapping)) {
                if (socketId === socket.id) {
                    delete userSocketMapping[userId];
                    break;
                }
            }
        });

        // Handle user registration with socket ID
        socket.on('registerUser', (userId) => {
            // Store the user's socket ID in the mapping
            userSocketMapping[userId] = socket.id;
            console.log('User registered:', userId);
        });

    // Handle message sending
    socket.on('sendMessage', async (message) => {
        try {
        // Retrieve the recipient's socket ID using their user ID
        const recipientSocketId = userSocketMapping[message.recipientId];
            console.log(recipientSocketId)
        if (recipientSocketId) {
            // Emit the message to the intended recipient
            io.to(recipientSocketId).emit('receiveMessage', message);
        } else {
            console.log('Recipient not found:', message.recipientId);
        }
        } catch (error) {
        console.error('Failed to send message:', error);
        }
    });
    });

  })
  .catch((error) => {
    console.error('Failed to connect to MongoDB:', error);
  });
