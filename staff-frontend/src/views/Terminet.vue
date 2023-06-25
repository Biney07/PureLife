<!-- eslint-disable vue/valid-v-bind -->
<template>
<div>
    <div v-if="loading" class="loading-spinner">
        <b-spinner variant="primary" label="Spinning"></b-spinner>
    </div>
    <div v-else class="terminet-table">
        <div v-if="terminet" class="terminet-header">
            <div class="starter-text">
                <p class="starter-text-title">Terminet</p>
                <p v-show="!terminet.length && currentDate.getDate() == selectedDate.split('-').at(-1)" @click="createNewTerminet">Krijo Terminet</p>
            </div>
            <input class="date-input" type="date" v-model="selectedDate">
        </div>

        <vue-good-table
            :columns="columns"
            :rows="terminet"
            :search-options="{
                enabled: true,
                skipDiacritics: true,
                placeholder: 'Kerko terminet',
            }"
        >

            <template v-slot:table-row="props">
                <span v-if="props.column.field == 'pacienti'">
                   {{props.row.pacientiName}} {{props.row.pacientiLastName}}
                </span>
                <span v-if="props.column.field == 'status'">
                   {{props.row.status}}
                </span>

                <span v-if="props.column.field == 'startTime'">
                   {{props.row.startTime.split(' ')[1].slice(0, -3) + ' - ' + props.row.endTime.split(' ')[1].slice(0, -3) + ' ' + props.row.endTime.split(' ')[2]}}
                </span>

                <span v-if="props.column.field == 'moreOptions'">
                    <mdb-dropdown end tag="li" class="nav-item">
                        <mdb-dropdown-toggle right tag="a" navLink color="secondary-color-dark" slot="toggle" waves-fixed>
                            <template #button-content>
                                <mdb-icon icon="ellipsis-h" class="mr-3" />
                            </template>
                        </mdb-dropdown-toggle>
                        <mdb-dropdown-menu>
                            <mdb-dropdown-item @click="triggerDeleteModal(props.row)"><mdb-icon icon="trash" class="mr-3" />Delete</mdb-dropdown-item>
                            <mdb-dropdown-item v-if="props.row.status && !props.row.hasTherapy" @click="triggerEditModal(props.row)"><mdb-icon icon="pen" class="mr-3" />Krijo Terapine</mdb-dropdown-item>
                        </mdb-dropdown-menu>
                    </mdb-dropdown>
                </span>
            </template>
        </vue-good-table>


    <mdb-modal v-if="showDeleteModal && modalData" @close="showDeleteModal = false">
      <mdb-modal-header>
        <mdb-modal-title>Anulo Terminin: {{modalData.pacientiName}} {{modalData.pacientiLastName}}, {{modalData.startTime.split(' ')[1].slice(0, -3) + ' - ' + modalData.endTime.split(' ')[1].slice(0, -3) + ' ' + modalData.endTime.split(' ')[2]}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <p>A jeni te sigurte qe deshironi te anuloni kete termin?</p>
      </mdb-modal-body>
      <mdb-modal-footer>
        <p
            type="button"
            class="close-button"
            @click="showDeleteModal = false;"
        >
            Close
        </p>
        <p
            type="button"
            class="anulo-button"
            @click="deleteTermin"
        >
            Anulo
        </p>
      </mdb-modal-footer>
    </mdb-modal>

    <mdb-modal v-if="showEditModal && modalData" @close="showEditModal = false">
      <mdb-modal-header>
        <mdb-modal-title>Krijo Terapine: {{modalData.pacientiName}} {{modalData.pacientiLastName}}, {{modalData.startTime.split(' ')[1].slice(0, -3) + ' - ' + modalData.endTime.split(' ')[1].slice(0, -3) + ' ' + modalData.endTime.split(' ')[2]}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <form>
            <div class="form-outline">
                <label class="form-label font-weight-normal" for="form1Example1">Pershkrimi:</label>
                <textarea required rows="4" v-model="terapia.pershkrimi" class="form-control" placeholder="Pershkrimi" />
            </div>

            <!-- Password input -->
            <div class="form-outline">
                <label class="form-label font-weight-normal" for="form1Example1">Diagnoza:</label>
                <textarea required rows="4" v-model="terapia.diagnoza" class="form-control" placeholder="Diagnoza" />
            </div>

            <div class="form-outline">
                <label class="form-label font-weight-normal" for="form1Example2">Barnat:</label>
                <input required placeholder="Barnat" v-model="terapia.barnat" class="form-control" />
            </div>

            <div class="form-outline">
                <label class="form-label font-weight-normal" for="form1Example2">Sherbimi:</label>
                <v-select required multiple v-model="terapia.sherbimetEKryera" :options="sherbimetOptions" :reduce="sherbimi => sherbimi.id" label="emri" />
            </div>

            <div class="form-outline">
                <label class="form-label font-weight-normal" for="form1Example2">Analizat e kryera:</label>
                <v-select required multiple v-model="terapia.analizatECaktuara" :options="analizatOptions" :reduce="analiza => analiza.id" label="emri" />
            </div>
        </form>
      </mdb-modal-body>
      <mdb-modal-footer>
        <p
            type="button"
            class="close-button"
            @click="showEditModal = false;"
        >
            Close
        </p>
        <p
            type="button"
            class="anulo-button"
            @click="krijoTerapine"
        >
            Save
        </p>
      </mdb-modal-footer>
    </mdb-modal>
  </div>
</div>
</template>

<script>
import {mapGetters} from "vuex"
import { mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbDropdown, mdbDropdownItem, mdbDropdownMenu, mdbDropdownToggle, mdbIcon } from 'mdbvue'
import {deleteTermini, createTerminet, updateTerminiHasTherapy} from "../staff-sdk/terminet"
import { getAllServices } from "../staff-sdk/sherbimet"
import { getAnalizat } from "../staff-sdk/analizat"
import {createTerapine} from "../staff-sdk/terapia"
export default {
  name: 'my-component',
  components: {
    mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbDropdown, mdbDropdownItem, mdbDropdownMenu, mdbDropdownToggle, mdbIcon
  },
  data(){
    return {
        loading: true,
        columns: [
            {
                label: 'Pacienti',
                field: 'pacienti',
            },
            {
                label: 'Orari',
                field: 'startTime',
                width: '250px',
            },
            {
                label: 'Statusi',
                field: 'status',
            },
            {
                label: '',
                field: 'moreOptions',
                width: '20px',
            },
        ],
        date: null,
        selectedDate: null,
        showDeleteModal: false,
        modalData: null,
        currentDate: null,
        showEditModal: false,
        sherbimet: [],
        analizat: [],
        terapia: {
            pershkrimi: null,
            diagnoza: null, 
            barnat: null,
            terminiId: null,
            sherbimetEKryera: null,
            analizatECaktuara: null,
        }
    }
  },
  mounted() {
    this.currentDate = new Date(); // Get the current date
    const year = this.currentDate.getFullYear();
    let month = this.currentDate.getMonth() + 1; // Months are zero-indexed, so add 1
    month = month < 10 ? `0${month}` : month; // Pad the month with leading zero if needed
    let day = this.currentDate.getDate();
    day = day < 10 ? `0${day}` : day; // Pad the day with leading zero if needed

    this.selectedDate = `${year}-${month}-${day}`;
    this.fetchTerminet();
  },
  watch: {
    selectedDate() {
        this.fetchTerminet(this.selectedDate)
    },
    showEditModal(newValue) {
        if(newValue === true) {
            this.fetchSherbimetDheAnalizat()
        }
    }
  },
  computed: {
    ...mapGetters({
      user: 'getUser'
    }),
    terminet() {
        return this.$store.state.terminet.terminet.data
    },
    sherbimetOptions() {
        return this.sherbimet
    },
    analizatOptions() {
        return this.analizat
    }
  },
  methods: {
    async fetchTerminet() {
        this.loading = true
        await this.$store.dispatch('fetchTerminetByDateAndStaff', {date: this.selectedDate, id: this.user.user.data.id})
        this.loading = false
    },
    triggerDeleteModal(termin) {
        this.showDeleteModal = true;
        this.modalData = termin;

    },
    triggerEditModal(termin) {
        this.showEditModal = true;
        this.modalData = termin;
        this.terapia.terminiId = termin.id
    },
    async deleteTermin() {
        try {
            await deleteTermini(this.modalData.id)
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        } finally {
            this.fetchTerminet()
            this.showDeleteModal = false
        }
    },
    async createNewTerminet() {
        try {
          await createTerminet(this.user.user.data.id)
          await updateTerminiHasTherapy(this.user.user.data.id, true)
        } catch (err) {
          // eslint-disable-next-line no-console
          console.log(err)
        } finally {
            await this.fetchTerminet()
        }
    },
    async fetchSherbimetDheAnalizat() {
        try {
            const sherbimetResponse = await getAllServices(this.$store.state.authenticate.user.data.id)
            this.sherbimet = sherbimetResponse.data
            const analizatResponse = await getAnalizat()
            this.analizat = analizatResponse.data
            // eslint-disable-next-line no-console
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        }
    },

    async krijoTerapine() {
        let sherbimetString = this.terapia.sherbimetEKryera.join(", ");
        let analizatString = this.terapia.analizatECaktuara.join(", ");

        this.terapia.sherbimetEKryera = sherbimetString
        this.terapia.analizatECaktuara = analizatString
        try {
            await createTerapine(this.terapia)
            this.reset()
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        }

    },
    reset() {
        for (var key in this.terapia) {
            if (this.terapia.hasOwnProperty(key)) {
                this.terapia[key] = null;
            }
        }

        this.showEditModal = false
    }
  }
};
</script>

<style scoped>

.terminet-table{
    width: 80%;
    margin: auto;
    overflow: hidden;
}

.terminet-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.starter-text {
    display: flex;
    gap: 10px;
    padding: 10px 0;
    align-items: center;
}

.starter-text .starter-text-title {
    font-size: 28px;
    padding: 0;
    margin: 0;
}

.starter-text p:last-child {
    font-size: 15px;
    font-weight: 500;
    padding: 0;
    margin: 0;
    padding: 5px 10px;
    border-radius: 8px;
    background: #1c80c8;
    color: white;
    cursor: pointer;
}

.date-input{
    border: none;
    outline: none;
    background: rgb(241,201,137);
    border-radius: 8px;
    padding: 5px 10px;
}

.delete-icon {
    width: 100%;
    margin: 0 auto;
    font-size: 25px;
    font-weight: 600;
    cursor: pointer;
}

.close-button{
    background: #f64e60;
    width: fit-content;
    padding: 10px 20px;
    border-radius: 10px;
    color: white;
    transition: background-color 0.3s;
    font-size: 1rem;
    font-weight: 500;
}

.close-button:hover{
    background: #ee2d41;
    transition: background-color 0.3s;
    cursor: pointer;
}

.anulo-button{
    background: rgb(107,98,255, 0.9);;
    width: fit-content;
    padding: 10px 20px;
    border-radius: 10px;
    color: white;
    transition: background-color 0.3s;
    font-size: 1rem;
    font-weight: 500;
}

.anulo-button:hover{
    background: rgb(107,98,255, 1);;
    transition: background-color 0.3s;
    cursor: pointer;
}

input.form-control, textarea.form-control {
  border: none;
  background: #f3f6f9;
  border-radius: 8px;
  min-height: 40px;
  transition: background-color 0.3s;
  padding: auto 15px;
  margin-bottom: 20px;
}

.form-control:focus {
  border: none;
  outline: none;
  background: #ebedf3;
  box-shadow: none;
  transition: background-color 0.3s;
}

.loading-spinner {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 90vh;
}

.loading-spinner span {
  width: 60px;
  height: 60px;
}

</style>