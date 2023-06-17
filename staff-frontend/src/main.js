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

import VueGoodTablePlugin from 'vue-good-table';

// import the styles
import 'vue-good-table/dist/vue-good-table.css'
import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import vSelect from 'vue-select'
import 'vue-select/dist/vue-select.css';


Vue.component('v-select', vSelect)
Vue.use(BootstrapVue)
Vue.use(BootstrapVueIcons)

Vue.use(VueGoodTablePlugin);

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
