import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL

export function fetchCurrentUser(userId) {
    return axios.get(`${API_URL}/api/StafiAPI/${userId}`)  
}

export function editUser(userData) {
    return axios.put(`${API_URL}/api/StafiAPI/${userData.id}`, userData)
        
}
