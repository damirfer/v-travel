<template>
  <div>
    <router-view :key="$route.path"></router-view>
  </div>
</template>

<script>
export default {
  name: "app",
  data() {
    return {};
  },
};
</script>
<style>
@import url("https://fonts.googleapis.com/css?family=Montserrat:300,:400,:500,:700");

#app {
  position: relative;
}
.theme--light.application {
  background-color: white !important;
}
.height-auto {
  height: auto !important;
}
.clearAfterFloats::after {
  content: "";
  display: block;
  clear: both;
}
.left {
  float: left;
}
.inline-block {
  display: inline-block;
}
.fullHeight {
  height: 100%;
}
.v-tabs__items {
  height: 100%;
}
.text-center {
  text-align: center !important;
}
.text-left {
  text-align: left !important;
}
.text-right {
  text-align: right !important;
}
.create-search-button {
  display: grid;
  align-content: center;
  margin-left: 20px;
  justify-content: right;
  margin-right: 20px;
}

.search-header {
  display: grid;
  align-content: center;
  border-top: 2px solid #aaa;
  background-color: white;
  border-bottom: 1px solid #aaa;
  margin-bottom: 5px;
  padding-top: 20px;
}

.navigation-bottom {
  font-family: "Montserrat", sans-serif;
  position: fixed;
  bottom: 0;
  width: 100%;
  font-size: 11px;
  padding: 7px 0;
  background-color: #0284a8;
  color: #f7f7f7;
  text-align: center;
}

.modal-header {
  display: grid;
  grid-template-columns: 1fr 10fr 1fr;
  grid-template-rows: 1;
  background-color: #0284a8;
  color: white;
  text-transform: uppercase;
  box-shadow: inset 0 -6px 20px -10px rgba(0, 0, 0, 0.6);
  font-family: "Montserrat", sans-serif;
  font-size: 14px;
}

.modal-logo {
  width: 90%;
}

.modal-title {
  font-weight: 400;
  margin-left: 20px;
}

.modal-actions {
  position: relative;
  width: 100%;
  height: 50px;
}

.modal-buttons {
  right: 0;
  bottom: 0;
}

.modal-close-icon {
  right: 0;
}

.modal-contet {
  margin: 25px 0;
}

.v-tabs__bar {
  border-radius: 10px 10px 0 0;
}

.edit-icons {
  margin: 0 15px;
}
.edit-icons-last {
  margin-right: 0;
}

/*SCHEDULE HEIGHT FIX*/
.v-window {
  height: 100% !important;
}
.v-window__container {
  height: 100%;
}

.section-container {
  overflow-y: scroll;
}

.section-title {
  text-transform: uppercase;
  color: #0284a8;
  margin-bottom: 20px;
}
.action-icon-lg {
  width: 35px;
  cursor: pointer;
  margin: 0 10px;
}

.headline {
  text-transform: uppercase;
  color: #0284a8;
  font-size: 18px !important;
}
.primary-c {
  color: #0284a8;
}
/* ABOVE 960px */
@media (min-width: 960px) {
  .pr-md-1 {
    padding-right: 4px;
  }
  .pl-md-1 {
    padding-left: 4px;
  }
}
</style>
