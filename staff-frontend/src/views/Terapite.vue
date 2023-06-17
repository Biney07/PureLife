<!-- eslint-disable vue/valid-v-bind -->
<template>
    <div class="terminet-table">
        <div class="terminet-header">
            <div class="starter-text">
                <p class="starter-text-title">Terapite e Pershkruara</p>
            </div>
        </div>
        <vue-good-table
            :columns="columns"
            :rows="allTerapite"
            :search-options="{
                enabled: true,
                skipDiacritics: true,
                placeholder: 'Kerko terapite',
            }"
            :pagination-options="{
                enabled: true,
                mode: 'records',
                perPage: 10,
                perPageDropdown: [5, 10],
            }"
        >
            <template v-slot:table-row="props">
                <span v-if="props.column.field == 'moreOptions'">
                    <mdb-dropdown end tag="li" class="nav-item">
                        <mdb-dropdown-toggle right tag="a" navLink color="secondary-color-dark" slot="toggle" waves-fixed>
                            <template #button-content>
                                <mdb-icon icon="ellipsis-h" class="mr-3" />
                            </template>
                        </mdb-dropdown-toggle>
                        <mdb-dropdown-menu>
                            <mdb-dropdown-item @click="triggerDeleteModal(props.row)"><mdb-icon icon="trash" class="mr-3" />Delete</mdb-dropdown-item>
                            <mdb-dropdown-item @click="triggerEditModal(props.row)"><mdb-icon icon="pen" class="mr-3" />Ndrysho Terapine</mdb-dropdown-item>
                        </mdb-dropdown-menu>
                    </mdb-dropdown>
                </span>
            </template>
        </vue-good-table>


    <mdb-modal v-if="showDeleteModal && modalData" @close="showDeleteModal = false">
      <mdb-modal-header>
        <mdb-modal-title>Fshiej Terapine: {{modalData.id}}, {{modalData.pacienti}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <p>A jeni te sigurte qe deshironi te fshini kete terapi?</p>
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
            @click="deleteTerapine"
        >
            Anulo
        </p>
      </mdb-modal-footer>
    </mdb-modal>

    <mdb-modal v-if="showEditModal && modalData" @close="showEditModal = false">
      <mdb-modal-header>
        <mdb-modal-title>Modifiko Terapine: {{modalData.id}}, {{modalData.pacienti}}</mdb-modal-title>
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
            @click="editTerapine"
        >
            Save
        </p>
      </mdb-modal-footer>
    </mdb-modal>
  </div>
</template>

<script>
import {mapGetters} from "vuex"
import { mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbDropdown, mdbDropdownItem, mdbDropdownMenu, mdbDropdownToggle, mdbIcon } from 'mdbvue'
import { getAllServices } from "../staff-sdk/sherbimet"
import { getAnalizat } from "../staff-sdk/analizat"
import { getTerapiaByStaff, editTerapine, deleteTerapia } from "../staff-sdk/terapia"
export default {
  name: 'my-component',
  components: {
    mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbDropdown, mdbDropdownItem, mdbDropdownMenu, mdbDropdownToggle, mdbIcon
  },
  data(){
    return {
        columns: [
            {
                label: 'Pacienti',
                field: 'pacienti',
            },
            {
                label: 'Koha',
                field: 'koha',
            },
            {
                label: 'Pershkrimi',
                field: 'pershkrimi',
            },
            {
                label: '',
                field: 'moreOptions',
                width: '20px',
            },
        ],
        showDeleteModal: false,
        modalData: null,
        showEditModal: false,
        sherbimet: [],
        analizat: [],
        terapia: {
            pershkrimi: null,
            diagnoza: null, 
            barnat: null,
            sherbimetEKryera: null,
        },
        terapite: null
    }
  },
  mounted() {
    this.fetchTerapite();
  },
  watch: {
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
    allTerapite() {
        return this.terapite
    },
    sherbimetOptions() {
        return this.sherbimet
    },
    analizatOptions() {
        return this.analizat
    }
  },
  methods: {
    async fetchTerapite() {
        try {
            const response = await getTerapiaByStaff(this.user.user.data.id)
            this.terapite = response.data
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        }
    },
    triggerDeleteModal(terapi) {
        this.showDeleteModal = true;
        this.modalData = terapi;
        this.terapia = this.modalData

    },
    triggerEditModal(terapi) {
        this.showEditModal = true;
        this.modalData = terapi;
        // eslint-disable-next-line no-console
        console.log('modalData', this.modalData)
        this.terapia.pershkrimi = this.modalData.pershkrimi
        this.terapia.diagnoza = this.modalData.diagnoza
        this.terapia.barnat = this.modalData.barnat
        // eslint-disable-next-line no-console
        console.log('modalData2', this.terapia)
    },
    async fetchSherbimetDheAnalizat() {
        try {
            const sherbimetResponse = await getAllServices()
            this.sherbimet = sherbimetResponse.data
            const analizatResponse = await getAnalizat()
            this.analizat = analizatResponse.data
            // eslint-disable-next-line no-console
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        }
    },
    async editTerapine() {
        let sherbimetString = this.terapia.sherbimetEKryera.join(", ");
        this.terapia.sherbimetEKryera = sherbimetString
        try {
            await editTerapine(this.modalData.id, this.user.user.data.id, this.terapia)
            await this.fetchTerapite()
            this.reset()
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        }
    },
    async deleteTerapine() {
        try {
            await deleteTerapia(this.modalData.id)
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        } finally {
            await this.fetchTerapite()
            this.showDeleteModal = false
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

</style>