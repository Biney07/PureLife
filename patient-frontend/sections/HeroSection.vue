

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
    <div v-for="message in messages" :key="message.content" class="message">
      <img v-if="message.sender === 'ai'" src="../assets/purelife.png" style="width: 30px;" alt="" />
      <img v-if="message.sender === 'user'" src="../assets/ai-user.svg"  alt="" />
      <p>{{ message.content }}</p>
      <div v-if="loading" class="loader"></div>
    </div>
  </div>
  <div class="question-input">
  <input v-model="userMessage" @keyup.enter="submitMessage" type="text" placeholder="Kam veshtiresi ne frymemarrje tek cili doktor duhet te shkoj?">
  <div class="search-button" @click="submitMessage">
    <img src="@/assets/search.svg" alt="search" />

  </div>

</div>


      </div>
    </div>
  </section>
</template>
<script>
import axios from 'axios';

export default {
  data() {
    return {
      messages: [],
      loading: false,
      userMessage : '',
    };
  },
  
  methods: {
  submitMessage() {
    if (this.userMessage) {
      this.sendMessage(this.userMessage);
      this.userMessage = '';
    }
  },
  async sendMessage(text) {
  // Push user message to the messages array
  this.messages.push({
    sender: 'user',
    content: text,
  });

  try {
    // Set loading to true before making the API call
    this.loading = true;

    // Send a POST request to the ChatGPT API
    const response = await axios.get('https://localhost:7292/api/OpenAIAPI/use-chat?query=' + text);

    // Push the AI's response to the messages array
    this.messages.push({
      sender: 'ai',
      content: response.data,
    });
  } catch (error) {
    // Handle API error
    console.error(error);
  } finally {
    // Set loading to false after the API call is completed
    this.loading = false;
  }
},
  // Other component methods
}

  
  // Other component methods
}
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
    url(./assets/hero.jpg);
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
  height: 100vh;
  background: var(--secondary-color);
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.hero-content {
  margin-top: 70px;
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
  width: 70vw;
}

.message-space {
  width: 110%;
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
  position: relative;
}

.message {

  display: flex;
  align-items: center;
  gap: 15px;
  margin-bottom: 10px;
}

.message p {
  font-size: 16px;
  margin-bottom: 0;
}

.question-input {
  display: flex;
  height: 60px;
  border: 1px solid black;
  border-radius: 8px;
  width: 110%;
  margin: 10px 0;
  position: relative;
  background: white;
  overflow: hidden;
  padding-left: 8px;

}

.question-input input {
  width: 100%;
}

.question-text {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  left: 10px;
  padding-left: 8px;
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
.loader {
  border: 6px solid #f3f3f3;
  border-top: 6px solid var(--button-background);
  border-radius: 50%;
  width: 30px;
  height: 30px;
  animation: spin 1s linear infinite;
  align-self: center;
  margin-top: 10px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

</style>
