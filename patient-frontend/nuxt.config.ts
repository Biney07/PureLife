import { defineNuxtConfig } from '@nuxt/bridge'

export default defineNuxtConfig({
  css: [
    '~/assets/base.css',
    'bootstrap/dist/css/bootstrap.css' // Add Bootstrap CSS file
  ],
  render: {
    csp: false // Disable Content Security Policy to allow inline scripts
  },
  vite: {
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: `@import "@/assets/_custom.scss";` // Add your custom SCSS file if needed
        }
      }
    }
  }
})
