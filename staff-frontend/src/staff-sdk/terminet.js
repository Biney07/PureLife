import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getTerminet(id) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByStaf/${id}`)
}

export function createTerminet(id) {
    return axios.post(`${API_URL}/api/TerminiAPI/Create/${id}`)
}

export function getTerminetByDateAndStaff(date, id) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByDateAndId/${date}/${id}`)
}

export function deleteTermini(id) {
    return axios.delete(`${API_URL}/api/TerminiAPI/DeleteTermin/${id}`)
}


