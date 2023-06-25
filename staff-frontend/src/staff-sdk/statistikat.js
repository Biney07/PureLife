import axios from 'axios';

const API_URL = process.env.VUE_APP_API_URL
export function getStatistics(staffId) {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/StafiStatistikat/${staffId}`)
}

export function getMonthlyTerminet(staffId) {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/TerminiMuaji/${staffId}`)
}

export function getStatisticsDrejtor() {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/DrejtoriStatistikat`)
}

export function getMonthlyIncome() {
    return axios.get(`${API_URL}/api/StafiDashboardAPI/FitimetPerMuaj`)
}

