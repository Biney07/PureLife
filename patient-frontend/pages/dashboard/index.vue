<template>
<div class="dashboard">
        <p>dashboard page</p>
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