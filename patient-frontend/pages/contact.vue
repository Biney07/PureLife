<template>
  <div class="wave-form-background-img">
    <div class="overlay-text">
      <h1 class="about-Title">NA KONTAKTONI</h1>
      <p class="about-paragraf">
        "Spitali PureLife: Kujdes për Shëndetin, Përkrahje për Jetën"
      </p>
    </div>
  </div>
  <div class="wrap-contact100">
    <div class="contactimage">
      <div class="containeremergjenca" style="display: flex">
        <h1 class="about-Titlee">THIRR EMERGJENCEN</h1>
        <p
          class="ose"
          style="
            display: flex;
            align-items: end;
            font-size: 50px;
            padding: 0px 0px 0px 30px;
            font-weight: 500;
          "
        >
          ose
        </p>
      </div>
      <h1 class="about-Titlee" style="color: #ed2b2a">+383 49 800 800</h1>
      <div style="border-top: 3px solid black"></div>
      <h1 class="about-Titlee" style="color: #ed2b2a">+383 44 500 500</h1>
    </div>
    <div class="formacontact">
      <form class="contact100-form validate-form" @submit.prevent="submitForm">
        <h1 class="about-Titlee">NA KONTAKTONI</h1>
        <div class="wrap-input100 validate-input">
          <input
            class="input100"
            type="text"
            v-model="name"
            name="name"
            :placeholder="namePlaceholder"
          />
          <span class="focus-input100"></span>
        </div>
        <div
          class="wrap-input100 validate-input"
          data-validate="Please enter email: e@a.x"
        >
          <input
            class="input100"
            v-model="email"
            type="text"
            name="email"
            :placeholder="emailPlaceholder"
          />
          <span class="focus-input100"></span>
        </div>
        <div class="wrap-input100 validate-input">
          <textarea
            class="input100"
            v-model="message"
            name="message"
            :placeholder="messagePlaceholder"
          ></textarea>
          <span class="focus-input100"></span>
        </div>
        <div class="container-contact100-form-btn">
          <button
            class="contact100-form-btn"
            :disabled="!name || !email || !message"
          >
            Send Email
          </button>
        </div>
      </form>

      <div class="contact100-more">
        Contact our 24/7 call center:
        <span class="contact100-more-highlight">+001 345 6889</span>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      name: "",
      email: "",
      message: "",
    };
  },
  computed: {
    namePlaceholder() {
      return this.name ? this.name : "Full Name";
    },
    emailPlaceholder() {
      return this.email ? this.email : "Email";
    },
    messagePlaceholder() {
      return this.message ? this.message : "Your Message";
    },
  },
  methods: {
    submitForm() {
      if (!this.name || !this.email || !this.message) {
        // Handle empty fields
        alert("Please fill in all the fields.");
        return;
      }

      // Prepare data for the request
      const formData = {
        name: this.name,
        email: this.email,
        message: this.message,
      };

      // Log the data as JSON format
      console.log(JSON.stringify(formData));

      // Make an axios request to send the data
      axios
        .post("/api/contact", formData)
        .then((response) => {
          // Handle success
          console.log(response.data);
          alert("Email sent successfully!");
          // Reset form fields
          this.name = "";
          this.email = "";
          this.message = "";
        })
        .catch((error) => {
          // Handle error
          console.error(error);
          alert("An error occurred while sending the email.");
        });
    },
  },
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900i&display=swap");
@font-face {
  font-family: Material-Design-Iconic-Font;
  src: url(https://colorlib.com/etc/cf/ContactFrom_v9/fonts/iconic/fonts/Material-Design-Iconic-Font.woff2?v=2.2.0)
      format("woff2"),
    url(https://colorlib.com/etc/cf/ContactFrom_v9/fonts/iconic/fonts/Material-Design-Iconic-Font.woff?v=2.2.0)
      format("woff"),
    url(https://colorlib.com/etc/cf/ContactFrom_v9/fonts/iconic/fonts/Material-Design-Iconic-Font.ttf?v=2.2.0)
      format("truetype");
}
@font-face {
  font-family: SourceSansPro-Regular;
  src: url("https://colorlib.com/etc/cf/ContactFrom_v9/fonts/source-sans-pro/SourceSansPro-Regular.ttf");
}
@font-face {
  font-family: SourceSansPro-Bold;
  src: url("https://colorlib.com/etc/cf/ContactFrom_v9/fonts/source-sans-pro/SourceSansPro-Bold.ttf");
}
@font-face {
  font-family: JosefinSans-Bold;
  src: url("https://colorlib.com/etc/cf/ContactFrom_v9/fonts/JosefinSans/JosefinSans-Bold.ttf");
}
.wave-form-background-img {
  height: 45rem;
  margin: -2px 0px;

  background-image: url("../assets/contact.jpg");
  background-size: cover;
  background-position: center;
  margin-bottom: -70px;
  display: flex;
  justify-content: center;
  align-items: center;
  align-content: center;
}
.overlay-text {
  text-align: center;
  color: white;
}

.about-Title {
  font-family: "Poppins", sans-serif;
  font-size: 100px;
  font-weight: 600;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 1.5);
}
.about-Titlee {
  font-family: "Poppins", sans-serif;
  font-size: 60px;
  font-weight: 600;
  text-align: left;
}
.about-paragraf {
  font-family: "Poppins", sans-serif;
  font-size: 20px;
  font-weight: 200;
  margin-top: -20px;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 2.5);
}
.upbanner {
  width: 100%;
  bottom: -7px;
}
.formacontact {
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 50%;
  padding: 3% 5.5% 3.125% 5.5%;
}
.contactimage {
  width: 50%;
  padding: 50px;
}
.contactimage img {
  width: 100%;
}
@media screen and (max-width: 1000px) {
  .formacontact {
    width: 80%;
    padding-top: 20px !important;
  }
  .ose {
    display: none !important;
  }
  .containeremergjenca {
    display: flex;
    flex-direction: column !important;
  }
  .contactimage {
    width: 85%;
    padding: 40px;
  }
  .wrap-contact100 {
    display: flex;
    flex-direction: column !important;
  }
  .about-Title {
    font-size: 40px;
  }
  .about-paragraf {
    font-size: 15px !important;
    padding-top: 5px;
  }
  .about-Titlee {
    font-size: 40px;
  }
  .formacontact {
    display: flex;
    flex-direction: column;
    justify-content: center;

    padding: 20% 10.5% 3.125% 10.5%;
  }
}

.contactcontainer {
  background-color: #b5dff4;
  display: flex;
  justify-content: center;
  padding: 100px 0px 0px 0px;
}
.container-contact100 {
  width: 100%;

  min-height: 100vh;
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  padding: 15px;
  position: relative;
  z-index: 1;
}

.wrap-contact100 {
  width: 100%;
  background: #fff;
  display: flex;
  flex-direction: row;
  overflow: hidden;
}

*,
:after,
:before {
  box-sizing: inherit;
}

.contact100-form {
  width: 100%;
}

.contact100-more {
  font-family: SourceSansPro-Regular;
  font-size: 16px;
  color: #999999;
  line-height: 1.5;
  text-align: center;
}

.contact100-form-title {
  display: block;
  font-size: 30px;
  color: #333333;
  line-height: 1.2;
  text-align: left;
  padding-bottom: 34px;
}

.wrap-input100 {
  width: 100%;
  position: relative;
  background-color: #fff;
  border-radius: 20px;
  margin-bottom: 30px;
}

.validate-input {
  position: relative;
}

.alert-validate:before {
  content: attr(data-validate);
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  align-items: center;
  position: absolute;
  width: 100%;
  min-height: 62px;
  background-color: #fff;
  border-radius: 20px;
  top: 0px;
  left: 0px;
  padding: 0 45px 0 22px;
  pointer-events: none;
  font-family: SourceSansPro-Bold;
  font-size: 16px;
  color: #fa4251;
  line-height: 1.2;
}

.container-contact100-form-btn {
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  padding-top: 10px;
  padding-bottom: 43px;
}

.contact100-more-highlight {
  color: #1d7eed;
}

input {
  -ms-touch-action: manipulation;
  touch-action: manipulation;
  margin: 0;
  font-family: inherit;
  font-size: inherit;
  line-height: inherit;
  overflow: visible;
  outline: none;
  border: none;
}

.input100 {
  display: block;
  width: 100%;
  background: transparent;
  font-family: SourceSansPro-Bold;
  font-size: 16px;
  color: #149fe5;
  line-height: 1.2;
}

input.input100 {
  height: 62px;
  padding: 0 20px 0 23px;
}

.focus-input100 {
  display: block;
  position: absolute;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  pointer-events: none;
  border-radius: 20px;
  box-shadow: 0 5px 20px 0px rgba(0, 0, 0, 0.3);
  -moz-box-shadow: 0 5px 20px 0px rgba(0, 0, 0, 0.3);
  -webkit-box-shadow: 0 5px 20px 0px rgba(0, 0, 0, 0.3);
  -o-box-shadow: 0 5px 20px 0px rgba(0, 0, 0, 0.3);
  -ms-box-shadow: 0 5px 20px 0px rgba(0, 0, 0, 0.5);
  -webkit-transition: all 0.4s;
  -o-transition: all 0.4s;
  -moz-transition: all 0.4s;
  transition: all 0.4s;
}

.alert-validate .focus-input100 {
  box-shadow: 0 5px 20px 0px rgba(250, 66, 81, 0.4);
  -moz-box-shadow: 0 5px 20px 0px rgba(250, 66, 81, 0.4);
  -webkit-box-shadow: 0 5px 20px 0px rgba(250, 66, 81, 0.4);
  -o-box-shadow: 0 5px 20px 0px rgba(250, 66, 81, 0.4);
  -ms-box-shadow: 0 5px 20px 0px rgba(250, 66, 81, 0.4);
}

.btn-hide-validate {
  font-family: Material-Design-Iconic-Font;
  font-size: 15px;
  color: #fa4251;
  cursor: pointer;
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  height: 62px;
  top: 0px;
  right: 28px;
}

textarea {
  -ms-touch-action: manipulation;
  touch-action: manipulation;
  margin: 0;
  font-family: inherit;
  font-size: inherit;
  line-height: inherit;
  overflow: auto;
  resize: vertical;
  outline: none;
  border: none;
}

textarea.input100 {
  min-height: 199px;
  padding: 19px 20px 0 23px;
}

button {
  -ms-touch-action: manipulation;
  touch-action: manipulation;
  margin: 0;
  font-family: inherit;
  font-size: inherit;
  line-height: inherit;
  overflow: visible;
  text-transform: none;
  -webkit-appearance: button;
  outline: none !important;
  border: none;
  background: transparent;
}

.contact100-form-btn {
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0 20px;
  min-width: 160px;
  height: 42px;
  background-color: #1d7eed;
  border-radius: 21px;
  font-size: 14px;
  color: #fff;
  text-transform: uppercase;
  -webkit-transition: all 0.4s;
  -o-transition: all 0.4s;
  -moz-transition: all 0.4s;
  transition: all 0.4s;
  box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.5);
  -moz-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.5);
  -webkit-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.5);
  -o-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.5);
  -ms-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.5);
}

button:hover {
  cursor: pointer;
}

.contact100-form-btn:hover {
  background-color: #0a53b2;
  box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.8);
  -moz-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.8);
  -webkit-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.8);
  -o-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.8);
  -ms-box-shadow: 0 10px 30px 0px rgba(42, 145, 228, 0.8);
}
</style>
