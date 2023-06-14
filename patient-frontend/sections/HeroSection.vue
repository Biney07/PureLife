<template>
  <section class="banner" id="banner">
    <div class="content">
      <h2>Qetsohu, ne jemi këtu!</h2>
      <h3 style="color: white; font-weight: 200;">
        Dedikuar në ofrimin e kujdesit mjekësor të jashtëzakonshëm,
         në fuqizimin e komuniteteve dhe në transformimin e jetëve 
         përmes dhembshurisë dhe ekspertizës
      </h3>

    </div>
  </section>
  <section class="hero">
    <div class="hero-content">
      <p>Vizitohu tek mjeket me te mire ne vend</p>
      <button class="rezervo-termin">Rezero Termin!</button>

      <div class="ai-chat">
        <div class="message-space">
          <div class="message">
            <img src="../assets/ai-user.svg" alt="" />
            <p>
              Nëse keni probleme me zemër, duhet të shkosh tek një mjek
              kardiolog. Mjeku kardiolog është specialisti i trajtimit të
              sëmundjeve të zemrës dhe sistemit kardiovaskular.
            </p>
          </div>
          <div class="message">
            <img src="../assets/ai-user.svg" alt="" />
            <p>
              Ju nevojitet te zgjidhni nje termin me te larte ne kategorine
              kardiologjike. Ju mund te beni kete duke shkuar ne faqen tone te
              internetit ose duke na telefonuar ne numrin +383 49 401 551.
            </p>
          </div>
        </div>
        <div class="question-input">
          <span class="question-text"
            >Kam therje ne zemer tek cili doktor duhet te shkoj??</span
          >
          <div class="search-button">
            <img src="../assets/search.svg" alt="search" />
          </div>
        </div>
        <small class="register-invite"
          >Per te perdorur sherbimet e inteligjences artificiale regjistrohuni
          <span>ketu</span>.</small
        >
      </div>
    </div>
  </section>
</template>

<script>
export default {};
</script>

<style scoped>

.banner {
  position: relative;
  width: 100%;
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background: linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)),
    url(../assets/hero.jpg);
  background-size: cover;
}

.banner .content {
  max-width: 900px;
  text-align: center;
}

.banner .content h2 {
  font-size: 4em;
  color: #fff;
}
.banner .content p {
  font-size: 1em;
  color: #fff;
}

.btn {
  font-size: 1.5em;
  color: #fff;
  background: var(--blue);
  display: inline-block;
  padding: 10px 30px;
  text-transform: uppercase;
  margin-top: 20px;
  text-decoration: none;
  letter-spacing: 2px;
  transition: 0.5s;
}

.btn:hover {
  letter-spacing: 5px;
}
.hero {
  padding-top: 90px;
  height: calc(100vh - var(--header-height));
  background: var(--secondary-color);
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.hero-content {
  margin-top: 50px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.hero-content p {
  font-size: 50px;
  font-weight: 300;
}

.rezervo-termin {
  border: none;
  outline: none;
  background: var(--button-background);
  color: white;
  font-size: 20px;
  padding: 15px 30px;
  border-radius: 10px;
  width: fit-content;
  margin-top: 40px;
  cursor: pointer;
  transition: background-color 0.5s;
  font-weight: 300 !important;
}

.rezervo-termin:hover {
  background: rgba(44, 100, 216, 0.8);
  transition: background-color 0.5s;
}

.ai-chat {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 400px;
  margin-top: 50px;
  width: 55%;
}

.message-space {
  height: 300px;
  border: 1px solid black;
  background: linear-gradient(
    180deg,
    rgba(255, 255, 255, 0) 0%,
    #ffffff 39.19%
  );
  border-radius: 10px;
  position: relative;
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  overflow-y: auto;
  padding: 10px;
  gap: 10px;
}

.message {
  padding: 0 10px;
  display: flex;
  align-items: center;
  gap: 15px;
  margin-bottom: 10px;
}

.message p {
  font-size: 16px;
}

.question-input {
  height: 60px;
  border: 1px solid black;
  border-radius: 5px;
  margin-top: 10px;
  width: 100%;
  position: relative;
  background: white;
}

.question-text {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  left: 10px;
}

.register-invite {
  margin-top: 10px;
}

.register-invite span {
  color: var(--secondary-font-color);
  cursor: pointer;
}

.search-button {
  background: var(--button-background);
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  height: 100%;
  right: 0;
  width: 60px;
  border-bottom-right-radius: 4px;
  border-top-right-radius: 4px;
}
</style>
