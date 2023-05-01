import * as auth from '../../../helpers/auth'

export const storeUser = (state, userData) => {
    state.isAuthenticated = true;
    state.user.data = userData;

    auth.storeUser(JSON.stringify(userData));
}

export const removeUser = (state) => {
    state.isAuthenticated = false;
    state.user.data = null;

    auth.removeUser()
}

export const updateUser = (state, userData) => {
    state.user.data = userData;
    auth.removeUser()
    auth.storeUser(JSON.stringify(userData));
}