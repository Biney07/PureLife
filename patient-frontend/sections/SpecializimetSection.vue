<template>
  <section class="specializimet">
    <p class="title-text">Specializimet Primare</p>
    <div class="specializimet-row">
        <div class="specializimi-card" v-for="specializimi in specializimet" :key="specializimi.title">
            <img :src="specializimi.img" :alt="specializimi.title" />
            <p>{{specializimi.title}}</p>
        </div>
    </div>

  </section>
</template>

<script>
export default {
    data() {
        return {
            specializimet: [
                {
                    img: "assets/trajtimi-primar.svg",
                    title: "Trajtimi Primar"
                },
                {
                    img: "assets/dentist.svg",
                    title: "Dentist"
                },
                {
                    img: "assets/ob-gyn.svg",
                    title: "Trajtimi Primar"
                },
                {
                    img: "assets/dermatolog.svg",
                    title: "Dermatologjist"
                },
                {
                    img: "assets/psikiater.svg",
                    title: "Psikiater"
                },
                {
                    img: "assets/syte.svg",
                    title: "Optamologji"
                },
            ]
        }
    }
}
</script>

<style scoped>
.specializimet {
    width: 100%;
    min-height: 450px;
    background: white;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

.specializimet-row {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 2%;
    padding: 2% 0px;
}

.specializimi-card{
    background: var(--primary-color);
    padding: 20px 35px;
    text-align: center;
    border-radius: 4px;
    border: 1px solid #CED5DD;
    flex-wrap: wrap;
}

.specializimi-card p{
    margin-top: 10px;
    font-weight: 500;
}

.title-text {
    font-size: 36px;
    color: var(--primary-font-color);
    text-align: center;
}
</style>