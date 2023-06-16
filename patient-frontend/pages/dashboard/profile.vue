<template>
    <div class="dashboard">
        <div class="profile-container">
            <div class="carta">
                <button class="button-35" @click="openForm" role="button">Ndrysho Profilin</button>
                <div v-if="isFormOpen" class="form-overlay">
                    <div class="form-container">
                        <div style="display: flex; justify-content: flex-end;">
                            <img id="svgButton" src="../../assets/closebutton.svg" alt="SVG Button" @click="closeForm"
                                style="color:var(--blue); width: 30px;">

                        </div>
                        <div>
                            <form>
                                <div class="mb-3">
                                    <label for="username" class="form-label">Username:</label>
                                    <input type="text" id="username" name="username" class="form-control">
                                </div>
                                <div class="mb-3">
                                    <label for="password" class="form-label">Password:</label>
                                    <input type="password" id="password" name="password" class="form-control">
                                </div>
                                <div class="mb-3">
                                    <label for="surname" class="form-label">Surname:</label>
                                    <input type="text" id="surname" name="surname" class="form-control">
                                </div>
                                <div class="mb-3">
                                    <label for="email" class="form-label">Email:</label>
                                    <input type="email" id="email" name="email" class="form-control">
                                </div>
                                <div class="mb-3">
                                    <label for="city" class="form-label">City:</label>
                                    <input type="text" id="city" name="city" class="form-control">
                                </div>
                                <!-- Add more form fields as needed -->
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <ProfileCard />
        </div>

    </div>
</template>
  

<script>
import ProfileCard from "~/components/ProfileCard";
import DropdownButton from '~/components/DropdownButton.vue';
export default {
    components: {
        ProfileCard,
        DropdownButton
    },
    data() {
        return {

            isFormOpen: false
        };
    },
    methods: {
        handleItemClick(item) {
            console.log('Clicked:', item);
        },
        openForm() {
            this.isFormOpen = true;
        },
        closeForm() {
            this.isFormOpen = false;
        }
    }
}
</script>

<style scoped>
/* Style specific form elements */

input {
    width: 94% !important;
}

.dashboard {
    height: 100vh;
}

.form-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 999;
}

.form-container {
    background-color: #fff;
    padding: 20px;
    border-radius: 5px;
    width: 35%;

}

.close-button {
    position: absolute;
    top: 40%;
    right: 40%;
    background: none;
    border: none;
    color: #fff;
    font-size: 18px;
    cursor: pointer;
}

.carta {
    display: flex;
    flex-direction: column;
    justify-content: flex-end;
    margin-top: 90px;
}



/* CSS */
.button-35 {
    margin-left: 18%;
    align-items: center;
    background-color: #fff;
    border-radius: 12px;
    box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
    width: 175px;
    margin-bottom: 9px;
    box-sizing: border-box;
    color: #121212;
    cursor: pointer;
    display: inline-flex;
    flex: 1 1 auto;
    font-family: Inter, sans-serif;
    font-size: 1.0rem;
    font-weight: 700;
    justify-content: center;
    line-height: 1;

    outline: none;
    padding: 0.8rem 1rem;
    text-align: center;
    text-decoration: none;
    transition: box-shadow .2s, -webkit-box-shadow .2s;
    white-space: nowrap;
    border: 0;
    user-select: none;
    -webkit-user-select: none;
    touch-action: manipulation;
}

.button-35:hover {
    box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
}
</style>