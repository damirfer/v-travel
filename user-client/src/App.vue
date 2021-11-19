<template>
<div id="app">
    <div v-if="isLoadingActive" class="loading-icon">
        <img src="./assets/loading-icon.gif" alt="Loading icon">
    </div>
        <router-view></router-view>
        <app-footer-navigation 
        v-if="!isLogin" 
        :navLeftInPx="0"></app-footer-navigation>
    </div>
</template>

<script>
import Navigation from "./components/Navigation.vue";
export default {
    components: {
        appFooterNavigation: Navigation
    },
    data() {
        return {
            isLogin: true
        };
    },
    computed:{
      isLoadingActive(){
        return this.$store.getters.getIsLoadingActive;
      }
    },
    watch: {
        $route(to, from) {
            if (to.name != "Login") this.isLogin = false;
            else this.isLogin = true;
        }
    },
    created() {
        if (this.$route.name != "Login") this.isLogin = false;
    }
};
</script>

<style>
* {
    box-sizing: border-box;
}

body {
    margin: 0;
    overflow: hidden;
    font-family: 'Roboto Slab', serif;
    background-color: #f7f7f7;
}

h1, h2, h3, h4{
    font-family: 'Montserrat', sans-serif;
}

#app {
    height: 100vh;
    overflow: hidden;
    position: relative;
}

.text-right {
    text-align: right;
}

.text-left {
    text-align: left;
}

.text-center {
    text-align: center;
}

.clear-floats::after {
    content: "";
    display: block;
    clear: both;
}

.left-half {
    float: left;
    width: 50%;
}

.relative {
    position: relative;
}
.loading-icon {
  position: absolute;
  top: 50%;
  left: 50%;
  z-index: 100;
  transform: translate(-50%, -50%);
}
.loading-icon img {
  width: 100px;
}
</style>
