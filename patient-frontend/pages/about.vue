<template>
  <div class="wave-form-background-img">
    <div class="overlay-text">
      <h1 class="about-Title">RRETH NESH</h1>
      <p class="about-paragraf">
        "Spitali PureLife: Kujdes për Shëndetin, Përkrahje për Jetën"
      </p>
    </div>
  </div>
  <img src="../assets/upbanner.svg" class="upbanner" alt="" />
  <div class="wave-form-background">
    <div class="padding-global snipcss-CvWsz">
      <div class="container-large-3">
        <div class="padding-section-large">
          <div class="w-layout-grid layout1_component">
            <div class="layout1_content albinflex">
              <div class="margin-bottom-2 margin-small albinflex1">
                <p class="text-size-medium">
                  "Kujdesi shëndetësor me cilësin më të lartë"
                </p>
                <h2 class="sg-heading-2 sg-text-color-dark-slate-blue">
                  PureLife Hospital cilsohet në një nivel krejtsisht tjetër
                </h2>
              </div>

              <div class="margin-top margin-medium albinflex2">
                <div class="w-layout-grid stat-award-grid">
                  <div
                    id="w-node-abddc768-20a2-8182-4ec9-9a9368eb86e9-ef1fd367"
                    class="stat-container"
                  >
                    <div class="margin-bottom margin-medium align-center">
                      <div class="stat">94%</div>
                    </div>
                    <div class="stat-text">
                      PËRQINDJA E PACIENTËVE TË KNAQUR
                    </div>
                  </div>
                  <div
                    id="w-node-abddc768-20a2-8182-4ec9-9a9368eb86ef-ef1fd367"
                    class="stat-container"
                  >
                    <div class="margin-bottom margin-medium align-center">
                      <div class="stat">50%</div>
                    </div>
                    <div class="stat-text">ZVOGËLIMI I KOHËS SË PRITJES</div>
                  </div>
                  <div
                    id="w-node-abddc768-20a2-8182-4ec9-9a9368eb86f5-ef1fd367"
                    class="stat-container"
                  >
                    <div class="margin-bottom margin-medium align-center">
                      <div class="stat">4x</div>
                    </div>
                    <div class="stat-text">
                      TEKNOLOGJI DHE FASILITETE TË AVANCUARA
                    </div>
                  </div>
                  <div
                    id="w-node-abddc768-20a2-8182-4ec9-9a9368eb86fb-ef1fd367"
                    class="stat-container"
                  >
                    <div class="margin-bottom margin-medium align-center">
                      <div class="stat">90%</div>
                    </div>
                    <div class="stat-text">STAFI MJEKËSOR I NJOHUR</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <img src="../assets/bootombanner.svg" class="upbanner" alt="" />

  <CardsServiceSection />
  <div class="historiku">
    <div class="headerhistoriku"><h1>Historiku Ynë</h1></div>
    <div class="contenthisoriku">
      <p>
        <br />
        Themelimi i Spitalit PureLife u realizua në vitin 1985, kur Qeveria e
        Shqipërisë vendosi të ndërtojë një institucion modern shëndetësor që të
        plotësonte nevojat e popullsisë për shërbime mjekësore të avancuara.
        Projektimi dhe ndërtimi i spitalit u bënë në bashkëpunim me ekspertë të
        njohur ndërkombëtarë dhe me përdorimin e teknologjisë së fundit në
        fushën e mjekësisë.<br />
        <br />
        Spitali PureLife u hap zyrtarisht më 4 prill 1987 dhe nisi të ofronte
        shërbime shëndetësore në një ambient modern dhe të pajisur me aparatura
        të avancuara. Ai u specializua në trajtimin e sëmundjeve të rënda dhe
        komplekse, si dhe në kirurgjinë kardiotorakale, ortopedike dhe
        neurologjike.<br />
        <br />
        Në vitet që pasuan, Spitali PureLife u rrit dhe u zgjerua, duke shtuar
        ndërtime të reja dhe duke investuar në zhvillimin e kapaciteteve të tij.
        U krijua një departament i ri për mjekësinë e emergjencës dhe u ndërtua
        një qendër kërkimore shkencore, ku specialistët e shëndetësisë
        zhvillonin studime dhe hulumtime të ndryshme për të përmirësuar
        trajtimin dhe diagnostikimin e sëmundjeve.<br />
        <br />
        Në vitin 2005, Spitali PureLife u privatisua dhe u zhvillua më tej nën
        menaxhimin e një kompanie të specializuar në sektorin e shëndetësisë.
        Kjo hapje e re solli investime të mëdha në infrastrukturë, pajisje
        mjekësore dhe zhvillim të stafit profesional.<br />
        <br />
        Gjatë viteve të fundit, Spitali PureLife ka vazhduar të përparojë dhe të
        sigurojë shërbime shëndetësore të larta për qytetarët e Shqipërisë. Ai
        ka hapur qendra të reja nëpër qytete të tjera të vendit dhe ka zgjeruar
        fushat e specializimit në disiplina të ndryshme mjekësore.
      </p>
    </div>
  </div>
  <svg
    width="100%"
    viewBox="0 0 1439 58"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
    style="margin: -1px"
  >
    <path
      d="M1439 57.5L0 57.4998L1.00686e-06 40.9962C706.5 93.928 537.5 -73.5422 1439 40.9964L1439 57.5Z"
      fill="#252525"
    />
  </svg>
</template>

<script>
import CardsServiceSection from "~/sections/CardsServiceSection.vue";
export default {
  components: {
    CardsServiceSection,
  },
};
</script>
<style scoped>
@import url("https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap");

.headerhistoriku h1 {
  font-size: 50px;
}
.contenthisoriku {
  font-size: 20px;
}
.upbanner2 {
  fill: lightblue;
  width: 100%;
  bottom: -7px;
}
.about-Title {
  font-family: "Poppins", sans-serif;
  font-size: 100px;
  font-weight: 600;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 1.5);
}
.historiku {
  margin: 50px 184px 90px 184px;
  display: flex;
  flex-direction: column;
  font-family: "Poppins", sans-serif;
  justify-content: center;
  align-items: center;
}
.about-paragraf {
  font-family: "Poppins", sans-serif;
  font-size: 20px;
  font-weight: 200;
  margin-top: -20px;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 2.5);
}
.wave-form-background {
  height: 35rem;
  background-color: #f6f2f0;
  margin: -2px 0px;
}
.wave-form-background-img {
  height: 45rem;
  margin: -2px 0px;

  background-image: url("../assets/stafii.jpg");
  background-size: cover;
  background-position: center;
  margin-bottom: -70px;
}

.overlay-text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  text-align: center;
  color: white;
}

.upbanner {
  width: 100%;
  bottom: -7px;
}
@media screen and (max-width: 700px) {
  .wave-form-background-img img {
    height: 100%;
    width: auto;
    object-fit: cover;
    position: absolute;
    top: 0;
    left: 50%;
    transform: translateX(-50%);
  }
  .wave-form-background {
    height: 100vh;
    background-color: #f6f2f0;
    margin: -2px 0px;
  }
  .padding-global {
    display: flex;
    flex-direction: column;
    justify-content: center;

    padding-right: 60px;
  }

  .albinflex {
    width: 100%;
    display: flex;
    justify-content: center;
    flex-direction: column;
  }
  .padding-section-large {
    padding: 40px 0px 40px 0px !important;
  }
  .albinflex1 {
    display: flex;
    width: 100% !important;
    padding: 0px 30px !important;
    flex-direction: column;
    justify-content: center;
  }
  .albinflex2 {
    display: flex;
    width: 100% !important;
    flex-direction: column;
    justify-content: center;
    padding: 0px 30px !important;
  }
  .albinflex1 h2 {
    font-size: 40px !important;
  }
  .stat {
    font-size: 60px !important;
  }
  .stat-text {
    font-size: 18px !important;
  }
}
.padding-global {
  display: flex;
  justify-content: center;
}
.albinflex {
  width: 100%;
  display: flex;
  justify-content: center;
}
.albinflex1 {
  width: 50%;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.albinflex2 {
  width: 50%;
  display: flex;
}
.container-large-3 {
  width: 100%;
  max-width: 80rem;
}

.padding-section-large {
  padding-top: 7rem;
  padding-bottom: 7rem;
}

.w-layout-grid {
  grid-row-gap: 16px;
  grid-column-gap: 16px;
  grid-template-rows: auto auto;
  grid-template-columns: 1fr 1fr;
  grid-auto-columns: 1fr;
  display: grid;
}

.layout1_component {
  grid-column-gap: 5rem;
  grid-row-gap: 4rem;
  grid-template-rows: auto;
  grid-template-columns: 1fr 1fr;
  grid-auto-columns: 1fr;
  align-items: center;
  display: flex;
}

.layout402_image-wrapper {
  position: relative;
}

#w-node-abddc768-20a2-8182-4ec9-9a9368eb8701-ef1fd367 {
  grid-area: span 1 / span 1 / span 1 / span 1;
}

.margin-bottom-2 {
  margin-top: 0;
  margin-left: 0;
  margin-right: 0;
}

.margin-small {
  margin: 1rem;
}

.margin-bottom-2.margin-small {
  margin: 0 0 1rem;
}

p {
  margin-top: 0;
  margin-bottom: 10px;
}

.text-size-medium {
  margin-bottom: 0;
  font-family: Inter-Font, sans-serif;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.8125rem;
}

.margin-top {
  margin-bottom: 0;
  margin-left: 0;
  margin-right: 0;
}

.margin-medium {
  margin: 2rem;
}

.margin-top {
  margin-right: 0rem !important;
  margin-bottom: 0rem !important;
  margin-left: 0rem !important;
}

.margin-top.margin-medium {
  margin-bottom: 0;
  margin-left: 0;
  margin-right: 0;
}

img {
  display: inline-block;
  border: 0;
  max-width: 100%;
  vertical-align: middle;
}

.results-image {
  max-width: 120%;
  position: absolute;
  top: -260px;
  right: -28.7656px;
}

@media screen and (min-width: 1280px) {
  .results-image {
    max-width: 120%;
    right: -36.7656px;
  }
}

@media screen and (min-width: 1440px) {
  .results-image {
    right: -102.766px;
  }
}

h2 {
  margin-bottom: 10px;
  font-weight: 700;
  margin-top: 70px;
  font-size: 32px;
  line-height: 36px;
}

.sg-heading-2 {
  letter-spacing: -1px;
  margin-top: 0;
  margin-bottom: 0;
  font-family: Quarto-black, sans-serif;
  font-size: 3.625rem;
  font-weight: 900;
  line-height: 120%;
  padding-right: 40px !important;
}

.sg-text-color-dark-slate-blue {
  color: #2e3f52;
}

.stat-container {
  width: 100%;
  padding-right: 0;
}

#w-node-abddc768-20a2-8182-4ec9-9a9368eb86e9-ef1fd367 {
  grid-area: span 1 / span 1 / span 1 / span 1;
}

#w-node-abddc768-20a2-8182-4ec9-9a9368eb86ef-ef1fd367 {
  grid-area: span 1 / span 1 / span 1 / span 1;
}

#w-node-abddc768-20a2-8182-4ec9-9a9368eb86f5-ef1fd367 {
  grid-area: span 1 / span 1 / span 1 / span 1;
}

#w-node-abddc768-20a2-8182-4ec9-9a9368eb86fb-ef1fd367 {
  grid-area: span 1 / span 1 / span 1 / span 1;
}

.margin-bottom {
  margin-top: 0;
  margin-left: 0;
  margin-right: 0;
}

.align-center {
  margin-left: auto;
  margin-right: auto;
}

.margin-bottom {
  margin-top: 0rem !important;
  margin-right: 0rem !important;
  margin-left: 0rem !important;
}

.margin-bottom.margin-medium {
  margin-top: 0;
  margin-left: 0;
  margin-right: 0;
}

@media screen and (min-width: 1280px) {
  .margin-bottom.margin-medium {
    margin-top: 0;
    margin-left: 0;
    margin-right: 0;
  }
}

.margin-bottom.margin-medium.align-center {
  justify-content: center;
  display: flex;
}

.stat-text {
  color: #333;
  text-align: center;
  font-size: 1.3rem;
  font-weight: 400;
  line-height: 1.5;
  margin-top: -20px;
}

.stat {
  color: #1175d1;
  text-align: center;
  letter-spacing: -0.1rem;
  white-space: nowrap;
  font-family: Quarto-black, sans-serif;
  font-size: 5rem;
  font-weight: 900;
  line-height: 1.2;
}
</style>
