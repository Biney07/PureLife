<template>
  <div class="terminet">
    <div v-if="step === 1" class="step-container">


      <h1 style="font-weight: 600; font-size: 45px ; margin-bottom: 20px;">REZERVONI TERMININ</h1>
      <div class="form-group">
        <h3 class="blueheader" for="date">ZGJEDHNI DATEN:</h3>
        <input v-model="selectedDate" type="date" id="date" class="form-control">
      </div>
      <div>
        <h3 class="blueheader" for="reparti">KLASIFIKONI PROBLEMIN:</h3>
        <div class="reparti-container">
          <div v-for="reparti in repartet" :key="reparti.id" @click="selectReparti(reparti.id)"
            :class="{ 'reparti-card': true, 'selected': reparti.id === terminiData.reparti }">
            <p>{{ reparti.emri }}</p>
          </div>
        </div>
      </div>

      <div class="button-container">
        <button type="button" class="btn btn-primary" @click="nextStep">Next</button>
      </div>
    </div>
    <div v-if="step === 2" class="step-container">
      <h3 class="blueheader">ZGJEDHNI MEJKUN:</h3>
      <div class="doctors">
        <div v-for="doctor in doctors" :key="doctor.id" @click="selectDoctor(doctor.id)"
          :class="{ 'doktori-card': true, 'selected': terminiData.stafi === doctor.id }"
          :style="{ backgroundImage: `url(${doctor.pictureUrl})` }">
          <p>{{ doctor.emri }} {{ doctor.mbiemri }}</p>
        </div>
      </div>
      <div class="buttons">
        <button class="btn btn-secondary" @click="previousStep">Kthehu</button>
        <button class="btn btn-primary" @click="nextStep">Vazhdo</button>
      </div>

    </div>
    <div v-if="step === 3" class="step-container">
      <h3 class="blueheader" for="reparti">ZGJEDHNI TERMININ:</h3>
      <div v-if="!terminet.length" class="terminet-warning">
        <h1>Nuk ka termine te krijuara per daten e zgjedhur</h1>
        <p>Ju lutemi qe paraprakisht te plotesoni fushat me larte!</p>
      </div>
      <div class="termini-container" v-else>
        <div @click="showDetails(termini)" class="termini-text-container" v-for="termini in terminet" :key="termini.id">
          <p>{{ termini.startTime.split(' ')[1].slice(0, -3) + ' ' + termini.startTime.split(' ')[2] }}</p>
        </div>
      </div>
      <div class="button-container">
        <button class="btn btn-primary" @click="previousStep">Back</button>
      </div>
    </div>
    <TerminiModal v-if="showModal && modalData" @close="closeModal">
      <template v-slot:header>
        Rezervo Terminin
      </template>
      <template v-slot:body>
        <div>
          <p class="confirm-text">Per te konfirmuar rezervimin e terminit ju lutem shkruani email-in tuaj!</p>
          <p class="confirm-text">Data e terminit: {{ selectedDate }}</p>
          <p class="confirm-text">Koha: {{ modalData.startTime.split(' ')[1].slice(0, -3) + ' ' +
            modalData.startTime.split(' ')[2] }}</p>
        </div>
        <div class="email-input-container">
          <label for="email">Email:</label>
          <input required v-model="email" type="email" id="email" class="form-control">
        </div>
        <p v-if="wrongEmail" class="wrong-email-text text-danger mt-2">{{ wrongEmail }}</p>
      </template>
      <template v-slot:footer>
        <p type="button" class="btn btn-primary" @click="rezervoTerminin">Rezervo</p>
      </template>
    </TerminiModal>
  </div>
</template>

<script>
import { getSpecializimet } from "@/patient-sdk/repartet.js"
import { getStaffByLemi } from "@/patient-sdk/staff.js"
import { getTerminetByDate, rezervoTerminin } from "@/patient-sdk/terminet.js"
import TerminiModal from '../../components/TerminiModal.vue'
import { getLocalStorage } from "@/helper/auth"
import { getPacienti } from "@/patient-sdk/auth.js"

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
    })
  },
  data() {
    return {
      selectedDate: null,
      terminiData: {
        reparti: null,
        stafi: null,
      },
      repartet: [],
      doctors: [],
      terminet: [],
      showModal: false,
      modalData: null,
      email: null,
      wrongEmail: null,
      step: 1
    }
  },
  watch: {
    selectedDate(newDate) {
      if (newDate === null) {
        this.terminiData.nacionalitetiId = null
        this.terminiData.doctorId = null
      }
      this.fetchTerminet()
    },
    "terminiData.reparti"(newReparti) {
      if (newReparti === null) {
        this.terminiData.stafi = null
      }
      this.fetchStafi()
    },
    "terminiData.stafi"(newStaff) {
      if (newStaff === null) {
        this.terminiData.stafi = null
      }
      this.fetchTerminet()
    }
  },
  mounted() {
    const currentDate = new Date()
    const year = currentDate.getFullYear()
    let month = currentDate.getMonth() + 1
    month = month < 10 ? `0${month}` : month
    let day = currentDate.getDate()
    day = day < 10 ? `0${day}` : day

    this.selectedDate = `${year}-${month}-${day}`

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
    async selectReparti(repartiId) {
      this.terminiData.reparti = repartiId;
    },
    async selectDoctor(doctorId) {
      this.terminiData.stafi = doctorId;
    },
    async fetchStafi() {
      try {
        const response = await getStaffByLemi(this.terminiData.reparti);
        console.log(response.data);
        this.doctors = response.data;
      } catch (err) {
        console.log(err)
      }
    },
    async fetchTerminet() {
      try {
        if (this.selectedDate && this.terminiData.reparti && this.terminiData.stafi) {
          const response = await getTerminetByDate(this.selectedDate)
          this.terminet = response.data
          this.terminet = this.terminet.filter(termini => termini.stafiId === this.terminiData.stafi && termini.status === false)
        }
      } catch (err) {
        console.log(err)
      }
    },
    closeModal() {
      this.showModal = false
    },
    showDetails(termini) {
      this.showModal = true
      this.modalData = termini
    },
    async rezervoTerminin() {
      try {
        const user = getLocalStorage()
        const pacientiResponse = await getPacienti(user.user.uid)

        if (user.user.email === this.email) {
          await rezervoTerminin(this.modalData.id, pacientiResponse.data.id)
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
    nextStep() {
      if (this.step < 3) {
        this.step++
      }
    },
    previousStep() {
      if (this.step > 1) {
        this.step--
      }
    }
  }
}
</script>


<style scoped>
.form-control {
  font-size: 19px;
}

.blueheader {
  margin: 20px 0px;
  color: rgb(59, 59, 227);
  font-weight: 200;
}

.reparti-container {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-top: 10px;

}

.center {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.reparti-card {
  width: 200px;
  height: 150px;
  border-radius: 6px;
  cursor: pointer;
  display: flex;
  align-items: flex-end;
  padding: 10px;
  transition: background-color 0.3s;
  background-size: cover;
  background-position: center;
  background-image: url('../../assets/profile.jpg');
}

.doktori-card {
  width: 200px;
  height: 200px;
  border-radius: 6px;
  cursor: pointer;
  display: flex;
  align-items: flex-end;
  padding: 10px;
  transition: background-color 0.3s;
  background-size: cover;
  background-position: center;

}

.doktori-card p {
  font-weight: bold;
  color: rgb(0, 0, 0);
  margin: 0;
}

.doktori-card.selected {
  border: 3px solid rgb(59, 59, 227);
}

.doktori-card.selected p {
  color: rgb(59, 59, 227);
}

.reparti-card p {
  font-weight: bold;
  color: rgb(0, 0, 0);
  margin: 0;
}

.reparti-card.selected {
  border: 3px solid rgb(59, 59, 227);
}

.reparti-card.selected p {
  color: rgb(59, 59, 227);
}


.btn {
  margin-top: 20px;
  margin-right: 20px;
}

.terminet {
  width: 70%;
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

.terminet-warning {
  text-align: center;
  font-family: var(--primary-font);
}

.terminet-warning p {
  color: red;
  font-weight: 500;
}

.termini-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  margin: 0 auto 10px auto;
}

.termini-text-container {
  width: 200px;
  text-align: center;
  margin: 0 auto 10px auto;
}

.termini-container p {
  padding: 10px 20px;
  border: 3px solid var(--primary-color);
  border-radius: 6px;
  transition: border-color 0.1s ease-in;
  color: black;
  font-weight: 500;
  margin: 0px;
}

.termini-container p:hover {
  border: 3px solid var(--button-background);
  transition: border-color 0.1s ease-in;
  cursor: pointer;
}

.confirm-text {
  font-weight: 500;
  font-family: var(--primary-font);
  margin: 5px;
}

.email-input-container {
  margin-top: 10px;
}

.save-button {
  background: #5cb85c;
  width: fit-content;
  padding: 10px 20px;
  border-radius: 10px;
  color: white;
  transition: background-color 0.3s;
  font-size: 1rem;
  font-weight: 500;
}

.save-button:hover {
  background: rgba(44, 101, 216, 0.9);
  transition: background-color 0.3s;
  cursor: pointer;
}

.wrong-email-text {
  font-weight: 500;
}
</style>
