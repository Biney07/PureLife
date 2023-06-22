import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getPacientet() {
    return axios.get(`${API_URL}/api/PacientiAPI/`)
}

export function getPacientVisitedByStaff(staffId) {
    return axios.get(`${API_URL}/api/PacientiAPI/GetPacientVisitedByStaf/${staffId}`)
}
