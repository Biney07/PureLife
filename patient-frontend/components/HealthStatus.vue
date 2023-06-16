<template>
    <div :class="colorClass">
        <h2 :class="`dark-${colorClass}`">{{ title }}</h2>
    </div>
</template>
  
<script>
export default {
    props: {
        healthPercentage: {
            type: Number,
            required: true
        }
    },
    computed: {
        colorClass() {
            if (this.healthPercentage < 25) {
                return "red";
            } else if (this.healthPercentage < 50) {
                return "orange";
            } else if (this.healthPercentage < 75) {
                return "yellow";
            } else {
                return "green";
            }
        },
        title() {
            if (this.healthPercentage < 25) {
                return "Gjendje e rëndë";
            } else if (this.healthPercentage < 50) {
                return "Gjendje jo e mirë";
            } else if (this.healthPercentage < 75) {
                return "I Shëndetshëm";
            } else {
                return "Super i shëndetshëm";
            }
        }
    }
};
</script>
  
<style scoped>
.red {
    background-color: #cc7272;
    padding: 10px;
    border-radius: 5px;
    margin-top: 10px;
}

.orange {
    background-color: #c69666;
    padding: 10px;
    border-radius: 5px;
    margin-top: 10px;
}

.yellow {
    background-color: #f5f58a;
    padding: 10px;
    border-radius: 5px;
    margin-top: 10px;
}

.green {
    background-color: #7edd7e;
    padding: 10px;
    border-radius: 5px;
    margin-top: 10px;

}

.dark-red,
.dark-orange,
.dark-yellow,
.dark-green {
    color: #000000;
    font-size: 25px;
}
</style>
  