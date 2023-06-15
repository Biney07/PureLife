import axios from 'axios';

const API_URL = "https://localhost:7292";
export function getTerminet(id) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByStaf/${id}`)
}

export function getTerminetByDate(date) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByDate/${date}`)
}

export function rezervoTerminin(terminiId, pacientiId) {
    return axios.put(`${API_URL}/api/TerminiAPI/RezervoTerminin/`, {
        terminiId: terminiId,
        pacientiId: pacientiId
    })
}