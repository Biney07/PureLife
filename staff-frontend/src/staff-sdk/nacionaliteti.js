import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL_BACKUP

export function getAllNationalities() {
    return axios.get(`${API_URL}/api/Nacionaliteti`)
}
