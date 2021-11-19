<template>
  <div id="footer-navigation" :style="{ left: navLeftInPx }">
    <router-link @click="test" to="/TourOverview">
      <div>
        <img src="../assets/menu/travel.png" alt="Tour overview" />
        <!-- <img v-else src="https://png.icons8.com/ios/100/0284a8/waypoint-map-filled.png" alt="Tour overview"> -->
      </div>
    </router-link>
    <router-link to="/Explore">
      <div>
        <img src="../assets/menu/explore.png" />
        <!-- <img v-else src="https://png.icons8.com/ios/100/0284a8/search-filled.png"> -->
      </div>
    </router-link>
    <router-link to="/Chat">
      <div>
        <img src="../assets/menu/chat.png" />
        <!-- <img v-else src="https://png.icons8.com/ios/100/0284a8/sent-filled.png"> -->
      </div>
    </router-link>
    <router-link to="/Transport">
      <div>
        <img src="../assets/menu/bus.png" />
        <!-- <img v-else src="https://png.icons8.com/ios/100/0284a8/trolleybus-filled.png"> -->
      </div>
    </router-link>
    <router-link to="/TourInfo">
      <div>
        <img src="../assets/menu/info.png" />
        <!-- <img v-else src="https://png.icons8.com/ios/100/0284a8/info-filled.png"> -->
      </div>
    </router-link>
  </div>
</template>

<script>
export default {
  props: ["navLeftInPx"],
  data() {
    return {};
  },
  methods: {
    test(e) {
      console.log(e);
    },
  },
  created() {
    console.log(this.$route.path);
  },
};
</script>

<style scoped>
#footer-navigation {
  background-color: #f7f7f7;
  height: 6vh;
  width: 100vw;
  position: fixed;
  bottom: -2px;
  transition: left 0.4s;
  box-shadow: 0px 2px 25px -10px rgba(0, 0, 0, 0.4);
  z-index: 50;
}

#footer-navigation::after {
  content: "";
  display: block;
  clear: both;
}

a {
  width: 20%;
  height: 100%;
  float: left;
}

#footer-navigation div {
  text-align: center;
  height: 100%;
  position: relative;
}

#footer-navigation div img {
  width: 28px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

a.router-link-exact-active {
  border-top: 2px solid #0284a8;
}
</style>
