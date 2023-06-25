<template>
  <div class="chat-container">
    <div>
      <Contacts @fetchContactMessages="fetchMessages" @changeContactImage="changeContactImage" />
    </div>
    <div v-if="chatData.recipientId" class="message-container">
        <div :class="{ sender: message.senderType === 'patient', recipient: message.senderType === 'doctor' }" class="message" v-for="message in reversedMessages" :key="message.id">
          <img v-if="message.senderType === 'doctor'" class="avatar-left avatar-receiver" :src="recipientImage" alt="Doctor Avatar" />
          {{ message.messageBody }}
          <img v-if="message.senderType === 'patient'" class="avatar-right avatar-sender" :src="currentUserImage" alt="Patient Avatar" />
        </div>
    </div>
    <div v-else class="message-container-warning">
        <h2>Ju lutemi qe ne fillim te klikoni ne stafin e caktuar</h2>    
    </div> 
    <form v-if="chatData.recipientId" @submit.prevent="sendMessage" class="input-container">
      <input v-model="chatData.messageBody" type="text" placeholder="Send message" />
      <button>Send</button>
    </form>
  </div>
</template>

<script>
import { sendMessage, getMessages } from "@/patient-sdk/chat";
import Contacts from "@/components/Contacts.vue";

import {getPacienti} from "@/patient-sdk/auth.js"

export default {
    components: {
        Contacts,
    },
    data() {
        return {
          recipientImage: null,
          currentUserImage: null,
            chatData: {
                senderId: null,
                recipientId: null, // id is the recipient id
                messageBody: null,
                senderType: 'patient',
            },
            messages: [], // Array to store messages
            socket: null,
            user: null,
        }
    },
    computed: {
        reversedMessages() {
            return this.messages.slice().reverse();
        },
    },
    mounted() {
        this.socket = this.$nuxtSocket('http://localhost:5000')
        this.user = JSON.parse(localStorage.getItem('patient'));
        if (this.user?.user?.uid) {
            this.chatData.senderId = this.user.user.uid;
        }
        this.socket.on('connect', () => {
            this.socket.emit('registerUser', this.chatData.senderId)
            // Event listener for receiveMessage event
            this.socket.emit('registerUser', this.chatData.senderId)
            // Event listener for receiveMessage event
            this.socket.on('receiveMessage', (message) => {
                this.fetchMessages(message)
            });
        });

        this.fetchUserImage()
    },
    methods: {
        async sendMessage() {
            try {
                await sendMessage(this.chatData);
                this.chatData.messageBody = null;
                // await this.fetchMessages(this.chatData);
                this.chatData["recipient"] = this.socket.id
                this.socket.emit('sendMessage', this.chatData);
                this.fetchMessages(this.chatData)
            } catch (err) {
                // eslint-disable-next-line no-console
                console.log(err);
            }
            },
        async fetchMessages(selectedContact) {
            if(selectedContact.recipientId === this.user.user.uid) {
                this.chatData.recipientId = selectedContact.senderId
            } else {
                this.chatData.recipientId = selectedContact.recipientId;
            }

            try {
                const response = await getMessages(this.chatData);
                this.messages = response.data;
            } catch (err) {
                // eslint-disable-next-line no-console
                console.log(err);
            }
        },
        changeContactImage(image) {
          this.recipientImage = image
          console.log(image)
        },
        async fetchUserImage(){
          try {
            const response = await getPacienti(this.user.user.uid)
            this.currentUserImage = response.data.pictureUrl
          } catch (err) {
            console.log(err)
          }
        }
    },
};
</script>

<style scoped>
.chat-container {
  width: 70%;
  height: 100%;
  display: flex;
  flex-direction: column;
  margin: 7% auto;
}

.message-container-warning{
  flex-grow: 1;
  overflow-y: auto;
  padding: 10px;
  height: 465px;
  margin-top: 15px;
  width: 100%;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  font-weight: 500;
  font-family: var(--primary-font);
}

.message-container {
    background: #f5f5f5;
  flex-grow: 1;
  overflow-y: auto;
  padding: 10px;
  height: 465px;
  margin-top: 15px;
  width: 100%;
  display: flex;
  flex-direction: column-reverse; 
  border-radius: 10px;
}

.message {
  background-color: #f2f2f2;
  border-radius: 8px;
  padding: 8px;
  margin-bottom: 8px;
  width: fit-content;
  align-self: flex-end;
  position: relative;
  margin-left: 40px;
  margin-right: 40px;
}


.sender {
  align-self: flex-end;
  background-color: rgb(83, 102, 189);
  color: white;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 15px 15px 0 15px;
}

.recipient {
  align-self: flex-start;
  background-color: rgb(79, 153, 79);
  color: white;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 15px 15px 15px 0px;
}

.input-container {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 10px 0;
}

input[type="text"] {
  flex-grow: 1;
  padding: 8px;
  border-radius: 4px;
  border: 1px solid #ccc;
}

button {
  margin-left: 10px;
  padding: 8px 16px;
  border-radius: 4px;
  border: none;
  background-color: #007bff;
  color: white;
  cursor: pointer;
}

.avatar-sender{
  width: 30px;
  height: 30px;
  margin-left: 20px;
  border-radius: 50%;
  position: absolute;
}

.avatar-receiver{
  width: 30px;
  height: 30px;
  right: 100%;
  margin-right: 10px;
  border-radius: 50%;
  position: absolute;
}
</style>