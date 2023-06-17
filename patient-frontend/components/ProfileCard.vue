<template>
    <div v-if="Object.keys(patientData).length === 0" class="text-center">
        <div class="spinner-border text-primary" role="status"
            style="height:100px; width:100px; margin-top: 100px;color:var(--blue) ">

        </div>
        <h3 style="color:var(--blue); margin-top:10px">Loading...</h3>
    </div>

    <div v-else class="megacard">

        <div class="card">
            <div class="cover-photo">
                <img src="../assets/profile.jpg" class="profile">
            </div>
            <h3 class="profile-name">{{ patientData.emri }} {{ patientData.mbiemri }}</h3>
            <div class="pacientinfo">
                <div class="part">
                    <p class="about">MOSHA:
                    <p class="about-bold"> {{ age }}</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about">GJINIA:
                    <p class="about-bold"> {{ patientData.gjinia }}</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about"> ID:
                    <p class="about-bold"> {{ patientData.nrLeternjoftimit }}</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about">DATA E LINDJES:
                    <p class="about-bold">{{ new Date(patientData.dataLindjes).toLocaleDateString() }}</p>

                    </p>
                </div>
                <div class="part">
                    <p class="about">VENDLINDJA:
                    <p class="about-bold">{{ patientData.qyteti }}</p>

                    </p>
                </div>
                <div class="part">
                    <p class="about">EMAIL:
                    <p class="about-bold">{{ patientData.email }}</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about">NR TEL:
                    <p class="about-bold">{{ patientData.nrTel }}</p>
                    </p>
                </div>

            </div>
        </div>
        <div class="rightcard">
            <MemberShipStatus :Membership="patientData.membershipStatus" />
            <div class="pacientinfo-right">
                <h3 class="profile-nameA">ALERGJI</h3>
                <div class="part-right">

                    <p class="about-bold"> {{ patientData.alergji }}
                    </p>


                </div>
                <h3 class="profile-name"> STATUSI I SHENDETIT:</h3>
                <div class="part-right">
                    <HealthStatus :healthPercentage="76" />

                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios';
import HealthStatus from '~/components/HealthStatus.vue';
import MemberShipStatus from '~/components/MemberShipStatus.vue';

export default {
    data() {
        return {
            patientData: {},
            age: null
        };
    },
    mounted() {
        console.log("mounted() called");
        const useri = JSON.parse(localStorage.getItem('patient'));

        console.log(useri);
        if (useri && useri.user.uid) {
            const uid = useri.user.uid;
            console.log("UID:", uid);
            this.fetchPatientData(uid);
        }
    },
    methods: {
        async fetchPatientData(uid) {
            try {
                console.log("fetchPatientData() called with UID:", uid);
                const response = await axios.get(`https://localhost:7292/api/PacientiAPI/GetPacientiByUId/${uid}`);
                this.patientData = response.data;
                console.log("Fetched patient data:", this.patientData);
                this.calculateAge(); // Call the calculateAge() method after fetching patient data
            } catch (error) {
                console.log("Error while fetching patient data:", error);
            }
        },
        calculateAge() {
            if (this.patientData.dataLindjes) {
                const birthDate = new Date(this.patientData.dataLindjes);
                const today = new Date();
                let age = today.getFullYear() - birthDate.getFullYear();
                const monthDiff = today.getMonth() - birthDate.getMonth();

                if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
                    age -= 1;
                }

                this.age = age; // Set the calculated age to the 'age' property
            }
        },
    },
    components: {
        HealthStatus,
        MemberShipStatus
    }
};
</script>




<style scoped>
.gold-div {
    position: absolute;
    top: 20px;
    right: 20px;
    background-color: rgb(254, 220, 23);
    padding: 10px;
    border-radius: 5px;
}

.status-text {
    color: rgb(0, 0, 0);
    font-weight: bold;
}

.megacard {
    display: flex;
    justify-content: center;

}

.card {
    font-family: var(--primary-font);
    padding: 15px 20px;
    /* Reduced padding */
    width: 350px;
    margin-right: 5%;
    /* Reduced width */
    background: #ffffff;
    border-radius: 5px 0px 0px 5px;
    display: flex;
    border: none;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 25px 50px -12px;
    border-radius: 15px;

}

.rightcard {
    position: relative;
    font-family: var(--primary-font);
    padding: 15px;
    /* Reduced padding */
    width: 350px;
    border-radius: 15px;
    /* Reduced width */
    background: #ffffff;

    display: flex;
    justify-content: flex-end;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 25px 50px -12px;

}

.part {
    display: flex;
    align-items: center;
}

.part-right {
    display: flex;
    text-align: right;
    justify-content: flex-end;
}

.pacientinfo {
    display: flex;
    justify-content: left;
    flex-direction: column;
    margin-top: 20px;
    /* Reduced margin */
}

.pacientinfo-right {
    display: flex;
    justify-content: right;
    flex-direction: column;
    margin-top: 20px;
    /* Reduced margin */
    text-align: right;
    justify-content: flex-end;
}

.cover-photo {
    position: relative;
    background: url("../assets/purelife.png");
    background-size: cover;
    height: 80px;
    /* Reduced height */
    border-radius: 5px 5px 0 0;
}

.about-bold {
    font-weight: 500;
    margin: 0px;
    font-size: 16px;
    /* Reduced font size */
    padding-left: 8px;
    /* Reduced padding */
}

.profile {
    position: absolute;
    width: 100px;
    /* Reduced width */
    bottom: -50px;
    /* Adjusted position */
    left: 5px;
    border-radius: 50%;
    border: 2px solid var(--blue);
    background: #222;
    padding: 2px;
}

.profile-name {
    font-size: 24px;
    /* Reduced font size */
    margin: 8px 0 1px 120px;
    /* Adjusted margin */
    color: var(--blue);
}

.profile-nameA {
    font-size: 24px;
    /* Reduced font size */
    margin: 32px 0 1px 120px;
    /* Adjusted margin */
    color: var(--blue);
}

.about {
    line-height: 1.4;
    /* Adjusted line height */
    margin: 0px;
    display: flex;
}
</style>