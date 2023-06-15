import axios from 'axios';

const API_URL = "https://localhost:7292";
export function getAllStaff() {
    return axios.get(`${API_URL}/api/StafiAPI/getall`)
}

export function getStaffByLemi(lemiaId) {
    return axios.get(`${API_URL}/api/StafiAPI/GetStafiByLemi/${lemiaId}`)
}