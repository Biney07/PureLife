<template>
    <div>
        <div v-if="isFormOpen" class="form-overlay">
            <div class="form-container">
                <div style="display: flex; justify-content: flex-end;">
                    <img id="svgButton" src="../../assets/closebutton.svg" alt="SVG Button" @click="closeForm"
                        style="color:var(--blue); width: 30px;">
                </div>
                <div>
                    <h1 class="mtitle">TERAPIA</h1>
                    <div class="spacebetween">
                        <h3 class="capitalize">{{ terapia.pacienti }}</h3>
                        <h3 class="capitalize">Termin Id: {{ terapia.terminiId }}</h3>
                    </div>

                    <div class="left-part">
                        <h5 class="title">Diagnoza:</h5>
                        <h4 class="parag">{{ terapia.diagnoza }}</h4>
                        <h5 class="title">Përshkrimi:</h5>
                        <h4 class="parag">{{ terapia.pershkrimi }}</h4>
                        <h5 class="title">Barnat:</h5>
                        <h4 class="parag">{{ terapia.barnat }}</h4>
                        <h5 class="title">Çmimi:</h5>
                        <h4 class="cmimi">50 €</h4>
                    </div>

                    <div class="right-part">
                        <p class="title" style="margin-bottom: 0;">Sherbimet:</p>
                        <ul style="padding: 0;">
                            <li class="parag" style="list-style: none; font-weight: 300; text-transform: uppercase;"
                                v-for="sherbimi in terapia.sherbimet" :key="sherbimi">
                                {{ sherbimi }}
                            </li>
                        </ul>

                        <p class="title">Analizat:</p>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col">Emri Analizes</th>
                                    <th scope="col">Price</th>
                                    <th scope="col">Date</th>
                                    <th scope="col">Time</th>
                                    <th scope="col">View</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="analiza in terapia.analizat" :key="analiza.id">
                                    <td>{{ analiza.emriAnalizes }}</td>
                                    <td>{{ analiza.cmimi }}</td>
                                    <td>{{ new Date(analiza.data.split('T')[0]).toLocaleDateString('en-US', {
                                        day: 'numeric',
                                        month: 'long', year: 'numeric'
                                    }) }}</td>

                                    <td>{{ analiza.data.split('T')[1].substring(0, 5) }}</td>
                                    <td> <button class="btn btn-primary" style="background-color: var(--blue);" @click="analizaopen(analiza.id)">View</button></td>
                                </tr>
                            </tbody>
                        </table>


                    </div>

                </div>
            </div>
        </div>

        <div v-if="loading" class="spinner-border text-primary" role="status"
            style="height: 100px; width: 100px; margin-top: 100px; color: var(--blue)">
        </div>
        <h3 style="color: var(--blue); margin-top: 10px" v-if="loading">Loading...</h3>
        <div v-else>
            <h1>Terminet</h1>
            <table class="table table-striped">
                <thead>
                    <tr>

                        <th>Emri i Pacientit</th>
                        <th>Mbiemri i Pacientit</th>
                        <th>Doktori</th>
                        <th>Data</th>
                        <th>Ora e Fillimit</th>
                        <th>Ora e Mbarimit</th>
                        <th>Departamenti</th>
                        <th>Statusi</th>
                        <th>View</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="termin in termini" :key="termin.id">

                        <td>{{ termin.pacientiName }}</td>
                        <td>{{ termin.pacientiLastName }}</td>
                        <td>{{ termin.doktori }}</td>
                        <td>{{ termin.reparti }}</td>
                        <td>{{ getDate(termin.startTime) }}</td>
                        <td>{{ getTime(termin.startTime) }}</td>
                        <td>{{ getTime(termin.endTime) }}</td>
                        <td>{{ termin.statusPaid }}</td>
                        <td>
                            <button class="btn btn-primary" @click="openForm(termin.id)">View</button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div v-if="termini && termini.length === 0">
                No termini found.
            </div>
        </div>
    </div>
</template>
  
<script>
import axios from 'axios';

export default {
    data() {
        return {
            termini: {},
            terapia: {},
            loading: true,
            selectedTerminId: null,
            isFormOpen: false
        };
    },
    mounted() {
        const useri = JSON.parse(localStorage.getItem('patient'));

        if (useri && useri.user.uid) {
            const uid = useri.user.uid;

            axios.get('https://localhost:7292/api/TerminiAPI/GetTerminetByPacient/' + uid)
                .then(response => {
                    this.termini = response.data;
                    console.log(this.termini);
                    this.loading = false;
                })
                .catch(error => {
                    console.error(error);
                    this.loading = false;
                });
        }
    },
    methods: {
        getDate(dateTime) {
            return new Date(dateTime).toLocaleDateString();
        },
        getTime(dateTime) {
            return new Date(dateTime).toLocaleTimeString([], { timeStyle: 'short' });
        },
        handleItemClick(item) {
            console.log('Clicked:', item);
        },
        openForm(id) {
            this.isFormOpen = true;
            this.selectedTerminId = id;
            axios
                .get('https://localhost:7292/api/TerapiaAPI/GetTerapiaByTermin/' + this.selectedTerminId)
                .then(response => {
                    this.terapia = response.data;
                    console.log(this.terapia);

                })
        },
        closeForm() {
            this.isFormOpen = false;
            this.selectedTerminId = null;
            this.terapia = null;
        }
    }
};
</script>
<style scoped>
.cmimi {
    background-color: var(--blue);
    color: white;
    font-size: 50px;
    width: 50%;
    padding: 5px 10px;
}

.left-part {
    float: left;
    width: 50%;
}

.right-part {
    float: right;
    width: 50%;
}

.parag {
    font-size: 22px;
    font-weight: 200;
    color: var(--blue);
}

.mtitle {
    font-weight: 900;
    color: var(--blue);

}

.title {
    color: var(--blue);
    font-weight: 500;
    font-size: 26px;
    margin-top: 20px;
    text-transform: uppercase;
}

.capitalize {
    text-transform: capitalize !important;
    color: var(--blue);
}

.spacebetween {
    display: flex;
    justify-content: space-between;
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
    width: 70%;

}
</style>