<template>
  <section class="application-section">
    <div class="section-content">
        <p class="section-content-title">SHKARKO APLIKACIONIN PURELIFE</p>
        <p class="section-content-subtitle">PURELIFE ESHTE NJE NDER PLATFORMAT ME TE 
        AVANCUARA NE FUSHEN E MJEKSISE</p>
        
        <div class="download-here">
            <small>Shkarko aplikacionin duke skanuar QR CODE</small>
            <img src="@/assets/qrCode.svg" alt="qr-code">
        </div>
        <div class="store-platforms">
            <img src="@/assets/app-store.svg" alt="">
            <img src="@/assets/play-store.svg" alt="">
        </div>
    </div>
    <div class="section-image">
        <img src="@/assets/app.svg" alt="app">
        <img src="@/assets/half-circle.svg" alt="qr-code">
    </div>
  </section>
</template>

<script>
export default {

}
</script>

<style scoped>

.application-section {
  display: flex;
  padding: 60px;
  width: auto;
  height: 600px;
  background: var(--primary-color);
  overflow: hidden;
}

.section-content {
  margin-top: 5%;
  width: 40%;
}

.section-image {
  position: relative;
}


.section-image img:first-child {
  position: absolute;
  top: 15%;
  left: 40% !important; /* Adjust the value as needed */
  z-index: 5;
}

.section-image img:last-child {
  position: absolute;
  top: calc(100% - 50%);
  left: 20%;

}

.section-content-title{
    font-size: 36px;
    font-weight: 500;
}

.section-content-subtitle{
    font-size: 16px;
    font-weight: 500;
    margin-top: 15px;
}

.download-here {
    font-size: 14px;
    margin-top: 20px;
    display: flex;
    flex-direction: column;
}

.download-here img {
    width: 150px;
    margin-top: 15px;
}

.store-platforms{
    display: flex;
    margin-top: 10px;
    gap: 2%;
}


.store-platforms img{
    width: 150px;
}
</style>