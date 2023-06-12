import Vue from 'vue'
import Vuex from 'vuex'
import authIndex from './modules/auth/authIndex'
import terminetIndex from './modules/terminet/terminetIndex'


Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        authenticate: authIndex,
        terminet: terminetIndex
    }
})