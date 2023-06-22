import { defineNuxtConfig } from '@nuxt/bridge'

export default defineNuxtConfig({
  devtools: { enabled: true },
  css: [
    '~/assets/base.css',
    '~/node_modules/bootstrap/dist/css/bootstrap.css',
    '@fortawesome/fontawesome-svg-core/styles.css'
  ],
  script: [
    {
      src: '~/node_modules/bootstrap/dist/js/bootstrap.bundle.min.js',
      body: true
    }
  ],
  modules: [
    'nuxt-socket-io',
  ],
  io: {
    // module options
    sockets: [{
      name: 'main',
      url: 'http://localhost:5000'
    }]
  }
})

