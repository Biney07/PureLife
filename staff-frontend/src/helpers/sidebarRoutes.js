import { roles } from './constants';

const routes = [
    {
      path: '/',
      name: 'Dashboard',
      icon: 'chart-pie',
      authRequired: false,
      role: [roles.MJEK, roles.LABORANT]
    },
    {
      path: '/profile',
      name: 'Profile',
      icon: 'user',
      authRequired: false,
      role: [roles.MJEK, roles.LABORANT]
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
      path: '/tables',
      name: 'Tables',
      icon: 'table',
      authRequired: false,
      role: [roles.MJEK]
    },
    {
      path: '/maps',
      name: 'Maps',
      icon: 'map',
      authRequired: false,
      role: [roles.MJEK]
    },
    {
      path: '/404',
      name: '404',
      icon: 'exclamation',
      authRequired: false,
      role: [roles.MJEK, roles.LABORANT]
    },
  ]
  
  export default routes
  