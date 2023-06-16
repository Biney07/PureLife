import axios from 'axios';

const API_URL = "https://localhost:7292";

const responseBody = (response) => response.data;

const requests = {
  get: (url, params) => axios.get(url, { params }).then(responseBody),
  post: (url, body) => axios.post(url, body).then(responseBody),
  put: (url, body) => axios.put(url, body).then(responseBody),
  delete: (url) => axios.delete(url).then(responseBody),
};

const Stafi = {
  getAll: () => requests.get(`${API_URL}/api/StafiAPI`),
};


const agent = {
  Account,
  Stafi
};

export default agent;
// const Account = {
//   login: (values) => requests.post(`${API_URL}/Account/login`, values),
//   register: (values) => requests.post(`${API_URL}/Account/register`, values),
//   currentUser: () => requests.get(`${API_URL}/Account/currentUser`),
//   getAll: () => requests.get(`${API_URL}/Account/getAllUsers`),
//   fetchAddress: () => requests.get(`${API_URL}/account/savedAddress`),
//   getUserById: (id) => requests.get(`${API_URL}/Account/getUserById/${id}`),
//   updateUser: (id, user) => requests.put(`${API_URL}/Account/updateUser/${id}`, user),
//   deleteUser: (id) => requests.delete(`${API_URL}/Account/delUser/${id}`),
// };