<template>
    <div>
        <Header v-if="showHeader" />

        <div>
            <slot />
        </div>
    </div>
</template>

<script>
import Header from '../components/Header.vue'
export default {
    components: {
        Header
    },
    computed: {
        showHeader() {
            return !['/register', '/login'].includes(this.$nuxt._route.path)
        }
    }
}
</script>

<style>

</style>