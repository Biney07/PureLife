import axios from 'axios';

export function login(userData) {
    return axios.post(`${API_URL}/api/StafiAPI/`, {
        emailAddress: userData.email,
        password: userData.password
    })
}
