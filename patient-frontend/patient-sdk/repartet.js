import axios from 'axios';

const API_URL = "https://localhost:7292";
export function getSpecializimet() {
    return axios.get(`${API_URL}/api/LemiaAPI`)
}

export function getTerminetByDate(date) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByDate/${date}`)
}
