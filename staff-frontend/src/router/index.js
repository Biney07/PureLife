import Vue from 'vue'
import VueRouter from 'vue-router'

import * as auth from '../helpers/auth'
import store from '../store'
import Dashboard from '@/components/Dashboard'
import Profile from '@/components/Profile'
import Login from '../views/auth/Login'
import Kujdestarite from '../views/Kujdestarite'
import Terminet from '../views/Terminet'
import Terapite from '../views/Terapite'
import Chat from '../views/Chat'

Vue.use(VueRouter)

const router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Dashboard',
      component: Dashboard,
      props: {page: 1},
      meta: { requiresAuth: true }
    },
    {
      path: '/profile',
      name: 'Profile',
      component: Profile,
      props: {page: 2},
      meta: { requiresAuth: true }
    },
    // {
    //   path: '/tables',
    //   name: 'Tables',
    //   component: Tables,
    //   props: {page: 3},
    //   meta: { requiresAuth: true }
    // },
    // {
    //   path: '/maps',
    //   name: 'Maps',
    //   component: Maps,
    //   props: {page: 4},
    //   meta: { requiresAuth: true }
    // },
    {
      path: '/login',
      name: 'Login',
      component: Login,
      props: {page: 6},
      meta: { requiresAuth: false }
    },
    // {
    //   path: '/404',
    //   name: 'BadGateway',
    //   component: BadGateway,
    //   props: {page: 5},
    //   meta: { requiresAuth: false }
    // },
    // {
    //   path: '*',
    //   redirect: '/404',
    //   props: {page: 5},
    //   meta: { requiresAuth: false }
    // },
    {
      path: '/shifts',
      name: 'Night Shifts',
      component: Kujdestarite,
      props: {page: 7},
      meta: { requiresAuth: true }
    },
    {
      path: '/terminet',
      name: 'Terminet',
      component: Terminet,
      props: {page: 8},
      meta: { requiresAuth: true }
    },
    {
      path: '/terapite',
      name: 'Terapite',
      component: Terapite,
      props: {page: 9},
      meta: { requiresAuth: true }
    },
    {
      path: '/bisedat',
      name: 'Bisedat',
      component: Chat,
      props: {page: 10},
      meta: { requiresAuth: true }
    },
  ]
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(route => route.meta.requiresAuth)) {
      const isAuthenticated = checkAuthentication();
      if (isAuthenticated) {
          store.commit('storeUser', auth.getUser());
          next();
      } else {
          next('/login');
      }
  } else {
    next();
  }
});

function checkAuthentication() {
    if(auth.userExists()) return true
    return false;
}
export default router;