<template>
  <div class="terminet-container">
    <div class="terminet">
    <div class="filter-sections">
      <div class="form-group">
        <div class="filter">
            <label class="date-label" for="date">Zgjedhni Daten:</label>
            <input :min="currentDate" required v-model="selectedDate" class="date-input" type="date" id="date" placeholder="Zgjedhni Daten:" />
        </div>
        
        <div class="arrow-down">
            <img src="https://www.svgrepo.com/show/80156/down-arrow.svg" alt="Arrow" />
        </div>

        <div class="filter">
            <label class="date-label" for="reparti">Reparti:</label>
            <select id="reparti" v-model="terminiData.reparti" class="reparti-input">
            <option value="" disabled selected>Zgjidhni repartin</option>
            <option v-for="reparti in repartet" :value="reparti.id" :key="reparti.id">{{ reparti.emri }}</option>
            </select>
        </div>
        
        <div class="arrow-down">
            <img src="https://www.svgrepo.com/show/80156/down-arrow.svg" alt="Arrow" />
        </div>

        <div class="filter" v-if="doctors.length">
            <label class="date-label" for="doctor">Doktori:</label>
            <div class="doctor-cards-container">
              <div
                v-for="doctor in doctors"
                :key="doctor.id"
                class="doctor-cards"
                @click="selectDoctor(doctor.id)"
                :class="{ active: terminiData.stafi === doctor.id }"
              >
                <img :src="doctor.pictureUrl" alt="Doctor Image" class="doctor-image" />
                <p class="doctor-name">{{ doctor.emri }} {{ doctor.mbiemri }}</p>
            </div>
            </div>
        </div>
        <div class="filter" v-if="!doctors.length && terminiData.reparti">
            <p class="filter-doctor-warning">Nuk kemi staf ne kete repart!</p>
        </div>
      </div>
    </div>

    <div class="terminet-cards">
        <div class="termini-container" v-if="terminet.length">
            <div @click="showDetails(termini)" class="termini-text-container" v-for="termini in terminet" :key="termini.id">
                <p>{{termini.startTime.split(' ')[1].slice(0, -3) + ' ' + termini.startTime.split(' ')[2]}} - {{termini.endTime.split(' ')[1].slice(0, -3) + ' ' + termini.endTime.split(' ')[2]}}</p>
            </div>
        </div>
        <div v-else-if="!terminiData.reparti || !terminiData.stafi" class="terminet-warning">
            <h2>Ju lutemi qe paraprakisht te plotesoni fushat per filtrim!</h2>
        </div>
        <div v-else class="terminet-warning">
            <h2>Nuk ka termine te lira per daten e zgjedhur</h2>
            <p>Ju lutemi qe paraprakisht te plotesoni fushat me larte!</p>
        </div>
    </div>

    <TerminiModal
      v-if="showModal && modalData"
      @close="closeModal"
    >
      <template v-slot:header>
        Rezervo Terminin
      </template>

      <template v-slot:body>
        <div>
            <p class="confirm-text">Per te konfirmuar rezervimin e terminit ju lutem shkruani email-in tuaj!</p>
            <p class="confirm-text">Data e terminit: {{selectedDate}}</p>
            <p class="confirm-text">Koha: {{modalData.startTime.split(' ')[1].slice(0, -3) + ' ' + modalData.startTime.split(' ')[2]}}</p>
        </div>
        <div class="email-input-container">
            <label for="email">Email:</label>
            <input required v-model="email" type="email" id="email" class="form-control">
        </div>
        <p v-if="wrongEmail" class="wrong-email-text text-danger mt-2">{{wrongEmail}}</p>
      </template>

      <template v-slot:footer>
        <p
            type="button"
            class="save-button"
            @click="rezervoTerminin"
        >
            Rezervo
        </p>
      </template>
    </TerminiModal>
   </div>
  </div>
</template>

<script>
import { getSpecializimet } from "@/patient-sdk/repartet.js"
import {getStaffByLemi} from "@/patient-sdk/staff.js"
import {getTerminetByDate, rezervoTerminin} from "@/patient-sdk/terminet.js"
import TerminiModal from '../../components/TerminiModal.vue';
import { getLocalStorage } from "@/helper/auth"
import {getPacienti} from "@/patient-sdk/auth.js"
export default {
    components: {
        TerminiModal,
    },
    setup() {
        definePageMeta({
            middleware: [
                'auth',
                // Add in more middleware here
            ]
        });
    },
  data() {
    return {
      selectedDate: null,
      terminiData: {
        reparti: null,
        stafi: null
      },
      repartet: [],
      doctors: [],
      terminet: [],
      showModal: false,
      modalData: null,
      email: null,
      wrongEmail: null,
      currentDate: null,
    }
  },
  watch: {
    selectedDate(newDate) {
      if (newDate === null) {
        this.terminiData.nacionalitetiId = null;
        this.terminiData.doctorId = null;
      }
      this.fetchTerminet()
    },
    "terminiData.reparti"(newReparti) {
      if (newReparti === null) {
        this.terminiData.stafi = null;
      }
      this.fetchStafi()
      this.terminiData.stafi = null
    },
    "terminiData.stafi"(newStaff) {
        if(newStaff === null) {
            this.terminiData.stafi = null;
        }
        this.fetchTerminet()
    }
  },
  mounted() {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    let month = currentDate.getMonth() + 1;
    month = month < 10 ? `0${month}` : month;
    let day = currentDate.getDate();
    day = day < 10 ? `0${day}` : day;

    this.currentDate = `${year}-${month}-${day}`

    this.selectedDate = `${year}-${month}-${day}`;

    this.fetchSpecializimet()
  },
  methods: {
    async fetchSpecializimet() {
        try {
            const response = await getSpecializimet()
            this.repartet = response.data
            
        } catch (err) {
            console.log(err)
        }
    },
    async fetchStafi() {
        try {
            const response = await getStaffByLemi(this.terminiData.reparti)
            this.doctors = response.data
            
        } catch (err) {
            console.log(err)
        }
    },
    async fetchTerminet() {
        try {
            if(this.selectedDate && this.terminiData.reparti && this.terminiData.stafi) {
                const response = await getTerminetByDate(this.selectedDate)
                this.terminet = response.data
                this.terminet = this.terminet.filter(termini => termini.stafiId === this.terminiData.stafi && termini.status === false)
            }
        } catch (err) {
            console.log(err)
        }
    },
    closeModal() {
      this.showModal = false;
      this.email = null;
    },
    showDetails(termini) {
        this.showModal = true
        this.modalData = termini;
        console.log(this.modalData)
    },
    async rezervoTerminin() {
        try {
            const user = getLocalStorage()
            const pacientiResponse = await getPacienti(user.user.uid)
            console.log(pacientiResponse)

            if(user.user.email === this.email) {
                await rezervoTerminin(this.modalData.id, pacientiResponse.data.id)
                this.closeModal()
                this.fetchTerminet()
            } else {
                setInterval(() => {
                    this.wrongEmail = null
                }, 3000)
                this.wrongEmail = "Emaili i shtypur nuk pershtatet me email-in tuaj. Porvoni serish!"
            }
        } catch (err) {
            console.log(err)
        }
    },
    selectDoctor(stafiId) {
      this.terminiData.stafi = stafiId
      console.log(stafiId, this.terminiData)
    }
  }
}
</script>

<style scoped>
.terminet-container {
  margin-top: 50px;
}

.terminet {
  display: flex;
  gap: 7%;
  padding: 5%;
}

.filter-sections {
  width: 30%;
}

.terminet-cards {
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  align-items: flex-start;
  gap: 30px;
  margin-top: 30px;
}

@media (max-width: 768px) {
  .terminet {
    flex-direction: column;
  }

  .filter-sections {
    width: 100%;
  }

  .terminet-cards {
    margin-top: 30px;
  }
}

.terminet-warning{
    margin: 0 auto;
    text-align: center;
    font-family: var(--primary-font);
}

.terminet-warning p{
    color: red;
    font-weight: 500;
}

.termini-container{
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    margin: 0 auto 30px auto;
}

.termini-text-container {
    width: 200px;
    text-align: center;
    margin: 0 auto 30px auto;
}

.termini-container p{
    padding: 10px 20px;
    border: 3px solid var(--primary-color);
    border-radius: 6px;
    transition: border-color 0.1s ease-in;
    color: black;
    font-weight: 500;
}

.termini-container p:hover{
    border: 3px solid var(--button-background);
    transition: border-color 0.1s ease-in;
    cursor: pointer;
}

.confirm-text{
    font-weight: 500;
    font-family: var(--primary-font);
    margin: 5px;
}

.email-input-container{
    margin-top: 10px;
}

.save-button{
    background: #5cb85c;
    width: fit-content;
    padding: 10px 20px;
    border-radius: 10px;
    color: white;
    transition: background-color 0.3s;
    font-size: 1rem;
    font-weight: 500;
}

.save-button:hover{
    background: #5cb85ce1;
    transition: background-color 0.3s;
    cursor: pointer;
}

.wrong-email-text{
    font-weight: 500;
}

.form-group {
    width: 100%;
}

.form-group .filter {
    display: flex;
    flex-direction: column;
}


.form-group .filter:not(:first-child) {
    margin-top: 20px;
}

.date-input, .reparti-input{
    border: 1px solid #abacad;
    padding: 18px 15px;
    margin-top: 5px;
    font-size: 18px;
}

.date-label{
    font-weight: 500;
    font-size: 18px;
}

.doctor-image {
  display: inline-block;
  width: 20px;
  height: 20px;
  margin-right: 5px;
  border-radius: 50%;
}

.arrow-down {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 10px 0;
}

.arrow-down img {
  width: 20px;
  height: 20px;
}
.doctor-cards-container{
  width: 100%;
  display: flex;
  flex-direction: row !important;
  justify-content: space-between;
  align-items: center;
  text-align: center;
  padding: 10px;
  margin-top: 10px;
  flex-wrap: wrap;
}

.doctor-cards{
  width: 120px;
  padding: 20px 10px;
  box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
  cursor: pointer;
  transition: box-shadow 0.1s ease-in;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 20px;
  margin-top: 10px;
}

.active{
  box-shadow: var(--button-background) 0px 1px 4px, rgb(51, 51, 51) 0px 0px 0px 3px;
}

.doctor-image{
  height: 80px;
  width: 80px;
}

.filter-doctor-warning{
  color: red;
  font-weight: 500;
  text-align: center;
  font-size: 18px;
  font-family: var(--primary-font);
}
</style>