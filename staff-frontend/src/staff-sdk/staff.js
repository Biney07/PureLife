import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getAllStaff() {
    return axios.get(`${API_URL}/api/StafiAPI/getall`)
}