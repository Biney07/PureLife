import { login, me } from '../../../staff-sdk/auth'
import { editUser } from '../../../staff-sdk/user'
import {getUser} from '../../../helpers/auth'

// eslint-disable-next-line no-unused-vars
export const loginUser = ({commit}, userData) => {
    return new Promise((resolve, reject) => {
        login(userData)
        .then((response) => {
            commit('storeUser', response.data)
            resolve(response)
        })
        .catch(error => { reject(error) })
    })
};

export const signOut = ({ commit } ) => {
    commit('removeUser');

    return Promise.resolve();
}

export const editUserProfile = ({commit}, userData) => {
    return new Promise((resolve, reject) => {
        editUser(userData)
        .then((response) => {
            // eslint-disable-next-line no-console
            console.log(response)
            commit('storeUser', response.data)
            resolve(response)
        })
        .catch(error => { reject(error) })
    })
};

export const authenticateUser = () => {
    const token = getUser()
    return new Promise((resolve, reject) => {
        me(token.token)
        .then((response) => {
            // eslint-disable-next-line no-console
            console.log(response);
            resolve(response)
        })
        .catch(error => { reject(error) })
    })
};

