<template>
  <div :style="{ 'padding-left': $route.path.includes('login') ? '0px' : '' }" :class="['flexible-content', {'flexible-content-minimized': minimized}]">
    <!--Navbar-->
    <mdb-navbar v-if="user.isAuthenticated" :class="['flexible-navbar white', {'flexible-navbar-minimized white': minimized}]" light position="top" scrolling>
      <mdb-navbar-brand href="https://mdbootstrap.com/docs/vue/" target="_blank">MDB</mdb-navbar-brand>
      <mdb-navbar-toggler>
        <mdb-navbar-nav left>
          <mdb-nav-item v-for="(route, index) in navbarRoutes" :key="index" :to="route.link" waves-fixed :class="{ 'active': isActive(route.link) }">
            <mdb-icon v-if="route.icon" :icon="route.icon" class="text-black"></mdb-icon>
            {{ route.name }}
          </mdb-nav-item>
        </mdb-navbar-nav>
        <mdb-navbar-nav right>
          <mdb-nav-item href="#!" waves-fixed
            ><mdb-icon fab class="text-black" icon="facebook-square"
          /></mdb-nav-item>
          <mdb-nav-item href="#!" waves-fixed
            ><mdb-icon fab icon="twitter"
          /></mdb-nav-item>
          <mdb-nav-item
            href="https://github.com/mdbootstrap/bootstrap-material-design"
            waves-fixed
            class="border border-light rounded mr-1"
            target="_blank"
            ><mdb-icon fab icon="github" class="mr-2" />MDB GitHub
          </mdb-nav-item>
          <mdb-nav-item
            href="https://mdbootstrap.com/products/vue-ui-kit/"
            waves-fixed
            class="border border-light rounded"
            target="_blank"
            ><mdb-icon icon="gem" far class="mr-2" />Go Pro
          </mdb-nav-item>
        </mdb-navbar-nav>
      </mdb-navbar-toggler>
    </mdb-navbar>
    <!--/.Navbar-->
    <!-- Sidebar -->
    <div v-if="user.isAuthenticated" class="sidebar-fixed position-fixed"
       :class="{ 'toggleSidebar': !minimized, 'shrink-sidebar': minimized }"
       @mouseover="minimized = false"
       @mouseleave="minimized = true">
      <a class="logo-wrapper"><img alt="" class="img-fluid" src="./assets/logo-mdb-vue-small.png"/></a>
      <mdb-list-group class="list-group-flush">
        <router-link v-for="(route, index) in routes" :key="index" :to="route.path" @click.native="activeItem = index + 1">
          <mdb-list-group-item v-if="!route.authRequired" :action="true" :class="{ active: activeItem === index + 1, 'custom-list-group-item': index === 4 }">
            <mdb-icon :icon="route.icon" class="mr-3 icon-shrink"/>
            <span>{{ route.name }}</span>
          </mdb-list-group-item>
        </router-link>
        <router-link to="/login" @click.native="signOut">
          <mdb-list-group-item v-if="user.isAuthenticated" :action="true">
            <mdb-icon icon="sign-out-alt" class="mr-3 icon-shrink"/>
            <span>Sign Out</span>
          </mdb-list-group-item>
        </router-link>
      </mdb-list-group>
    </div>
    <!-- /Sidebar  -->
    <main>
      <div :class="{'m-5 p-5': !this.$route.path.includes('login')}">
        <router-view></router-view>
      </div>
    </main>
  </div>
</template>

<script>
import {
  mdbNavbar,
  mdbNavbarBrand,
  mdbNavItem,
  mdbNavbarNav,
  mdbNavbarToggler,
  mdbIcon,
  mdbListGroup,
  mdbListGroupItem,
  waves
} from "mdbvue";

import routes from './helpers/sidebarRoutes'
import navbarRoutes from './helpers/navbarRoutes'
import { mapGetters } from 'vuex'

export default {
  name: "AdminTemplate",
  components: {
    mdbNavbar,
    mdbNavbarBrand,
    mdbNavItem,
    mdbNavbarNav,
    mdbNavbarToggler,
    mdbListGroup,
    mdbListGroupItem,
    mdbIcon,
  },
  data() {
    return {
      activeItem: 1,
      minimized: true
    };
  },
  computed: {
    routes() {
      return routes
    },
    navbarRoutes() {
      return navbarRoutes
    },
    ...mapGetters({
      user: 'getUser'
    })
  },
  mounted() {
    if(this.$route.matched.length){
      this.activeItem = this.$route.matched[0].props.default.page;
    }
  },
  mixins: [waves],
  methods: {
    toggleSidebar() {
      this.minimized = !this.minimized;
    },
    isActive(link) {
      return link === this.$route.path
    },
    signOut() {
      this.$store.dispatch('signOut')
    }
  },
};
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');
.navbar-light .navbar-brand {
  margin-left: 15px;
  color: #2196f3 !important;
  font-weight: bolder;
}
</style>

<style scoped>

main {
  font-family: 'Poppins', Helvetica, sans-serif;
  background-color: #ededee;
}

.flexible-content {
  transition: padding-left 0.3s;
  padding-left: 270px;
}

.flexible-content-minimized {
  transition: padding-left 0.3s;
  padding-left: 80px;
}


.flexible-navbar {
  transition: padding-left 0.5s;
  padding-left: 270px;
}

.flexible-navbar-minimized {
  transition: padding-left 0.5s;
  padding-left: 80px;
}

.sidebar-fixed {
  left: 0;
  top: 0;
  height: 100vh;
  width: 80px;
  box-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
  z-index: 1050;
  background-color: #fff;
  padding: 1.5rem;
  padding-top: 0;
}

.sidebar-fixed .logo-wrapper img {
  width: 100%;
  padding: 2.5rem;
}

.sidebar-fixed .list-group-item {
  display: block !important;
  transition: background-color 0.3s;
}

.sidebar-fixed .list-group .active {
  box-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
  border-radius: 5px;
}

.toggleSidebar {  
  width: 270px;
  transition: width 0.3s;
}

.shrink-sidebar {  
  width: 80px;
  transition: width 0.3s;
}

.shrink-sidebar .list-group-item span {
  display: none;
}

.shrink-sidebar .list-group-item {
  padding-left: 8px;
}

@media (max-width: 1199.98px) {
  .flexible-content {
    padding-left: 80px;
  }
  .flexible-navbar {
    padding-left: 80px;
  }
}
</style>
