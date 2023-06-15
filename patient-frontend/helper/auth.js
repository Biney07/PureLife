export function storeUser(userData){
    localStorage.setItem('patient', userData);
}

export function userExists() {
    if (process.client) {
        // Access localStorage only in the browser
        return localStorage.getItem('patient') ? true : false;
      } else {
        // Handle the server-side case
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
    if(userExists) {
        return localStorage.removeItem('patient')
    }
}