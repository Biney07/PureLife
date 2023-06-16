<template>
    <div :class="colorClass">
        <span style="color:rgb(34, 34, 34)"> Membership Status: {{ title }}</span>
    </div>
</template>
  
<script>
export default {
    props: {
        Membership: {
            type: Boolean,
            required: true

        }
    },
    computed: {
        colorClass() {
            if (this.Membership == false) {
                return "normal";
            } else if (this.Membership == true) {
                return "premium";
            }
        },
        title() {
            if (this.Membership == false) {
                return "Normal";
            } else if (this.Membership == true) {
                return "Premium";
            }
        }
    }
};
</script>
  
<style scoped>
.premium {
    position: absolute;
    top: 20px;
    right: 20px;
    background-color: rgb(254, 220, 23);
    padding: 10px;
    border-radius: 5px;
}

.normal {
    background-color: #9b9b9b;
    position: absolute;
    top: 20px;
    right: 20px;

    padding: 10px;
    border-radius: 5px;

}
</style>
  