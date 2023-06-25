<template>
<div>
  <div v-show="loading" class="loading-spinner">
    <b-spinner width="200px" variant="primary" label="Spinning"></b-spinner>
  </div>

  <section v-if="!loading" id="dashboard">
    <mdb-card class="mb-4">
      <mdb-card-body class="d-sm-flex justify-content-between">
        <h4 class="mb-sm-0 pt-2">
          <span class="text-primary">Home Page</span><span>/</span><span>Dashboard</span>
        </h4>
      </mdb-card-body>
    </mdb-card>
    <section v-if="data && [userRoles.MJETK, userRoles.LABORANT].includes($store.state.authenticate.user.data.roletId)" class="mt-lg-5">
      <mdb-row>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="money-bill-alt" far class="primary-color"/>
              <div class="data">
                <p>TOTALI I PACIENTEVE</p>
                <h4>
                  <strong>{{data.numriTotalIPacienteve}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="chart-line" class="warning-color"/>
              <div class="data">
                <p>TERMINET E PERFUNDUARA</p>
                <h4>
                  <strong>{{data.totaliTermineveEPerfunduara}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="chart-pie" class="light-blue lighten-1"/>
              <div class="data">
                <p>TERMINET E REZERVUARA</p>
                <h4>
                  <strong>{{data.numriTermineveTeRezervuara}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="chart-bar" class="red accent-2"/>
              <div class="data">
                <p>TERAPITE E KRIJUARA</p>
                <h4>
                  <strong>{{data.totaliTerapiveTePerfunduara}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
      </mdb-row>
    </section>

    <section v-else class="mt-lg-5">
      <mdb-row>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="money-bill-alt" far class="primary-color"/>
              <div class="data">
                <p>TOTALI I PACIENTEVE</p>
                <h4>
                  <strong>{{data.numriTotalIPacienteve}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="chart-line" class="warning-color"/>
              <div class="data">
                <p>FITIMI TOTAL</p>
                <h4>
                  <strong>{{data.fitimiTotal}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="chart-pie" class="light-blue lighten-1"/>
              <div class="data">
                <p>NUMRI I MJEKVE</p>
                <h4>
                  <strong>{{data.numriIMjekeve}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
        <mdb-col xl="3" md="6" class="mb-r">
          <mdb-card cascade class="cascading-admin-card">
            <div class="admin-up">
              <mdb-icon icon="chart-bar" class="red accent-2"/>
              <div class="data">
                <p>TERAPITE E PERFUNDUARA</p>
                <h4>
                  <strong>{{data.numriTermineveTePerfunduara}}</strong>
                </h4>
              </div>
            </div>
          </mdb-card>
        </mdb-col>
      </mdb-row>
    </section>

    <section>
      <mdb-row class="mt-5">
          <mdb-col md="12" class="mb-4">
              <mdb-card>
                  <mdb-card-body>
                      <div style="display: block">
                        <mdb-bar-chart :data="[userRoles.MJETK, userRoles.LABORANT].includes($store.state.authenticate.user.data.roletId) ? barChartData : barChartData2" :options="barChartOptions" :height="500"/>
                      </div>
                  </mdb-card-body>
              </mdb-card>
          </mdb-col>
      </mdb-row>
    </section>
  </section>
</div>
</template>

<script>
import { mdbRow, mdbCol, mdbCard, mdbCardBody, mdbIcon, mdbBarChart } from 'mdbvue'
import { getStatistics, getMonthlyTerminet, getStatisticsDrejtor, getMonthlyIncome } from "../staff-sdk/statistikat"
import { roles } from '../helpers/constants';
export default {
  name: 'Dashboard',
  components: {
    mdbRow,
    mdbCol,
    mdbCard,
    mdbCardBody,
    mdbIcon,
    mdbBarChart,
  },
  data () {
    return {
      userRoles: roles,
      loading: true,
      data: null,
      currentUser: this.$store.state.authenticate.user.data.id,
      barChartData: {
        labels: ['Janar', 'Shkurt', 'Mars', 'Prill', 'Maj', 'Qershor', 'Korrik', 'Gusht', 'Shtator', 'Tetor', 'Nentor', 'Dhjetor'],
        datasets: [
          {
            label: 'Numri I Termineve te Perfunduara',
            data: [],
            backgroundColor: 'rgba(245, 74, 85, 0.5)',
            borderWidth: 1
          }
        ]
      },
      barChartData2: {
        labels: ['Janar', 'Shkurt', 'Mars', 'Prill', 'Maj', 'Qershor', 'Korrik', 'Gusht', 'Shtator', 'Tetor', 'Nentor', 'Dhjetor'],
        datasets: [
          {
            label: 'Fitimet Per Muaj',
            data: [],
            backgroundColor: 'rgba(245, 74, 85, 0.5)',
            borderWidth: 1
          }
        ]
      },
      barChartOptions: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          yAxes: [{
            gridLines: {
              display: true,
              color: 'rgba(0, 0, 0, 0.1)'
            },
          }]
        }
      },
    }
  },
  async mounted() {
    await this.fetchStatistikat()
  },
  methods: {
    async fetchStatistikat() {
      this.loading = true
      try {
        if([this.userRoles.MJETK, this.userRoles.LABORANT].includes(this.$store.state.authenticate.user.data.roletId)) {
          const response = await getStatistics(this.currentUser)
          this.data = response.data
  
          const monthlyDataResponse = await getMonthlyTerminet(this.currentUser)
          for(let i =0;i<monthlyDataResponse.data.length;i++) {
            this.barChartData.datasets[0].data.push(monthlyDataResponse.data[i].numriTermineveTePerfunduara)
          }
        } else {
          const response = await getStatisticsDrejtor()
          this.data = response.data
  
          const monthlyDataResponse = await getMonthlyIncome()
          for(let i =0;i<monthlyDataResponse.data.length;i++) {
            this.barChartData2.datasets[0].data.push(monthlyDataResponse.data[i].fitimi)
          }
        }
        
      } catch (err) {
        // eslint-disable-next-line no-console
        console.log(err)
      } finally {
        this.loading = false
      }
    },
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.cascading-admin-card {
  margin: 20px 0;
}
.cascading-admin-card .admin-up {
  margin-left: 2%;
  margin-right: 4%;
  margin-top: -20px;
}
.cascading-admin-card .admin-up .fas,
.cascading-admin-card .admin-up .far {
  box-shadow: 0 2px 9px 0 rgba(0, 0, 0, 0.2), 0 2px 13px 0 rgba(0, 0, 0, 0.19);
  padding: 1.7rem;
  font-size: 2rem;
  color: #fff;
  text-align: left;
  margin-right: 1rem;
  border-radius: 3px;
}
.cascading-admin-card .admin-up .data {
  float: right;
  margin-top: 2rem;
  text-align: right;
}
.admin-up .data p {
  color: #999999;
  font-size: 12px;
}
.classic-admin-card .card-body {
  color: #fff;
  margin-bottom: 0;
  padding: 0.9rem;
}
.classic-admin-card .card-body p {
  font-size: 13px;
  opacity: 0.7;
  margin-bottom: 0;
}
.classic-admin-card .card-body h4 {
  margin-top: 10px;
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
