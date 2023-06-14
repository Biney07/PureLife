import { defineNuxtConfig } from '@nuxt/bridge'

export default defineNuxtConfig({
  devtools: { enabled: true },
  css: [
    '~/assets/base.css',
    'bootstrap/dist/css/bootstrap.css'
  ],
})
