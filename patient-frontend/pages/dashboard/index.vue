<template>
    <div class="dashboard">

    </div>
</template>
  

<script>

export default {
    setup() {
        definePageMeta({
            middleware: [
                'auth',
            ]
        });
    },
}
</script>

<style scoped>
.dashboard {
    height: 100vh;
}
</style>