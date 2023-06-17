<!-- eslint-disable vue/valid-v-bind -->
<template>
    <div class="terminet-table">
        <div class="terminet-header">
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
                   {{props.row.pacienti ? props.row.pacienti : "I parezervuar" }}
                </span>
                <span v-if="props.column.field == 'status'">
                   {{!props.row.status ? "I lire" : "I rezervuar"}}
                </span>

                <span v-if="props.column.field == 'startTime'">
                   {{props.row.startTime.split(' ')[1].slice(0, -3) + ' - ' + props.row.endTime.split(' ')[1].slice(0, -3) + ' ' + props.row.endTime.split(' ')[2]}}
                </span>

                <span @click="triggerModal(props.row)" v-if="props.column.field == 'moreOptions'">
                   <b-icon icon="trash" variant="danger" class="delete-icon"></b-icon>
                </span>
            </template>
        </vue-good-table>


    <mdb-modal v-if="showModal && modalData" @close="showModal = false">
      <mdb-modal-header>
        <mdb-modal-title>Anulo Terminin: {{modalData.pacienti}}, {{modalData.startTime.split(' ')[1].slice(0, -3) + ' - ' + modalData.endTime.split(' ')[1].slice(0, -3) + ' ' + modalData.endTime.split(' ')[2]}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <p>A jeni te sigurte qe deshironi te anuloni kete termin?</p>
      </mdb-modal-body>
      <mdb-modal-footer>
        <p
            type="button"
            class="close-button"
            @click="showModal = false;"
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
  </div>
</template>

<script>
import {mapGetters} from "vuex"
import { mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter } from 'mdbvue'
import {deleteTermini, createTerminet} from "../staff-sdk/terminet"
export default {
  name: 'my-component',
  components: {
    mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter
  },
  data(){
    return {
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
                width: '100px',
            },
        ],
        date: null,
        selectedDate: null,
        showModal: false,
        modalData: null,
        currentDate: null,
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
    }
  },
  computed: {
    ...mapGetters({
      user: 'getUser'
    }),
    terminet() {
        return this.$store.state.terminet.terminet.data
    }
  },
  methods: {
    async fetchTerminet() {
        await this.$store.dispatch('fetchTerminetByDateAndStaff', {date: this.selectedDate, id: this.user.user.data.id})
    },
    triggerModal(termin) {
        this.showModal = true;
        this.modalData = termin;

    },
    async deleteTermin() {
        try {
            await deleteTermini(this.modalData.id)
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        } finally {
            this.fetchTerminet()
            this.showModal = false
        }
    },
    async createNewTerminet() {
        try {
          await createTerminet(this.user.user.data.id)
        } catch (err) {
          // eslint-disable-next-line no-console
          console.log(err)
        } finally {
            await this.fetchTerminet()
        }
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

</style>