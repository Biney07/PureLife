import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getAllServices() {
    return axios.get(`${API_URL}/api/SherbimetAPI/Index`)
}