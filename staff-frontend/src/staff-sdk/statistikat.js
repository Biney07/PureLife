import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getTotalinEPacienteve(staffId) {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/NumriTotalIPacienteve/${staffId}`)
}

export function getTotalinTermineve(staffId) {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/TotaliTermineveEPerfunduara/${staffId}`)
}


export function getTotalinTermineveRezervuara(staffId) {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/NumriTermineveTeRezervuara/${staffId}`)
}

export function getTotaliTerapive(staffId) {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/TotaliTerapiveTePerfunduara/${staffId}`)
}