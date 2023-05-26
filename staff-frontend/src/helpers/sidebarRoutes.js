const routes = [
    {
      path: '/',
      name: 'Dashboard',
      icon: 'chart-pie',
      authRequired: false,
    },
    {
      path: '/profile',
      name: 'Profile',
      icon: 'user',
      authRequired: false,
    },
    {
      path: '/shifts',
      name: 'Night Shifts',
      icon: 'clock',
      authRequired: false,
    },
    {
      path: '/tables',
      name: 'Tables',
      icon: 'table',
      authRequired: false,
    },
    {
      path: '/maps',
      name: 'Maps',
      icon: 'map',
      authRequired: false,
    },
    {
      path: '/404',
      name: '404',
      icon: 'exclamation',
      authRequired: false,
    },
  ]
  
  export default routes
  