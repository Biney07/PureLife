import { defineNuxtConfig } from '@nuxt/bridge'
import dotenv from 'dotenv'

// Load environment variables from .env file
dotenv.config()
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
    sockets: [{
      name: 'main',
      url: 'http://localhost:5000'
    }]
  },
  env: {
    STRIPE_PUBLISHABLE_KEY: process.env.STRIPE_PUBLISHABLE_KEY,
    STRIPE_SECRET_KEY: process.env.STRIPE_SECRET_KEY
  },
})
