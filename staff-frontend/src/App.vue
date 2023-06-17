<template>
  <div :style="{ 'padding-left': ['/login', '/404'].includes($route.path) ? '0px' : '' }" :class="['flexible-content', {'flexible-content-minimized': minimized}]">
    <!-- Sidebar -->
    <div v-if="user.isAuthenticated" class="sidebar-fixed position-fixed"
       :class="{ 'toggleSidebar': !minimized, 'shrink-sidebar': minimized }"
       @mouseover="minimized = false"
       @mouseleave="minimized = true">
      <a class="logo-wrapper"><img alt="" class="img-fluid" src="./assets/purelife color.png"/></a>
      <mdb-list-group class="list-group-flush">
        <router-link v-for="(route, index) in filteredRoutes" :key="index" :to="route.path" @click.native="activeItem = index + 1">
          <mdb-list-group-item v-if="!route.authRequired" :action="true" :class="{ active: activeItem === index + 1 }">
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
      <div :class="{'mt-2 p-5': !['/login', '/404'].includes($route.path)}">
        <router-view></router-view>
      </div>
    </main>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbListGroup,
  mdbListGroupItem,
  waves
} from "mdbvue";

import routes from './helpers/sidebarRoutes'
import navbarRoutes from './helpers/navbarRoutes'
import { mapGetters } from 'vuex'
import { getLocalStorage } from "./helpers/auth"
import {createTerminet} from './staff-sdk/terminet'
export default {
  name: "AdminTemplate",
  components: {
    mdbListGroup,
    mdbListGroupItem,
    mdbIcon,
  },
  data() {
    return {
      activeItem: 1,
      minimized: true,
      terminet: [],
      userId: null
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
      user: 'getUser',
    }),
    filteredRoutes() {
      const userRole = this.user?.user?.data?.roletId;
      return routes.filter((route) => route.role.includes(userRole));
    },

  },
  async mounted() {
    const currentDate = new Date(); // Get the current date
    const year = currentDate.getFullYear();
    let month = currentDate.getMonth() + 1; // Months are zero-indexed, so add 1
    month = month < 10 ? `0${month}` : month; // Pad the month with leading zero if needed
    let day = currentDate.getDate();
    day = day < 10 ? `0${day}` : day; // Pad the day with leading zero if needed

    this.selectedDate = `${year}-${month}-${day}`;


    if(this.$route.matched.length){
      this.activeItem = this.$route.matched[0].props.default.page;
    }

    this.userId = getLocalStorage()
    // eslint-disable-next-line no-console
    console.log(this.userId)
    await this.fetchTerminet();
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
    },
    async fetchTerminet() {
        // eslint-disable-next-line no-console
        if(this.userId) {
          const response = await this.$store.dispatch('fetchTerminetByDateAndStaff', {date: this.selectedDate, id: this.userId.id})
          this.terminet = response.data
        }

        if(this.terminet.length == 0) {
          await this.createNewTerminet()
        }
        // eslint-disable-next-line no-console
    },
    async createNewTerminet() {
      if(this.userId) {
        // eslint-disable-next-line no-console
        console.log(this.userId.id)
        try {
          await createTerminet(this.userId.id)
        } catch (err) {
          // eslint-disable-next-line no-console
          console.log(err)
        }
      }
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
  height: 100%;
  overflow: hidden;
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
