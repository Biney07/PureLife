<template>
    <div
        style="position: fixed; top: 150px; right: 0; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.25); display: flex; align-items: flex-end; flex-direction: column; padding:10px 30px">
        <h3 class="profile-nameA">Borxhi Total:</h3>
        <h3 class="profile-nameB">{{ calculateTotalDebt }}€</h3>
    </div>
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

                    </div>

                    <div class="left-part">
                        <h5 class="title">Diagnoza:</h5>
                        <h4 class="parag">{{ terapia.diagnoza }}</h4>
                        <h5 class="title">Përshkrimi:</h5>
                        <h4 class="parag">{{ terapia.pershkrimi }}</h4>
                        <h5 class="title">Barnat:</h5>
                        <h4 class="parag">{{ terapia.barnat }}</h4>
                        <h5 class="title">Çmimi:</h5>
                        <h4 class="cmimi">{{ terapia.cmimi }} Euro</h4>
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

                                    <th scope="col">View</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="analiza in terapia.analizat" :key="analiza.id">
                                    
                                    <td>{{ analiza.emriAnalizes }}</td>
                                    <td>{{ analiza.cmimi }} €</td>
                                    <td>
                                        <button class="btn btn-primary" style="background-color: var(--blue);"
                                            @click="analizaopen(analiza.id, terapia.id)">View</button>
                                    </td>


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


                        <td>{{ termin.doktori }}</td>
                        <td>{{ getDate(termin.startTime) }}</td>
                        <td>{{ getTime(termin.startTime) }}</td>
                        <td>{{ getTime(termin.endTime) }}</td>
                        <td>{{ termin.reparti }}</td>
                        <td :class="getStatusClass(termin.statusPaid)">{{ termin.statusPaid }}</td>
                        <td v-if="isPaidVisible(termin.statusPaid)">
                            <button class="btn " style="background-color: purple; color:white"
                                @click="openForm(termin.id)">View</button>
                        </td>
                        <td v-if="isUnpaidVisible(termin.statusPaid)">
                            <button class="btn" style="background-color: navy; color:white" @click="payNow(termin.id)"
                                :disabled="isLoading">
                                <span v-if="isLoading" class="spinner-border spinner-border-sm" role="status"
                                    aria-hidden="true"></span>
                                <span v-else>Pay Now</span>
                            </button>
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
            termini: [],
            terapia: {},
            loading: true,
            isLoading: false,
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
        analizaopen(analizaId, terapiaId) {
            localStorage.setItem('analizaData', JSON.stringify({ analizaId, terapiaId }));
            window.location.href = "http://localhost:3000/dashboard/analiza";
        },


        getStatusClass(status) {
            return status === 'I paguar' ? 'paid' : 'unpaid';
        },
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
        },
        payNow(id) {
            this.isLoading = true; // Activate the loading state

            const url = `https://localhost:7292/api/TerminiAPI/${id}`;
            const data = {
                statusPaid: true
            };

            axios.put(url, data)
                .then(response => {
                    console.log('StatusPaid updated successfully.');
                    // Perform any additional actions upon successful update

                    setTimeout(() => {
                        this.isLoading = false; // Deactivate the loading state after 2 seconds
                    }, 2000);
                })
                .catch(error => {
                    console.error('Error updating StatusPaid:', error);
                    // Handle any errors that occur during the update process
                    this.isLoading = false; // Deactivate the loading state
                });
            window.location.reload();
        }

    },
    computed: {
        calculateTotalDebt() {
            let total = 0;

            for (const termin of this.termini) {
                if (termin.statusPaid === 'I pa paguar') {
                    total += termin.price;
                }

            }

            return total;
        },
        isPaidVisible() {
            return (statusPaid) => statusPaid === 'I paguar';
        },
        isUnpaidVisible() {
            return (statusPaid) => statusPaid === 'I pa paguar';
        }
    }


};
</script>
<style scoped>
.profile-nameA {
    font-size: 24px;
    /* Reduced font size */
    margin: 0px;
    /* Adjusted margin */
    color: var(--blue);
}

.profile-nameB {
    font-size: 54px;
    font-weight: 200;
    /* Reduced font size */
    margin: 0px;
    /* Adjusted margin */
    color: var(--blue);
}

.paid {
    background-color: rgb(159, 240, 159);
    color: green;
}

.unpaid {
    background-color: rgb(243, 150, 150);
    color: rgb(138, 3, 3);
}

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