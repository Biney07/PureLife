<template>
  <div class="aboutcontainer">
    <div class="container">
      <div class="card a">
        <span></span>
        <div class="content">
          <h2>Shkëlqim në Ekspertizën Mjekësore</h2>
          <p>
            Krenohemi me ekspertizën tonë të shkëlqyeshme në fushën mjekësore.
            Ekipi ynë i mjekëve, infermierëve dhe specialistëve të kualifikuar
            është përkushtuar të ofrojë standardet më të larta të kujdesit
            shëndetësor.
          </p>
          <a href="#">Staf I Përkryer</a>
        </div>
      </div>

      <div class="card b">
        <span></span>
        <div class="content">
          <h2>Qasje përfekte ndaj Pacientëve</h2>
          <p>
            Vendosim pacientët tanë në qendër të çdo gjëje që bëjmë. Krenohemi
            me qasjen tonë qendrore ndaj pacientëve, e cila përqendrohet në
            ndërtimin e marrëdhënieve të besimit, sigurimin e transparencës dhe
            ofrimin e kujdesit me dashamirësi.
          </p>
          <a href="#">Qasje perfekte </a>
        </div>
      </div>

      <div class="card c">
        <span></span>
        <div class="content">
          <h2>Objekte të Avancuara Teknologjike</h2>
          <p>
            Jemi krenarë që ofrojmë objekte të avancuara teknologjike që janë
            pajisur me teknologjinë mjekësore të fundit. Nga pajisjet e
            avancuara për imazh dhe diagnostikim deri te sallat moderne të
            operacionit, jemi numri 1 ne rajon.
          </p>
          <a href="#">Teknologji e Avancuar</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AboutUsCards",
};
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500&display=swap");
.aboutcontainer {
  display: flex;
  justify-content: center;
  padding: 100px 0px 100px 0px;
}

.container {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
}

.card {
  position: relative;
  width: 320px;
  height: 400px;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 40px 30px;
  transition: 0.4s;
  background: none !important;
  border: none;
}

.card::before {
  content: "";
  position: absolute;
  background: none;
  width: 50%;
  height: 100%;
  top: 0;
  left: 50px;
  border-radius: 10px;
  transform: skewx(15deg);
  transition: 0.4s;
}

.card::after {
  content: "";
  position: absolute;

  width: 50%;
  height: 100%;
  top: 0;
  left: 50px;
  border-radius: 10px;
  transform: skewx(15deg);
  transition: 0.4s;
  filter: blur(30px);
}

.card:hover::before,
.card:hover::after {
  transform: skewx(0);
  left: 20px;
  width: calc(100% - 80px);
}

.card:nth-child(1)::before,
.card:nth-child(1)::after {
  background: linear-gradient(315deg, #064f8f, #51a3ea);
}

.card:nth-child(2)::before,
.card:nth-child(2)::after {
  background: linear-gradient(315deg, #064f8f, #51a3ea);
}

.card:nth-child(3)::before,
.card:nth-child(3)::after {
  background: linear-gradient(315deg, #064f8f, #51a3ea);
}

.card span {
  display: block;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 2;
  pointer-events: none;
}

.card span::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border-radius: 10px;
  opacity: 0;
  transition: 0.4s;
}

.card:hover span::before {
  width: 100px;
  height: 100px;
  opacity: 1;
  left: 50px;
  top: -50px;
  animation: swing 2s ease-in-out infinite;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}

.card span::after {
  content: "";
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 100%;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border-radius: 10px;
  opacity: 0;
  transition: 0.4s;
}

.card:hover span::after {
  width: 100px;
  height: 100px;
  opacity: 1;
  right: 50px;
  bottom: -50px;
  animation: swing 2s ease-in-out infinite;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
  animation-delay: -1s;
}

.content {
  z-index: 1;
  position: relative;
  left: 0;
  background: rgba(255, 255, 255, 0.05);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  backdrop-filter: blur(10px);
  padding: 20px 50px;
  transition: 0.4s;
  color: white;
}

.card:hover .content {
  left: -25px;
  padding: 40px 50px;
}

.card h2 {
  font-size: 1.9em;
  margin-bottom: 10px;
}

.card p {
  font-size: 0.95em;
  font-weight: 300;
  margin-bottom: 20px;
}

.card a {
  display: inline-block;
  text-decoration: none;
  background: white;
  color: #222;
  font-size: 1.1em;
  font-weight: 500;
  padding: 10px;
  border-radius: 5px;
}

@keyframes swing {
  0% {
    transform: translatey(10px);
  }

  50% {
    transform: translatey(-10px);
  }

  100% {
    transform: translatey(10px);
  }
}
</style>
