import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function createTerapine(terapia) {
    return axios.post(`${API_URL}/api/TerapiaAPI/`, terapia)
}