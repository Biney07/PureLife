<template>
  <header>
    <nav class="header__nav">
        <img src="../assets/purelife color.png" alt="purelife-logo">
        <ul class="header__menu" data-aos="fade-down">
            <div class="seperated-links">
                <li>
                    <NuxtLink to="/contact">Kontakti</NuxtLink>
                </li>
                <li>
                    <NuxtLink to="/about">Rreth Nesh</NuxtLink>
                </li>
            </div>
            <div class="auth-links">
                <li>
                    <NuxtLink to="/login">Login</NuxtLink>
                </li>
                <li>
                    /
                </li>
                <li>
                    <NuxtLink to="/register">Register</NuxtLink>
                </li>
            </div>
        </ul>
    </nav>
  </header>
</template>

<script>
export default {
}
</script>

<style scoped>
header{
    width: 100%;
    background: var(--primary-color);
    overflow: hidden;
    height: var(--header-height);
}

.header__nav {
    height: 100%;
    display: flex;
    justify-content: space-between;
    padding: 0 100px;
    align-items: center;
}

.header__nav img {
    width: 20%;
    height: auto;
}

.header__menu {
    display: flex;
    list-style-type: none;
    gap: 30px;
    text-decoration: none;
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

.header__nav ul li a{
    font-size: 16px;
    text-decoration: none;
}

.router-link-exact-active {
    font-weight: 600;
}
</style>