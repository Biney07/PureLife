import { getAuth, createUserWithEmailAndPassword, updateProfile, signInWithEmailAndPassword, signOut } from "firebase/auth";
import axios from 'axios'
const API_URL = "https://localhost:7292";

export async function register(userData) {
    const auth = getAuth();
    try {
      await createUserWithEmailAndPassword(auth, userData.email, userData.password);
      await updateProfile(auth.currentUser, { displayName: `${userData.firstName} ${userData.lastName}` });
    } catch (error) {
      console.error(error);
    } finally {
        registerUserToDatabase(auth, userData);
    }
}

async function registerUserToDatabase(auth, userData) {
    try {
        await axios.post(`${API_URL}/api/PacientiAPI`, {
            uId: auth.currentUser.uid,
            emri: userData.firstName,
            mbiemri: userData.lastName,
            membershipStatus: false,
            email: userData.email,
            password: userData.password,
            confirmPassword: userData.password
        })
    } catch(err) {
        console.log(err)
    }
}

export async function signin(userData) {
    const auth = getAuth();
    const response = await signInWithEmailAndPassword(auth, userData.email, userData.password)
    return response
}

export async function userSignOut() {
    const auth = getAuth();
    try {
        const response = await signOut(auth)
        console.log(response)
        return response
    } catch(err) {
        console.log(err)
    }
}
  