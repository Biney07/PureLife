<template>
    <SweetAlert v-if="showSweetAlert" :message="alertMessage" :type="alertType" :duration="2000" />

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
                                            <label for="pictureUrl" class="form-label">Upload Picture:</label>
                                            <input type="file" id="pictureUrl" name="pictureUrl" @change="onFileChange"
                                                class="form-control">
                                        </div>

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
                                            <label for="gjinia" class="form-label">Gjinia:</label>
                                            <select id="gjinia" name="gjinia" v-model="patientData.gjinia"
                                                class="form-control">
                                                <option value="Mashkull">Mashkull</option>
                                                <option value="Femer">Femer</option>
                                            </select>
                                        </div>

                                        <div class="mb-3">
                                            <label for="nacionalitetiId" class="form-label">Nacionaliteti:</label>
                                            <select id="nacionalitetiId" name="nacionalitetiId" v-model="nacionalitetid"
                                                class="form-control">
                                                <option v-for="nacionalitet in nacinaliteti" :key="nacionalitet.id"
                                                    :value="nacionalitet.id">{{ nacionalitet.emri }}</option>
                                            </select>
                                        </div>
                                        <div class="mb-3">
                                            <label for="shtetetid" class="form-label">Shteti:</label>
                                            <select id="shtetetid" name="shtetetid" v-model="shtetiid" class="form-control">
                                                <option v-for="shtet in shtetet" :key="shtet.id" :value="shtet.id">{{
                                                    shtet.emri }}</option>
                                            </select>
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
            <div class="termini">
                <Terminishow />
            </div>
          

        </div>

    </div>
</template>
  

<script>
import axios from "axios";
import ProfileCard from "~/components/ProfileCard";
import DropdownButton from '~/components/DropdownButton.vue';
import Terminishow from '~/components/TerminetShow.vue';
import SweetAlert from '~/components/SweetAlert.vue';

export default {
    components: {
        ProfileCard,
        DropdownButton,
        Terminishow,
        SweetAlert

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
        },

    },
    data() {
        return {
            patientData: {},
            isFormOpen: false,
            nacinaliteti: {},
            shtetet: {},
            nacionalitetid: null,
            shtetiid: null,
            showSweetAlert: true,
            alertMessage: 'Your Profile is complete',
            alertType: 'success',
            hasNullValues: true,
        };
    },
    created() {
        this.fetchNationalities();
        this.fetchQytet();
    },
    mounted() {

        const useri = JSON.parse(localStorage.getItem('patient'));


        if (useri && useri.user.uid) {
            const uid = useri.user.uid;

            this.fetchPatientData(uid);
        }

    },


    methods: {
        async fetchNationalities() {
            try {
                const response = await axios.get('https://localhost:7292/api/ShtetiAPI');
                this.shtetet = response.data;
                console.log('Fetched nationalities:', this.shtetet);
            } catch (error) {
                console.log('Error while fetching nationalities:', error);
            }
        },
        async fetchQytet() {
            try {
                const response = await axios.get('https://localhost:7292/api/Nacionaliteti');
                this.nacinaliteti = response.data;
                console.log('Fetched nationalities:', this.nacinaliteti);
            } catch (error) {
                console.log('Error while fetching nationalities:', error);
            }
        },
        async onFileChange(event) {
            const file = event.target.files[0];
            this.patientData.pictureFile = file;
        },
        async updatePatientData(event) {
            event.preventDefault();

            try {
                const formData = new FormData();
                formData.append('Id', this.patientData.id);
                formData.append('PictureFile', this.patientData.pictureFile);
                formData.append('Emri', this.patientData.emri);
                formData.append('Mbiemri', this.patientData.mbiemri);
                formData.append('Gjinia', this.patientData.gjinia);
                formData.append('DataLindjes', this.patientData.dataLindjes);
                formData.append('NrTel', this.patientData.nrTel);
                formData.append('Qyteti', this.patientData.qyteti);
                formData.append('Alergji', this.patientData.alergji);
                formData.append('NrLeternjoftimit', this.patientData.nrLeternjoftimit);
                formData.append('ShtetiId', this.shtetiid);
                formData.append('NacionalitetiId', this.nacionalitetid);
                for (const [key, value] of formData.entries()) {
                    console.log(key + ':', value);
                }
                // Log the values of the FormData object


                const response = await axios.put(
                    `https://localhost:7292/api/PacientiAPI/${this.patientData.id}`,
                    formData,
                    {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        }
                    }
                );

            } catch (error) {
                console.log('Error while updating patient data:', error);
            }






        },

        async fetchPatientData(uid) {
            try {

                const response = await axios.get(`https://localhost:7292/api/PacientiAPI/GetPacientiByUId/${uid}`);
                this.patientData = response.data;
                console.log(this.patientData);
                this.nacionalitetid = this.patientData.nacionalitetiId;
                this.shtetiid = this.patientData.shtetiId;
                const values = Object.values(this.patientData);
                const excludedProperties = ['modifiedDate', 'modifiedFrom'];

                this.hasNullValues = values.some((value, index) => {
                    const propertyName = Object.keys(this.patientData)[index];
                    return value === null && !excludedProperties.includes(propertyName);
                });

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
        },
        selectNationality(nationality) {
            this.patientData.nacionalitetiId = nationality.id;
        },
    }
}
</script>

<style scoped>
.dashboard{
    margin-top: 150px;
}
.termini {
    padding: 70px 90px;
}

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
    margin-left: 23%;
    align-items: center;
    background-color: #fff;
    border-radius: 12px;
    box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
    width: 175px;
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
    margin-bottom: 1%;
}

.button-35:hover {
    box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
}
</style>