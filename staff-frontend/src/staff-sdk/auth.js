import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL_BACKUP

export function login(userData) {
    return axios.post(`${API_URL}/api/StafiAPI/login`, {
        emailAddress: userData.emailAddress,
        password: userData.password
    })
}
