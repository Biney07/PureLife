<template>
  <div class="contactcontainer">
    <div class="wrap-contact100">
      <div class="contactimage">
        <img src="../assets/contactimage.jpg" alt="" />
      </div>
      <div class="formacontact">
        <form
          class="contact100-form validate-form"
          @submit.prevent="submitForm"
        >
          <span class="contact100-form-title">Contact Us</span>
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
* {
  box-sizing: border-box;
  margin: 0px;
  padding: 0px;
}
.formacontact {
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 50%;
  padding: 1% 10.5% 3.125% 10.5%;
}
.contactimage {
  width: 50%;
}
.contactimage img {
  width: 100%;
}
@media screen and (max-width: 1000px) {
  .contactimage {
    display: none;
  }
  .formacontact {
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 100%;
  padding: 9% 10.5% 3.125% 10.5%;
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
  font-family: var(--primary-font);
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
  font-family: var(--primary-font);
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
  font-family: var(--primary-font);
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
  font-family: var(--primary-font);
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
