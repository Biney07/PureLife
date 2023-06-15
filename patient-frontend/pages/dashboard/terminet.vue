<template>
  <div class="terminet">
    <div class="filter-buttons">
      <div class="form-group">
        <label for="date">Zgjedhni Daten:</label>
        <input v-model="selectedDate" type="date" id="date" class="form-control">
      </div>

      <div>
            <label class="form-label font-weight-normal" for="reparti">Reparti:</label>
            <select id="reparti" v-model="terminiData.reparti" class="form-control">
            <option value="" disabled selected>Zgjidhni repartin</option>
            <option v-for="reparti in repartet" :value="reparti.id" :key="reparti.id">{{ reparti.emri }}</option>
            </select>
        </div>

      <div>
        <label class="form-label font-weight-normal" for="doctor">Doktori:</label>
        <select id="doctor" v-model="terminiData.stafi" class="form-control" :disabled="terminiData.reparti === null">
          <option value="" disabled selected>Zgjedhni Doktorin:</option>
          <option v-for="doctor in doctors" :value="doctor.id" :key="doctor.id">{{ doctor.emri }} {{ doctor.mbiemri }}</option>
        </select>
      </div>
    </div>

    <div class="terminet">
        <div v-if="!terminet.length" class="terminet-warning">
            <h1>Nuk ka termine te krijuara per daten e zgjedhur</h1>
            <p>Ju lutemi qe paraprakisht te plotesoni fushat me larte!</p>
        </div>

        <div class="termini-container" v-else>
            <div @click="showDetails(termini)" class="termini-text-container" v-for="termini in terminet" :key="termini.id">
                <p>{{termini.startTime.split(' ')[1].slice(0, -3) + ' ' + termini.startTime.split(' ')[2]}} - {{termini.endTime.split(' ')[1].slice(0, -3) + ' ' + termini.endTime.split(' ')[2]}}</p>
            </div>
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
                console.log(this.terminet)
            }
        } catch (err) {
            console.log(err)
        }
    },
    closeModal() {
      this.showModal = false;
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
            }
        } catch (err) {
            console.log(err)
        }
    }
  }
}
</script>


<style scoped>

.terminet {
    width: 80%;
    height: 100%;
    padding: 7% 5% 0 5%;
    margin: 0 auto 30px auto;
}

.filter-buttons {
    display: flex;
    justify-content: space-evenly;
    align-items: center;
}

.filter-buttons div {
    display: flex;
    flex-direction: column;
}

.terminet-warning{
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
    background: rgba(44, 101, 216, 0.9);
    transition: background-color 0.3s;
    cursor: pointer;
}
</style>