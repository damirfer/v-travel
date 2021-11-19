<template>
  <div>
    <!-- <v-toolbar-side-icon class="toolbar-side-icon" @click.stop="drawer = !drawer"></v-toolbar-side-icon> -->
    <v-navigation-drawer v-model="drawer" fixed app style="z-index: 10">
      <div class="logo-holder">
        <div class="logo-holder--left">
          <!-- <img src="@/assets/sighter_logo.png" alt="sighter logo" /> -->
        </div>
        <div class="logo-holder--right">
          <!-- <a href="https://sighter.co" target="_blank">sighter.co</a>
          <a href="mailto:support@sighter.co">support@sighter.co</a> -->
        </div>
      </div>

      <v-list class="no-background">
        <v-list-tile v-for="i in item" :key="i.title" :to="i.to">
          <v-list-tile-action>
            <v-icon>{{ i.action }}</v-icon>
          </v-list-tile-action>

          <v-list-tile-content>
            <v-list-tile-title>{{ i.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>

        <v-list-group
          v-for="item in items"
          v-model="item.active"
          :key="item.title"
          :prepend-icon="item.action"
          no-action
        >
          <v-list-tile slot="activator">
            <v-list-tile-content>
              <v-list-tile-title>{{ item.title }}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>

          <v-list-tile
            v-for="subItem in item.items"
            :key="subItem.title"
            :to="subItem.to"
          >
            <v-list-tile-content>
              <v-list-tile-title>{{ subItem.title }}</v-list-tile-title>
            </v-list-tile-content>

            <v-list-tile-action>
              <v-icon>{{ subItem.action }}</v-icon>
            </v-list-tile-action>
          </v-list-tile>
        </v-list-group>
      </v-list>
      <!-- <div class="navigation-bottom">
          <span>Sighter product family - Sighter &copy; 2019</span>
      </div>-->
    </v-navigation-drawer>
    <!-- LOADER OVDJE -->
    <v-content class="content-main">
      <div v-if="isLoaderActive" class="loader-container">
        <img id="loader" src="@/assets/loader.gif" alt="Loading..." />
        <p>PLEASE WAIT</p>
      </div>
      <main-header @drawerToggle="drawer = !drawer"></main-header>
      <v-container fluid fill-height>
        <v-layout justify-center>
          <v-flex text-xs-center>
            <router-view></router-view>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </div>
</template>

<script>
export default {
  data() {
    return {
      drawer: null,
      //Navigation list
      items: [],
      item: [
        {
          action: "dashboard",
          title: "Dashboard",
          to: "/AdminHome/Dashboard",
        },
        {
          action: "card_travel",
          title: "Booking",
          to: "/AdminHome/Booking",
        },
        {
          action: "map",
          title: "Tour",
          to: "/AdminHome/Tour",
        },
        {
          action: "public",
          title: "Countries",
          to: "/AdminHome/Countries",
        },
        {
          action: "account_balance",
          title: "Cities",
          to: "/AdminHome/Cities",
        },
        {
          action: "place",
          title: "Places",
          to: "/AdminHome/Places",
        },
        {
          action: "hotel",
          title: "Accomodation",
          to: "/AdminHome/Accommodations",
        },
        {
          action: "local_taxi",
          title: "Transport",
          to: "/AdminHome/Transport",
        },
        {
          action: "supervised_user_circle",
          title: "Tour Leaders",
          to: "/AdminHome/Guides",
        },
      ],
    };
  },
  computed: {
    isLoaderActive() {
      return this.$store.state.dataLoader;
    },
  },
};
</script>
<style>
.v-navigation-drawer {
  font-family: "Montserrat", sans-serif;
  font-weight: 500;
  background-color: #fbfbfb !important;
  padding-top: 40px;
  /* box-shadow: inset -10px 0 20px -14px rgba(0, 0, 0, 0.6); */
}

.v-navigation-drawer .v-list {
  background: transparent;
}
.toolbar-side-icon {
  position: absolute;
  left: 2px;
  top: 5px;
  z-index: 9999;
}
.logo-holder {
  position: relative;
  margin-bottom: 20px;
  text-align: center;
  padding-bottom: 20px;
  width: 100%;
  /*border-bottom: 1px solid black;*/
}
.logo-holder--left {
  position: relative;
  width: 25%;
  /* border-right: 1px solid #ccc; */
  box-sizing: border-box;
  left: 0;
}
.logo-holder--left img {
  width: 70%;
  margin-top: 10px;
}
.logo-holder--right {
  position: absolute;
  text-align: left;
  box-sizing: border-box;
  right: 35px;
  top: 15px;
}
.logo-holder--right a {
  text-decoration: none;
  color: #444;
  display: block;
}

.v-list__tile__title {
  font-weight: 700;
}

.content-main {
  margin-top: 50px;
  height: 90vh;
}
.loader-container {
  position: absolute;
  width: 100%;
  height: 100%;
  background-color: #fafafa;
  z-index: 1999;
}
.loader-container p {
  color: #0284a8;
  text-transform: uppercase;
  position: absolute;
  top: 60%;
  left: 50%;
  transform: translateX(-50%);
  font-weight: 600;
}
#loader {
  width: 100px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>