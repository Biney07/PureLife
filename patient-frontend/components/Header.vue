<template>
  <div class="navbar">
    <header v-if="!isUserLoggedIn || !isDashboardPath">
      <div class="logoheader">
        <img src="../assets/purelife.png" loading="lazy" class="logo-header" />
        <NuxtLink to="/" class="logo">Pure<span>Life</span></NuxtLink>
      </div>

      <div class="menutoggle" @click="toggleMenu"></div>
      <ul class="navigation" :class="{ active: isMenuActive }">
        <li>
          <NuxtLink to="/">Home</NuxtLink>
        </li>
        <li>
          <NuxtLink to="/staf">Stafi</NuxtLink>
        </li>
        <li>
          <NuxtLink to="/contact">Kontakti</NuxtLink>
        </li>
        <li>
          <NuxtLink to="/about">About</NuxtLink>
        </li>
        <li v-if="!isUserLoggedIn">
          <NuxtLink to="/login">Login</NuxtLink>
        </li>
        <li v-if="!isUserLoggedIn">
          <NuxtLink to="/register">Register</NuxtLink>
        </li>
        <li class="dropdown" v-if="isUserLoggedIn">
          <div class="profilinav">
            <div class="dropdown-toggle" role="button" @click="toggleContainer" aria-expanded="false">
              <img class="profile-image" src="../assets/profile.jpg" alt="Profile Image" />
              <div :class="{ 'whitecontainer-on': isContainerVisible, 'whitecontainer-off': !isContainerVisible }">
                <div class="buttons">
                  <NuxtLink class="btn" to="/dashboard/profile">Profile</NuxtLink>
                  <NuxtLink class="btn" @click="logout" to="/login">Logout</NuxtLink>

                </div>
              </div>
            </div>
          </div>


        </li>

        <li v-if="isUserLoggedIn">

        </li>
      </ul>
    </header>

    <header class="dashboard-header" v-else>
      <nav class="header__nav">
        <NuxtLink to="/dashboard"><img src="../assets/purelife color.png" alt="purelife-logo"></NuxtLink>
        <ul class="header__menu" data-aos="fade-down">
          <div class="seperated-links">
            <li>
              <NuxtLink to="/dashboard/terminet">Terminet</NuxtLink>
            </li>
            <li>
              <NuxtLink to="/dashboard/profile">Profile</NuxtLink>
            </li>
            <li>
              <NuxtLink to="/dashboard/chat">Bisedat</NuxtLink>
            </li>
          </div>
          <div class="auth-links">
            <li>
              <NuxtLink to="/">Home
              </NuxtLink>
            </li>
          </div>
        </ul>
      </nav>
    </header>
  </div>
</template>

<script>




import { userExists, removeUser } from "@/helper/auth"
import { userSignOut } from "@/patient-sdk/auth"
export default {
  data() {
    return {
      isMenuActive: false,
      isContainerVisible: false
    };
  },
  mounted() {
    window.addEventListener("scroll", this.handleScroll);
  },
  beforeDestroy() {
    window.removeEventListener("scroll", this.handleScroll);
  },
  computed: {
    isUserLoggedIn() {
      return userExists()
    },
    isDashboardPath() {
      return this.$route.path.includes("/dashboard");
    },
  },
  methods: {
    handleScroll() {
      const header = document.querySelector("header");
      header.classList.toggle("sticky", window.scrollY > 0);
    },
    toggleMenu() {
      this.isMenuActive = !this.isMenuActive;
    },
    async logout() {
      try {
        await userSignOut()
        removeUser();
        window.location.reload();
      } catch (err) {
        console.log(err)
      }
    },

    toggleContainer() {
      console.log(this.isContainerVisible);
      this.isContainerVisible = !this.isContainerVisible;
    },
  },
};
</script>
<style scoped>
.profilinav {
  position: relative;
}

.whitecontainer-off {
  display: none;
}

.whitecontainer-on {
  margin-top: 10px;
  display: block !important;
  right: 0;
  top: 20;
  position: absolute;
  width: 100px;
  background-color: white;
}



.buttons {
  display: flex;
  flex-direction: column;
  width: 100%;
}


.dropdown {
  position: relative !important;
  display: inline-block !important;
}

.nav-link {
  display: inline-block !important;
  color: #000 !important;
  text-decoration: none !important;
  cursor: pointer !important;
}

.nav-link:hover {
  color: #333 !important;
}

.profile-image {
  width: 40px !important;
  height: 40px !important;
  border-radius: 50% !important;
}

.dropdown-menu {
  position: absolute !important;
  top: 100% !important;
  left: 0 !important;
  z-index: 9900 !important;
  display: none !important;
  min-width: 120px !important;
  padding: 0 !important;
  margin: 0 !important;
  list-style-type: none !important;
  background-color: #fff !important;
  border: 1px solid #ccc !important;
}

.dropdown-menu.show {
  display: block !important;
  z-index: 999;
}

.dropdown-item {
  display: block !important;
  width: 100% !important;
  padding: 8px 16px !important;
  text-decoration: none !important;
  color: #333 !important;
}

.dropdown-item:hover {
  background-color: #f8f8f8 !important;
  color: #333 !important;
}

.dropdown-item:focus,
.dropdown-item:active {
  background-color: #ebebeb !important;
  color: #333 !important;
}

.dropdown-item:last-child {
  border-bottom: none !important;
}

.profile-image {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}



.navbar {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
  font-family: "Poppins", sans-serif;
  scroll-behavior: smooth;
  z-index: 10;
}

.logoheader {
  display: flex;
  align-items: center;
}

.logo-header {
  width: 8%;
  margin-right: 18px;
}

.navbar p {
  font-weight: 300;
  color: #111;
}

.banner {
  position: relative;
  width: 100%;
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background: linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)),
    url(https://i.postimg.cc/s29LNR86/bg.jpg);
  background-size: cover;
}

.banner .content {
  max-width: 900px;
  text-align: center;
}

.banner .content h2 {
  font-size: 5em;
  color: #fff;
}

.banner .content p {
  font-size: 1em;
  color: #fff;
}

.btn {

  font-size: 0.9em !important;
  color: #fff !important;
  background: #1c41ea;
  padding: 10px 30px;
  text-transform: uppercase;
  text-decoration: none;
  transition: 0.5s;
  border-radius: 0px;
  margin: 0px;
  display: flex;
  justify-content: center;
  border: 0.5px black !important;
}

.btn:hover {
  letter-spacing: 1px;
}

header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  padding: 25px 90px;
  z-index: 1;
  display: flex;
  justify-content: space-between;
  align-items: center;
  transition: 0.5s;
}

.sticky {
  background: #fff;
  padding: 10px 100px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.05);
}

.sticky .logo {
  color: #111;
}

.sticky .navigation li a {
  color: #111;
}

header .logo {
  color: #fff;
  font-weight: bold;
  font-size: 2.5em;
  text-decoration: none;
}

header .logo span {
  color: #1c41ea;
}

header .navigation {
  position: relative;
  display: flex;
  margin-bottom: 0px;
  align-items: center;
}

header .navigation li {
  list-style: none;
  margin-left: 30px;
}

header .navigation li a {
  text-decoration: none;
  color: #ffffff;
  font-weight: 300;
  font-size: 1.3em;
  transition: 0.5s;
}

header .navigation li a:hover {
  color: #1c41ea;
}

section {
  padding: 80px;
}

.row {
  position: relative;
  width: 100%;
  display: flex;
  justify-content: space-between;
}

.row .col50 {
  position: relative;
  width: 48%;
}

.titeText {
  color: #111;
  font-size: 2em;
  font-weight: 300;
}

.titeText span {
  color: #ff0157;
  font-weight: 700;
  font-size: 1.5em;
}

.imgbx {
  position: relative;
  width: 100%;
  min-height: 300px;
}

.imgbx img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.title {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.menu .content {
  display: flex;
  justify-content: center;
  flex-direction: row-reverse;
  flex-wrap: wrap;
  margin-top: 40px;
}

.menu .content .box {
  width: 340px;
  margin: 20px;
  border: 15px solid #fff;
  box-shadow: 0 5px 35px rgba(0, 0, 0, 0.08);
}

.menu .content .box .imgbx {
  position: relative;
  width: 100%;
  height: 300px;
}

.menu .content .box .imgbx img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.text {
  padding: 15px 0 5px;
  text-align: center;
}

.text h3 {
  font-weight: 400;
  color: #111;
}

.expert .content {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  flex-direction: row;
  margin-top: 40px;
}

.expert .content .box {
  width: 250px;
  margin: 30px;
  border: 15px solid #fff;
  box-shadow: 0 5px 35px rgba(0, 0, 0, 0.08);
}

.expert .content .box .imgbx {
  position: relative;
  width: 100%;
  height: 300px;
}

.expert .content .box .imgbx img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.testimonial {
  background: linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)),
    url(https://i.postimg.cc/k5dZs5jR/bg2.jpg);
  background-size: cover;
}

.white .titeText,
.white p {
  color: #fff;
}

.testimonial .content {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  flex-direction: row;
  margin-top: 40px;
}

.testimonial .content .box {
  width: 350px;
  margin: 20px;
  padding: 40px;
  background: #fff;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.testimonial .content .box img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  border-radius: 48%;
}

.testimonial .content .box .text {
  text-align: center;
}

.testimonial .content .box .text p {
  color: #666;
  font-style: italic;
}

.testimonial .content .box .text h2 {
  margin-top: 20px;
  color: #111;
  font-size: 1.5em;
  color: #ff0157;
  font-weight: 600;
}

.contactus {
  background: url(https://i.postimg.cc/4NmrNq7y/bg3.jpg);
  background-size: cover;
}

.contactform {
  padding: 75px 50px;
  background: #fff;
  box-shadow: 0 15px 50px rgba(0, 0, 0, 0.1);
  max-width: 500px;
  margin-top: 50px;
}

.contactform h3 {
  color: #111;
  font-size: 1.2em;
  margin-bottom: 20px;
  font-weight: 500;
}

.contactform .inputbox {
  position: relative;
  width: 100%;
  margin-bottom: 20px;
}

.contactform .inputbox input,
.contactform .inputbox textarea {
  width: 100%;
  border: 1px solid #555;
  border-radius: 10px;
  padding: 10px;
  color: #111;
  outline: none;
  font-size: 16px;
  font-weight: 300;
  resize: none;
}

.contactform .inputbox input[type="submit"] {
  font-size: 1em;
  color: #fff;
  background: #1c41ea;
  display: inline-block;
  text-transform: uppercase;
  text-decoration: none;
  letter-spacing: 2px;
  max-width: 100px;
  transition: 0.5s;
  font-weight: 500;
  border: none;
  cursor: pointer;
}

.footer {
  background: #ff0157;
  width: 100%;
  height: 70px;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.footer p {
  color: #fff;
  font-weight: 300;
}

.footer .icons a {
  color: #fff;
}

@media (max-width: 991px) {

  header,
  header .sticky {
    padding: 10px 20px;
  }

  header .navigation {
    display: none;
  }

  header .navigation.active {
    width: 100%;
    height: calc(100% - 68px);
    position: fixed;
    top: 68px;
    left: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    background: #fff;
  }

  header .navigation li {
    margin-left: 0;
  }

  header .navigation li a {
    color: #111;
    font-size: 1.6em;
  }

  .menutoggle {
    position: relative;
    width: 40px;
    height: 40px;
    background: url(https://i.postimg.cc/qq7WKZ1m/menu.png);
    background-size: 30px;
    background-repeat: no-repeat;
    background-position: center;
    cursor: pointer;
  }

  .menutoggle .active {
    background: url(https://i.postimg.cc/yNcK65Gc/close.png);
    background-size: 25px;
    background-repeat: no-repeat;
    background-position: center;
  }

  .sticky .menutoggle {
    filter: invert(1);
  }

  .navbar section {
    padding: 20px;
  }

  .banner .content h2 {
    font-size: 3em;
    color: #fff;
  }

  .row {
    flex-direction: column;
  }

  .row .col50 {
    position: relative;
    width: 100%;
  }

  .row .col50 .imgbx {
    height: 300px;
    margin-top: 20px;
  }

  .menu .content {
    margin-top: 20px;
  }

  .menu .content .box {
    margin: 10px;
  }

  .menu .content .box .imgbx {
    height: 260px;
  }

  .titeText {
    font-size: 1.8em;
    text-align: center;
  }

  .expert .content {
    margin-top: 20px;
  }

  .testimonial .content .box {
    margin: 10px;
    padding: 30px;
  }

  .expert .content .box {
    margin: 20px;
  }

  .contactform {
    padding: 40px 30px;
  }

  .sticky {
    padding: 10px 30px;
  }
}

.dashboard-header {
  width: 100%;
  overflow: hidden;
  height: var(--header-height);
  font-family: var(--primary-font);
  color: black;
}

.header__nav {
  height: 100%;
  display: flex;
  justify-content: space-between;
  padding: 0 100px;
  align-items: center;
}

.header__nav img {
  width: 25%;
  height: auto;
}

.header__menu {
  display: flex;
  list-style-type: none;
  gap: 30px;
  text-decoration: none;
  margin: 0;
}

.seperated-links {
  display: flex;
  gap: 15px;
}

.seperated-links a {
  color: var(--primary-font-color);
}

.auth-links {
  display: flex;
  gap: 5px;
  color: var(--secondary-font-color);
}

.auth-links li a {
  color: var(--secondary-font-color);
}

.header__nav ul li a {
  font-size: 19px;
  text-decoration: none;
}

.router-link-exact-active {
  font-weight: 600;
}
</style>
