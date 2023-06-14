import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getTerminet(id) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByStaf/${id}`)
}

export function getTerminetByDate(date) {
    return axios.get(`${API_URL}/api/TerminiAPI/GetTerminiByDate/${date}`)
}
