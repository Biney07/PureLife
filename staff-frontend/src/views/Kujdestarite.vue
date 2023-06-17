<template>
  <div class="calendar-container">
    <div class="starter-text">
        <p>24h Shifts</p>
        <p>Here you can view yours and your colleagues 24h periodical shifts</p>
    </div>
    <v-calendar
    v-if="attributes"
      class="custom-calendar"
      :masks="masks"
      :attributes="attributes"
      disable-page-swipe
      is-expanded
    >

      <template v-slot:day-content="{ day, attributes }">
        <div class="day-cell" @click="showDetails(day)">
          <span>{{ day.day }}</span>
          <div
            v-for="attr in attributes"
            :key="attr.id"
          >
             <p
              class="staff-name"
            >
              <span>Dr: {{ attr.customData.staffName }}, {{ attr.customData.reparti }}</span>
            </p>
           
          </div>
        </div>
      </template>
    </v-calendar>

    <mdb-modal v-if="showModal && modalData.attributes.length" @close="showModal = false">
      <mdb-modal-header>
        <mdb-modal-title>Data: {{modalData.ariaLabel}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <div
          v-for="attr in modalData.attributes"
          :key="attr.id"
          class="mb-4"
        >
          <p class="reparti-modal-text">
            Reparti: {{ attr.customData.reparti }}, Kati: {{ attr.customData.repartiKati }}
          </p>
          
          <p
            class="staff-name-modal-text"
          >
            Emri: {{ attr.customData.staffName }}
          </p>
          
        </div>
      </mdb-modal-body>
      <mdb-modal-footer>
        <p
            type="button"
            class="close-button"
            @click="showModal = false;"
        >
            Close
        </p>
      </mdb-modal-footer>
    </mdb-modal>


  </div>
</template>

<script>
import { getAllShifts } from "../staff-sdk/kujdestarite"
import { mapGetters } from 'vuex'
import { mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter } from 'mdbvue'
export default {
  components: {
    mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter,
  },
  data() {
    // const month = new Date().getMonth();
    // const year = new Date().getFullYear();
    // const date = new Date().getDate();
    // eslint-disable-next-line no-console
    // console.log(year, month);
    return {
      masks: {
        weekdays: 'WWW',
      },
      attributes: [],
      showModal: false,
      modalData: null,
    };
  },
  mounted() {
    this.fetchAllShifts();
  },
  computed: {
    ...mapGetters({
      user: 'getUser'
    })
  },
  methods: {
    async fetchAllShifts() {
        const response = await getAllShifts();
        const newData = response.data.map(item => {
            const [dateString] = item.data.split('T');
            // eslint-disable-next-line no-unused-vars
            const [year, month, date] = dateString.split('-');
            const reparti = item.reparti;
            const stafi = item.stafiEmriMbiemri;
            const kati = item.kati;
            return {
                customData: {
                    staffId: item.stafiId,
                    reparti: reparti,
                    staffName: stafi,
                    repartiKati: kati
                },
                dates: new Date(parseInt(year), parseInt(month - 1), parseInt(date))
            };
        });
        this.attributes.push(...newData);
        // eslint-disable-next-line no-console
        console.log(this.attributes);
    },
    showDetails(day) {
      this.showModal = true;
      this.modalData = day;
      // eslint-disable-next-line no-console
      console.log(this.modalData);
    },
    closeModal() {
      this.showModal = false;
    },
  }
};
</script>

<style lang="postcss" scoped>
/deep/ .custom-calendar.vc-container {
  --day-border: 1px solid #b8c2cc;
  --day-border-highlight: 1px solid #b8c2cc;
  --day-width: 100%;
  --day-height: 100px;
  --weekday-bg: #f8fafc;
  --weekday-border: 1px solid #eaeaea;

  border-radius: 0;
  width: 100%;

  & .vc-header {
    background-color: #f1f5f8;
    padding: 10px 0;
  }
  & .vc-weeks {
    padding: 0;
  }
  & .vc-weekday {
    background-color: var(--weekday-bg);
    border-bottom: var(--weekday-border);
    border-top: var(--weekday-border);
    padding: 5px 0;
  }
  & .vc-day {
    padding: 0 5px 3px 5px;
    text-align: left;
    height: var(--day-height);
    min-width: var(--day-width);
    background-color: white;
    &.weekday-1,
    &.weekday-7 {
      background-color: #eff8ff;
    }

    overflow: auto;
    &:not(.on-bottom) {
      border-bottom: var(--day-border);
      &.weekday-1 {
        border-bottom: var(--day-border-highlight);
      }
    }
    &:not(.on-right) {
      border-right: var(--day-border);
    }
  }
  & .vc-day-dots {
    margin-bottom: 5px;
  }
}

.reparti-text, .staff-name {
    padding: 4px 8px;
    font-weight: 500;
    color: black;
    background: rgb(107,98,255, 0.9);
    border-radius: 6px;
    font-size: 12px;
}

.staff-name {
    background: rgb(241,201,137);
}

.change-background {
    background: rgb(107,98,255, 0.4);
}

.starter-text p:first-child {
    font-size: 28px;
    padding: 0;
    margin: 0;
}

.calendar-container {
    background-color: #fff;
    padding: 20px 30px;
    border-radius: 6px;
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
}

.reparti-modal-text{
    font-size: 23px;
    padding: 0;
    margin: 0;
    margin-bottom: 5px;
}

.staff-name-modal-text {
    padding: 5px 10px;
    border-radius: 5px;
    background: rgb(241,201,137);
    font-weight: 500;
    font-size: 16px;
}

.day-cell{
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

@media screen and (max-width: 450px) {
  main, div{
    padding: 0 !important;
  }
  .calendar-container {
    width: 100%;
    box-shadow: none;
    background: transparent;
    padding: 0;
  }

  /deep/ .custom-calendar.vc-container {
    --day-border: 1px solid #b8c2cc;
    --day-border-highlight: 1px solid #b8c2cc;
    --day-width: 100%;
    --day-height: 80px;
    --weekday-bg: #f8fafc;
    --weekday-border: 1px solid #eaeaea;

    border-radius: 0;
    width: 100%;
  }

}
</style>