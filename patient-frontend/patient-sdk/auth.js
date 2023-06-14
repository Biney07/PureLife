import { getAuth, createUserWithEmailAndPassword, updateProfile } from "firebase/auth";

export async function register(userData) {
    const auth = getAuth();
    try {
      await createUserWithEmailAndPassword(auth, userData.email, userData.password);
      await updateProfile(auth.currentUser, { displayName: `${userData.firstName} ${userData.lastName}` });
      console.log(auth)
    } catch (error) {
      console.error(error);
    }
}
  