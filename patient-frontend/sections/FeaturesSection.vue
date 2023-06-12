<template>
  <section class="features-section">
    <p class="features-title">Cfare mund te beni ne PureLife?</p>
    <div class="features-row">
        <div class="feature-card" v-for="feature in features" :key="feature.title">
            <img class="feature-image" :src="feature.img" :alt="feature.title">
            <p class="feature-title">{{feature.title}}</p>
            <NuxtLink to="/login"><button>Shiko Me shume</button></NuxtLink>
        </div>
    </div>
  </section>
</template>

<script>
export default {
    data() {
        return {
            features: [
                {
                    img: "../assets/membership.svg",
                    title: "Purelife Membership"
                },
                {
                    img: "../assets/vleresimi.svg",
                    title: "Vlereso Sherbimin"
                },
                {
                    img: "../assets/rezervimi.svg",
                    title: "Rezervo Terminin"
                }
            ]
        }
    }
}
</script>

<style scoped>

.features-section {
    width: 100%;
    height: 600px;
    background: var(--third-color);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.features-row {
    width: 100%;
    height: 100%;
    background: var(--third-color);
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 5%;
    padding-top: 3%;
}

.feature-card {
    padding: 25px 40px;
    background: var(--primary-color);
    position: relative;
    width: 300px;
    height: 250px;
}

.feature-image {
  width: 290px;
  height: 290px;
  position: absolute;
  top: -110px;
  left: 50%;
  transform: translateX(-50%);
}

.feature-card p{
    position: absolute;
    bottom: 25%;
    left: 50%;
    transform: translateX(-50%);
    font-weight: 500;
    font-size: 18px;
    width: 100%;
    text-align: center;
    text-transform: uppercase;
}

.feature-card button{
    position: absolute;
    bottom: 15px;
    left: 50%;
    transform: translateX(-50%);
    font-size: 18px;
    text-align: center;
    text-transform: uppercase;
    padding: 12px 40px;
    outline: none;
    background: var(--button-background);
    border-radius: 4px;
    border: 1px solid #2C64D8;
    font-size: 14px;
    width: 60%;
    cursor: pointer;
    color: white;
    text-decoration: none;
    transition: background-color 0.5s;
}

.feature-card button:hover {
    background: rgba(44, 100, 216, 0.8);
    transition: background-color 0.5s;
}

.features-title {
    font-size: 36px;
    color: var(--primary-font-color);
    text-align: center;
    margin-top: 50px;
}
</style>