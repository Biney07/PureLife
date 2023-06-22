import axios from 'axios';

const API_URL = "http://localhost:5000"
// eslint-disable-next-line no-console
console.log(API_URL)

export function sendMessage(chatData) {
    return axios.post(`${API_URL}/api/messages`, {
        senderId: String(chatData.senderId),
        recipientId: chatData.recipientId,
        messageBody: chatData.messageBody,
        senderType: chatData.senderType
    })
}

export function getMessages(chatData) {
    // eslint-disable-next-line no-console
    console.log(chatData)
    return axios.get(`${API_URL}/api/chat/${chatData.senderId}/${chatData.recipientId}`)
}
