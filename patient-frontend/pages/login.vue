<template>
<div class="login">
  <div class="login-page">
    <p class="back-link"><NuxtLink to="/">Back to Homepage</NuxtLink></p>
    <h1 class="page-title">To log in, enter your email and password</h1>
    <form class="input-form" action="">
        <div>
            <label class="email-label" for="email">Email Address</label>
            <input class="email-input" type="email" id="email" placeholder="Email Address" />
        </div>

        <div>
            <label class="password-label" for="password">Password</label>
            <input class="password-input"  type="password" id="password" placeholder="Password" />
        </div>

        <button class="login-button">Continue</button>
    </form>
    <div class="border-line">
        <span>or</span>
    </div>

    <button class="google-button">
        <img src="@/assets/google.svg" alt="google">
        Continue with Google
    </button>
    <p class="redirect-link">New to PureLife? <NuxtLink to="/register"><span>Create an account</span></NuxtLink></p>

  </div>

</div>
</template>

<script>
export default {};
</script>

<style scoped>
.login {
    width: 100%;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
}

.login-page {
    position: relative;
    padding: 60px 40px;
    max-width: 500px;
    border: 1px solid #ebebeb;
    margin: auto;
    border-radius: 10px;
    box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 24px;
}

.page-title{
    font-weight: 500;
    font-size: 23px;
    color: var(--primary-font-color);
    margin-top: 20px;
}

.input-form {
    margin-top: 25px;
    display: flex;
    flex-direction: column;
    gap: 20px;
}

.input-form div {
    display: flex;
    flex-direction: column;
    gap: 5px;
}

.password-label, .email-label{
    font-size: 18px;
    font-weight: 500;
}

.password-input, .email-input {
    border: 1px solid #E2E6EB;
    padding: 18px 15px;
    margin-top: 5px;
    font-size: 18px;
}

.password-input::placeholder, .email-input::placeholder {
   color: var(--primary-font-color);
   font-size: 16px;
   font-weight: 500;
}

.login-button{
    margin-top: 10px;
    padding: 16px 0;
    font-size: 16px;
    font-weight: 500;
    border: none;
    outline: none;
    cursor: pointer;
    background: rgba(183, 203, 233, 0.7);
    transition: background-color 0.3s ease-in;
}

.login-button:hover{
    transition: background-color 0.3s ease-in;
    background: rgba(183, 203, 233, 1);
}

.border-line {
  display: flex;
  align-items: center;
  margin-top: 20px;
}

.border-line::before,
.border-line::after {
  content: "";
  flex-grow: 1;
  border-bottom: 1px solid #E2E6EB;
}

.border-line::before {
  margin-right: 10px;
}

.border-line::after {
  margin-left: 10px;
}

.border-line span {
  padding: 0 10px;
  color: var(--primary-font-color);
  font-size: 16px;
  font-weight: 500;
}

.google-button{
    margin-top: 20px;
    position: relative;
    width: 100%;
    border: 1px solid #00234B;
    padding: 15px;
    background-color: white;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 14px;
    font-weight: 500;
}

.google-button img{
    position: absolute;
    left: 10px;
}

.redirect-link{
    font-size: 15px;
    font-weight: 500;
    color: var(--primary-font-color);
    text-align: center;
    position: absolute;
    bottom: 20px;
    left: 50%;
    transform: translateX(-50%);
}

.redirect-link a{
    color: #1F6CD6;
    border-bottom: 1px solid #1F6CD6;
    text-decoration: none;
}

.back-link a{
    font-size: 14px;
    color: var(--primary-font-color);
    width: fit-content;
    border-bottom: 1px solid #E2E6EB;   
    transition: border 0.3s; 
    text-decoration: none;
}

.back-link a:hover{
    cursor: pointer;
    color: black;
    border-bottom: 1px solid #606061;  
    transition: border 0.3s; 
}

@media screen and (max-width: 500px) {
    .login-page {
        height: 100vh;
    }
}

</style>
