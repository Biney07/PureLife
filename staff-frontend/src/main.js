import '@fortawesome/fontawesome-free/css/all.min.css'
import 'bootstrap-css-only/css/bootstrap.min.css'
import 'mdbvue/lib/css/mdb.min.css'
import Vue from 'vue'
import App from './App'
import router from './router'
import * as VueGoogleMaps from 'vue2-google-maps'
import store from './store';
import Vuex from 'vuex'
import VCalendar from 'v-calendar';

Vue.use(Vuex)

Vue.use(VueGoogleMaps, {
  load: {
    libraries: 'places'
  }
})

Vue.use(VCalendar);

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
