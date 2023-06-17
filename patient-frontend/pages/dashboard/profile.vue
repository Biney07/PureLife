<template>
    <div class="dashboard">
        <div class="profile-container">

            <div class="carta">
                <button class="button-35" @click="openForm" role="button">Ndrysho Profilin</button>
                <div v-if="isFormOpen" class="form-overlay">
                    <div class="form-container">
                        <div style="display: flex; justify-content: flex-end;">
                            <img id="svgButton" src="../../assets/closebutton.svg" alt="SVG Button" @click="closeForm"
                                style="color:var(--blue); width: 30px;">

                        </div>
                        <div>
                            <form @submit="updatePatientData">
                                <div class="form-row">
                                    <div class="form-column">
                                        <div class="mb-3">
                                            <label for="emri" class="form-label">Emri:</label>
                                            <input type="text" id="emri" name="emri" v-model="patientData.emri"
                                                class="form-control">
                                        </div>

                                        <div class="mb-3">
                                            <label for="mbiemri" class="form-label">Mbiemri:</label>
                                            <input type="text" id="mbiemri" name="mbiemri" v-model="patientData.mbiemri"
                                                class="form-control">
                                        </div>

                                        <div class="mb-3">
                                            <label for="email" class="form-label">Email:</label>
                                            <input type="email" id="email" name="email" v-model="patientData.email"
                                                class="form-control">
                                        </div>

                                        <div class="mb-3">
                                            <label for="gjinia" class="form-label">Gjinia:</label>
                                            <input type="text" id="gjinia" name="gjinia" v-model="patientData.gjinia"
                                                class="form-control">
                                        </div>
                                        <div class="mb-3">
                                            <label for="nacionalitetiId" class="form-label">Nacionaliteti:</label>
                                            <input type="number" id="nacionalitetiId" name="nacionalitetiId"
                                                v-model="patientData.nacionalitetiId" class="form-control">
                                        </div>
                                    </div>

                                    <div class="form-column">
                                        <div class="mb-3">
                                            <label for="dataLindjes" class="form-label">Data e Lindjes:</label>
                                            <input type="date" id="dataLindjes" name="dataLindjes"
                                                v-model="formattedDataLindjes" class="form-control">

                                        </div>


                                        <div class="mb-3">
                                            <label for="nrTel" class="form-label">Numri i Telefonit:</label>
                                            <input type="tel" id="nrTel" name="nrTel" v-model="patientData.nrTel"
                                                class="form-control">
                                        </div>

                                        <div class="mb-3">
                                            <label for="qyteti" class="form-label">Qyteti:</label>
                                            <input type="text" id="qyteti" name="qyteti" v-model="patientData.qyteti"
                                                class="form-control">
                                        </div>

                                        <div class="mb-3">
                                            <label for="alergji" class="form-label">Alergjitë:</label>
                                            <textarea id="alergji" name="alergji" v-model="patientData.alergji"
                                                class="form-control"></textarea>
                                        </div>
                                        <div class="mb-3">
                                            <label for="nrTel" class="form-label">Numri Leternjoftimit:</label>
                                            <input type="text" id="nrTel" name="nrTel"
                                                v-model="patientData.nrLeternjoftimit" class="form-control">
                                        </div>


                                    </div>
                                </div>

                                <button style="margin-left: 10px;" type="submit" class="btn btn-primary">Update</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <ProfileCard />
        </div>

    </div>
</template>
  

<script>
import axios from "axios";
import ProfileCard from "~/components/ProfileCard";
import DropdownButton from '~/components/DropdownButton.vue';
export default {
    components: {
        ProfileCard,
        DropdownButton
    },
    computed: {
        formattedDataLindjes: {
            get() {
                if (this.patientData.dataLindjes) {
                    const date = new Date(this.patientData.dataLindjes);
                    const year = date.getFullYear();
                    const month = (date.getMonth() + 1).toString().padStart(2, '0');
                    const day = date.getDate().toString().padStart(2, '0');
                    return `${year}-${month}-${day}`;
                }
                return '';
            },
            set(value) {
                if (value) {
                    const [year, month, day] = value.split('-');
                    this.patientData.dataLindjes = `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
                } else {
                    this.patientData.dataLindjes = '';
                }
            }
        }

    },
    data() {
        return {
            patientData: {},
            isFormOpen: false
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
        async updateDataLindjes(event) {
            const selectedDate = event.target.value;
            if (selectedDate) {
                const [year, month, day] = selectedDate.split('-');
                this.patientData.dataLindjes = `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
            } else {
                this.patientData.dataLindjes = '';
            }
        },
        async updatePatientData(event) {
            event.preventDefault();


            try {
                // Prepare the updated patient data
                const updatedData = {
                    emri: this.patientData.emri,
                    mbiemri: this.patientData.mbiemri,
                    email: this.patientData.email,
                    gjinia: this.patientData.gjinia,
                    dataLindjes: this.patientData.dataLindjes,
                    nrTel: this.patientData.nrTel,
                    qyteti: this.patientData.qyteti,
                    alergji: this.patientData.alergji,
                    nrLeternjoftimit: this.patientData.nrLeternjoftimit,
                    shtetiId: this.patientData.shtetiId,
                    nacionalitetiId: this.patientData.nacionalitetiId

                };
                console.log(this.patientData.dataLindjes);
                console.log(updatedData);
                // Make the POST request to update the patient data
                const response = await axios.put(
                    `https://localhost:7292/api/PacientiAPI/${this.patientData.id}`,
                    updatedData
                );

                // Handle the response if needed

                console.log("Patient data updated successfully:", response.data);

            } catch (error) {
                console.log("Error while updating patient data:", error);
            }
            window.location.reload();
        },
        async fetchPatientData(uid) {
            try {
                console.log("fetchPatientData() called with UID:", uid);
                const response = await axios.get(`https://localhost:7292/api/PacientiAPI/GetPacientiByUId/${uid}`);
                this.patientData = response.data;
                console.log("Fetched patient data:", this.patientData);

            } catch (error) {
                console.log("Error while fetching patient data:", error);
            }
        },
        handleItemClick(item) {
            console.log('Clicked:', item);
        },
        openForm() {
            this.isFormOpen = true;
        },
        closeForm() {
            this.isFormOpen = false;
        }
    }
}
</script>

<style scoped>
/* Style specific form elements */

input {
    width: 94% !important;
}

.form-row {
    display: flex;
}

.form-column {
    flex: 1;
    margin: 0px 10px;
}

.dashboard {
    height: 100vh;
}

.form-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 999;
}

.form-container {
    background-color: #fff;
    padding: 20px;
    border-radius: 5px;


}

.close-button {
    position: absolute;
    top: 40%;
    right: 40%;
    background: none;
    border: none;
    color: #fff;
    font-size: 18px;
    cursor: pointer;
}

.carta {
    display: flex;
    flex-direction: column;
    justify-content: flex-end;
    margin-top: 90px;
}



/* CSS */
.button-35 {
    margin-left: 18%;
    align-items: center;
    background-color: #fff;
    border-radius: 12px;
    box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
    width: 175px;
    margin-bottom: 9px;
    box-sizing: border-box;
    color: #121212;
    cursor: pointer;
    display: inline-flex;
    flex: 1 1 auto;
    font-family: Inter, sans-serif;
    font-size: 1.0rem;
    font-weight: 700;
    justify-content: center;
    line-height: 1;

    outline: none;
    padding: 0.8rem 1rem;
    text-align: center;
    text-decoration: none;
    transition: box-shadow .2s, -webkit-box-shadow .2s;
    white-space: nowrap;
    border: 0;
    user-select: none;
    -webkit-user-select: none;
    touch-action: manipulation;
}

.button-35:hover {
    box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
}
</style>