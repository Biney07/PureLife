<template>
  <div class="container-fluid login-container">
    <div class="row h-100">
      <div class="col-md-6 d-flex align-items-end justify-content-center p-5" style="background-color: rgb(241,201,137)">
        <!-- SVG image -->
        <img src="../../assets/staff.svg" alt="Your image" class="img-fluid staff-img sm-w-25">

        <!-- Logo image -->
        <img src="../../assets/purelife color.png" alt="Logo image" class="img-fluid logo-img">
      </div>
      <div class="col-md-5 m-auto p-4">
        <h2 class="title-text">Welcome to PureLife Hospital</h2>
        <p class="instructions-text">Please make sure to use the email and password provided to you by the <strong>administrator</strong></p>
        <!-- Login form -->
        <form @keydown.enter="login">
          <!-- Email input -->
          <div class="form-outline mb-4">
            <input v-model="formData.emailAddress" type="email" id="form1Example1" class="form-control" placeholder="example@purelife.net" />
            <label class="form-label font-weight-normal" for="form1Example1">Email address</label>
          </div>

          <!-- Password input -->
          <div class="form-outline mb-4">
            <input v-model="formData.password" placeholder="your password" type="password" id="form1Example2" class="form-control" />
            <label class="form-label font-weight-normal" for="form1Example2">Password</label>
          </div>
        </form>
        <!-- Submit and forgot password buttons -->
          <div class="row mb-4 align-items-center">
            <div class="col">
              <p class="sign-in-button" @click="login">Sign in</p>
            </div>
            <div class="col text-right">
              <p class="forgot-password">Forgot password?</p>
            </div>
          </div>
      </div>
    </div>
  </div>
</template>



<script>
export default {
  data() {
    return {
      formData: {
        emailAddress: null,
        password: null
      }
    }
  },
  methods: {
    async login() {
      // eslint-disable-next-line no-console
      try {
        await this.$store.dispatch('loginUser', this.formData)
      } catch (err) {
        // eslint-disable-next-line no-console
        console.log(err);
      } finally {
        this.$router.push({ path: '/' })
      }
    }
  }
}
</script>

<style>
.login-container {
  height: 100vh !important;
  background: white !important;
}

.staff-img{
  width: 80%;
}

.logo-img {
  position: absolute; 
  top: 15%; 
  margin: auto;
  width: 60%;
}

.title-text{
  font-weight: 500;
}

.instructions-text{
  font-size: 12px;
  padding: 5px 10px;
  background: rgb(107,98,255, 0.9);
  color: white;
  border-radius: 8px;
  width: fit-content;
  margin-bottom: 6%;
}

@media (max-width: 950px) { 
  .staff-img{
    width: 100%;
  }

  .logo-img {
    position: absolute; 
    top: 15%; 
    margin: auto;
    width: 70%;
  }
}


@media (max-width: 767.98px) { 
  .staff-img{
    width: 45%;
  }

  .logo-img {
    position: absolute; 
    top: 10%; 
    margin: auto;
    width: 35%;
  }
}

@media (max-width: 620px) { 
  .staff-img{
    width: 50%;
  }

  .logo-img {
    position: absolute; 
    top: 15%; 
    margin: auto;
    width: 40%;
  }
}

@media (max-width: 500px) { 
  .staff-img{
    width: 70%;
  }
}

input.form-control{
  border: none;
  background: #f3f6f9;
  border-radius: 15px;
  height: 60px;
  transition: background-color 0.3s;
  padding: 0 15px;
}

.form-control:focus {
  border: none;
  outline: none;
  background: #ebedf3;
  box-shadow: none;
  transition: background-color 0.3s;
  padding: 0 15px;
}

.sign-in-button{
  background: rgb(107,98,255, 0.9);
  width: fit-content;
  padding: 15px 40px;
  border-radius: 10px;
  color: white;
  transition: background-color 0.3s;
  font-size: 1rem;
  font-weight: 500;
}

.sign-in-button:hover{
  background: rgb(107,98,255, 1);
  transition: background-color 0.3s;
  cursor: pointer;
}

.forgot-password{
  color: rgb(80, 76, 76);
  transition: color 0.3s;
}

.forgot-password:hover{
  color: black;
  cursor: pointer;
  transition: color 0.3s;
}

@media (max-width: 860px) { 
  .sign-in-button{
    font-weight: 500;
    padding: 15px 20px;
  }
}
</style>
