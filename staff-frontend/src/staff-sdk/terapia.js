import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function createTerapine(terapia) {
    return axios.post(`${API_URL}/api/TerapiaAPI/`, terapia)
}


export function getTerapiaByStaff(id) {
    return axios.get(`${API_URL}/api/TerapiaAPI/GetTerapiteWrittenByStaff/${id}`)
}

export function editTerapine(id, staffId, terapia) {
    return axios.put(`${API_URL}/api/TerapiaAPI/Edit/${id}/${staffId}`, terapia)
}


export function deleteTerapia(id) {
    return axios.put(`${API_URL}/api/TerapiaAPI/DeleteTerapia/${id}`)
}

