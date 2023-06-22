<template>
    <div v-if="showAlert" class="sweet-alert" :class="alertType">
        <p class="fade-in-out">{{ message }}</p>
      </div>
</template>
  
<script>
export default {
    props: {
        message: {
            type: String,
            required: true
        },
        type: {
            type: String,
            default: 'info',
            validator: (value) => ['info', 'success', 'warning', 'error'].includes(value)
        },
        duration: {
            type: Number,
            default: 3000
        }
    },
    data() {
        return {
            showAlert: false,
            alertType: ''
        };
    },
    mounted() {
        this.showAlert = true;
        this.alertType = `alert-${this.type}`;
        setTimeout(() => {
            this.showAlert = false;
        }, this.duration);
    }
};
</script>
  
<style scoped>
.fade-in-out {
    animation: fade-in 0.5s ease-out, fade-out 0.5s 2.5s ease-out forwards;
}

@keyframes fade-in {
    0% {
        opacity: 0;
        transform: translateY(-20px);
    }

    100% {
        opacity: 1;
        transform: translateY(0);
    }
}

@keyframes fade-out {
    0% {
        opacity: 1;
    }

    100% {
        opacity: 0;
    }
}

p {
    font-family: var(--primary-font);
    font-weight: 600;
    margin: 0px !important;
}

.sweet-alert {
    position: absolute;
    top: 40px;
    right: -2px;
    padding: 10px 25px;
    text-align: justify;
    z-index: 9999;

    border-radius: 10px 0px 0px 10px;
    margin: 0px;
    /* Adjust the width as per your requirement */

    /* Adjust the width as per your requirement */
}


.alert-info {
    background-color: #b3d7ff;
    color: #333;
    border: 2px solid #6699cc;
    margin: 0px !important;
}

.alert-success {
    background-color: #b3ffcc;
    color: #333;
    border: 2px solid #66cc99;
    margin: 0px !important;
}

.alert-warning {
    background-color: #fff3b3;
    color: #333;
    border: 2px solid #e6c800;
    margin: 0px !important;
}

.alert-error {
    background-color: #ffb3b3;
    color: #333;
    border: 2px solid #cc6666;
    margin: 0px !important;
}
</style>
  