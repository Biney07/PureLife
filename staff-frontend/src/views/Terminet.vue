<!-- eslint-disable vue/valid-v-bind -->
<template>
    <div class="terminet-table">
        <div class="terminet-header">
            <div class="starter-text">
                <p>Terminet</p>
                <p>{{selectedDate}}</p>
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
                 <span v-if="props.column.field == 'status'">
                   {{!props.row.status ? "I lire" : "I rezervuar"}}
                </span>

                <span v-if="props.column.field == 'startTime'">
                   {{props.row.startTime.split(' ')[1] + ' ' + props.row.startTime.split(' ')[2]}}
                </span>

                <span v-if="props.column.field == 'endTime'">
                   {{props.row.endTime.split(' ')[1] + ' ' + props.row.endTime.split(' ')[2]}}
                </span>
            </template>
        </vue-good-table>
  </div>
</template>

<script>
import {mapGetters} from "vuex"
export default {
  name: 'my-component',
  data(){
    return {
        columns: [
            {
                label: 'Pacienti',
                field: 'pacienti',
            },
            {
                label: 'Fillon',
                field: 'startTime',
            },
            {
                label: 'Mbaron',
                field: 'endTime',
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
        selectedDate: null
    }
  },
  mounted() {
    const currentDate = new Date(); // Get the current date
    const year = currentDate.getFullYear();
    let month = currentDate.getMonth() + 1; // Months are zero-indexed, so add 1
    month = month < 10 ? `0${month}` : month; // Pad the month with leading zero if needed
    let day = currentDate.getDate();
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
        await this.$store.dispatch('fetchTerminetByDate', this.selectedDate)
        // await this.changeDate({currentPage: 1, currentPerPage: 12})
    },
    // async changeDate(params) {
    //     const currentPageRows = await this.terminet.slice(
    //         (params.currentPage - 1) * params.currentPerPage,
    //         params.currentPage * params.currentPerPage
    //     );
    //     const startTimeValues = currentPageRows.map((row) => row.startTime);
    //     this.date = startTimeValues[0].split(" ")[0]
    //     // eslint-disable-next-line no-console
    //     console.log(this.date)
    // },
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

.starter-text p {
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
}

.date-input{
    border: none;
    outline: none;
    background: rgb(241,201,137);
    border-radius: 8px;
    padding: 5px 10px;
}

</style>