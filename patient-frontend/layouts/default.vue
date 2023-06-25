<template>
    <div>
        <Header v-if="!isDashboardPath" />
        <div>
            <slot />
        </div>
        <Footer v-if="!isDashboardPath" />
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
        isDashboardPath() {
            return this.$route.path.includes("/login") || this.$route.path.includes("/register");
        },
    },
    

}
</script>

<style></style>