import { roles } from './constants';

const routes = [
    {
      path: '/',
      name: 'Dashboard',
      icon: 'chart-pie',
      authRequired: false,
      role: [roles.MJEK, roles.LABORANT, roles.DREJTOR]
    },
    {
      path: '/profile',
      name: 'Profile',
      icon: 'user',
      authRequired: false,
      role: [roles.MJEK, roles.LABORANT, roles.DREJTOR]
    },
    {
      path: '/shifts',
      name: 'Night Shifts',
      icon: 'calendar',
      authRequired: false,
      role: [roles.MJEK]
    },
    {
      path: '/terminet',
      name: 'Terminet',
      icon: 'clock',
      authRequired: false,
      role: [roles.MJEK]
    },
    {
      path: '/terapite',
      name: 'Terapite',
      icon: 'clipboard',
      authRequired: false,
      role: [roles.MJEK]
    },
    {
      path: '/bisedat',
      name: 'Bisedat',
      icon: 'comment-dots',
      authRequired: false,
      role: [roles.MJEK]
    },
    // {
    //   path: '/tables',
    //   name: 'Tables',
    //   icon: 'table',
    //   authRequired: false,
    //   role: [roles.MJEK]
    // },
    // {
    //   path: '/maps',
    //   name: 'Maps',
    //   icon: 'map',
    //   authRequired: false,
    //   role: [roles.MJEK]
    // },
    // {
    //   path: '/404',
    //   name: '404',
    //   icon: 'exclamation',
    //   authRequired: false,
    //   role: [roles.MJEK, roles.LABORANT]
    // },
  ]
  
  export default routes
  