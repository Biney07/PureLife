const mongoose = require('mongoose');

const chatSchema = new mongoose.Schema({
  senderId: {
    type: String,
    required: true,
  },
  senderType: {
    type: String,
    required: true,
  },
  recipientId: {
    type: String,
    required: true,
  },
  messageBody: {
    type: String,
    required: true,
  },
  timestamp: {
    required: false,
    type: Date,
    default: Date.now,
  },
});

const Chat = mongoose.model('Chat', chatSchema);

module.exports = Chat;
