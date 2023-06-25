export function storeUser(userData){
    localStorage.setItem('patient', userData);
}

export function userExists() {
    if (process.client) {
        return localStorage.getItem('patient') ? true : false;
    } else {
        return false;
    }
}

export function getLocalStorage() {
    var user = JSON.parse(localStorage.getItem('patient'));
    return user;
}

export function getUser() {
    if (userExists()) {
        return getLocalStorage();
    }
}

export function removeUser() {
    if(process.client) {
        if(userExists()) {
            return localStorage.removeItem('patient')
        }
    }
}