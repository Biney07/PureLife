<template>
<div>
  <div class="wave-form-background-img">
    <div class="overlay-text">
      <h1 class="about-Title">STAFI YNE</h1>
      <p class="about-paragraf">
        "Spitali PureLife: Kujdes për Shëndetin, Përkrahje për Jetën"
      </p>
    </div>
  </div>
  <div class="stafi" v-if="stafiList">
    <ul class="style-list">
     
      <li class="list-unstyled" v-for="stafi in stafiList" :key="stafi.id">
        <div :style="'background-image: url(' + stafi.pictureUrl + '); background-repeat: no-repeat;'" class="card card0">
          <div class="border">
            <h2>{{ stafi.emri }} {{ stafi.mbiemri }}</h2>
          </div>
        </div>
      </li>
    </ul>
  </div>
</div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      stafiList: [],
    };
  },
  async mounted() {
    await this.fetchStafiList();
  },
  methods: {
    async fetchStafiList() {
      try {
        const response = await axios.get('https://localhost:7292/api/StafiAPI');
        this.stafiList = response.data;
        console.log('Fetched stafi list:', this.stafiList);
      } catch (error) {
        console.log('Error while fetching stafi list:', error);
      }
    },
  },
};
</script>

<style scoped>
.stafi {
  margin: 8% auto;
  width: 100%;
}

.container {
  height: 100vh;
  width: 100vw;
  max-height: 800px;
  max-width: 1280px;
  min-height: 600px;
  min-width: 1000px;
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin: 0 auto;
}

.border {
  height: 369px;
  width: 290px;
  background: transparent;
  border-radius: 10px;
  transition: border 1s;
  position: relative;
}

.border:hover {
  border: 1px solid #fff;
}

.card {
  height: 379px;
  width: 300px;
  background: #808080;
  border-radius: 10px;
  transition: background 0.8s;
  overflow: hidden;
  background: #3d3c3c;
  box-shadow: 0 70px 63px -60px #000;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
}

.card0 {
  background-size: 300px;
}

.card0:hover {
  background-size: 600px;
}

.card0:hover h2 {
  opacity: 1;
}

.card0:hover .fa {
  opacity: 1;
}

h2 {
  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
  color: #fff;
  margin: 20px;
  opacity: 0;
  transition: opacity 1s;
}

.fa {
  opacity: 0;
  transition: opacity 1s;
}

.icons {
  position: absolute;
  fill: #fff;
  color: #fff;
  height: 130px;
  top: 226px;
  width: 50px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-around;
}

.style-list{
  gap: 20px;
  display: flex;
  margin: auto;
  flex-wrap: wrap;
  width: fit-content;
}
</style>
