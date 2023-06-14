import { initializeApp } from 'firebase/app'
import { getAuth } from "firebase/auth"
import { getFirestore } from 'firebase/firestore'

export default defineNuxtPlugin(nuxtApp => {
    const config = useRuntimeConfig()

    const firebaseConfig = {
        apiKey: "AIzaSyCIG2HhBNPxpOnt-ND9tdt8zA7vwZlgej0",
        authDomain: "purelife-patient.firebaseapp.com",
        projectId: "purelife-patient",
        storageBucket: "purelife-patient.appspot.com",
        messagingSenderId: "645065555811",
        appId: "1:645065555811:web:5893cd1f0908f96e749d56"
    };

    const app = initializeApp(firebaseConfig)

    const auth = getAuth(app)
    const firestore = getFirestore(app)

    nuxtApp.vueApp.provide('auth', auth)
    nuxtApp.provide('auth', auth)

    nuxtApp.vueApp.provide('firestore', firestore)
    nuxtApp.provide('firestore', firestore)
})