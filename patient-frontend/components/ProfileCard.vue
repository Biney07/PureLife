<template>
    <div class="megacard">

        <div class="card">
            <div class="cover-photo">
                <img src="../assets/profile.jpg" class="profile">
            </div>
            <h3 class="profile-name">Albin Saraqi</h3>
            <div class="pacientinfo">
                <div class="part">
                    <p class="about">MOSHA:
                    <p class="about-bold"> 27 Vjet</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about">GJINIA:
                    <p class="about-bold"> Mashkull</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about"> ID:
                    <p class="about-bold"> 112333213232</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about">VENDLINDJA
                    <p class="about-bold"> Prishtine, Kosove</p>
                    </p>
                </div>
                <div class="part">
                    <p class="about">EMAIL:
                    <p class="about-bold"> saraqialbin@gmail.com</p>
                    </p>
                </div>

            </div>
        </div>
        <div class="rightcard">
            <MemberShipStatus :Membership="false"/>
            <div class="pacientinfo-right">
                <h3 class="profile-nameA">ALERGJI</h3>
                <div class="part-right">

                    <p class="about-bold"> Poleni
                        Food( peanut, milk, patlixhan)
                        Animal( Lopa, ) </p>


                </div>
                <h3 class="profile-name"> STATUSI I SHENDETIT:</h3>
                <div class="part-right">
                    <HealthStatus :healthPercentage="76" />

                </div>
            </div>
        </div>
    </div>
</template>

<script>

import HealthStatus from '~/components/HealthStatus.vue';
import MemberShipStatus from '~/components/MemberShipStatus.vue';
export default {
    components: {
        HealthStatus,
        MemberShipStatus
    },

}
    ;
</script>

<style scoped>
.gold-div {
    position: absolute;
    top: 20px;
    right: 20px;
    background-color: rgb(254, 220, 23);
    padding: 10px;
    border-radius: 5px;
}

.status-text {
    color: rgb(0, 0, 0);
    font-weight: bold;
}

.megacard {
    display: flex;
}

.card {
    font-family: var(--primary-font);
    padding: 25px 30px;
    width: 300px;
    background: #ffffff;
    border-radius: 5px 0px 0px 5px;
    display: flex;



}

.rightcard {
    position: relative;
    font-family: var(--primary-font);
    padding: 25px;
    width: 350px;
    background: #ffffff;
    border-radius: 0px 5px 5px 0px;
    display: flex;
    justify-content: flex-end;


}

.part {
    display: flex;
    align-items: center;

}

.part-right {
    display: flex;
    text-align: right;
    justify-content: flex-end;
}

.pacientinfo {
    display: flex;
    justify-content: left;
    flex-direction: column;
    margin-top: 30px;
}

.pacientinfo-right {
    display: flex;
    justify-content: right;
    flex-direction: column;
    margin-top: 30px;
    text-align: right;
}

.cover-photo {
    position: relative;
    background: url("../assets/purelife.png");
    background-size: cover;
    height: 100px;
    border-radius: 5px 5px 0 0;
}

.about-bold {
    font-weight: 500;
    margin: 0px;
    font-size: 20px;
    padding-left: 10px;
}

.profile {
    position: absolute;
    width: 120px;
    bottom: -60px;
    left: 5px;
    border-radius: 50%;
    border: 2px solid var(--blue);
    background: #222;
    padding: 2px;
}

.profile-name {
    font-size: 27px;
    margin: 10px 0 1px 140px;
    color: var(--blue);
}

.profile-nameA {
    font-size: 27px;
    margin: 40px 0 1px 140px;
    color: var(--blue);
}

.about {
    line-height: 1.6;
    margin: 0px;
}
</style>