<template>
    <div>
        <Header v-if="showHeader" />
        <div>
            <slot />
        </div>
        <Footer v-if="showHeader" />
    </div>
</template>

<script>
import Header from '../components/Header.vue'
import Footer from '../components/Footer.vue'
export default {
    components: {
        Header,
        Footer
    },
    computed: {
        computed: {
            showHeader() {
                return !['/register', '/login'].includes(this.$nuxt._route.path)
            }
        }
    },
    

}
</script>

<style></style>