import Vue from "vue";
import App from "./App";
import Vue2TouchEvents from "vue2-touch-events";
// import VueIdb from "vue-idb";

// Vue.use(VueIdb);
// const idb = new VueIdb({
//   version: 1,
//   database: "eTravel-db",
//   schemas: [
//     { tests: "id, title, created_at, updated_at" }
//   ]
// });

Vue.use(Vue2TouchEvents);

Vue.config.productionTip = false;

//Router
import router from "./router";


//Vuex
import store from "./store";

new Vue({
  el: "#app",
  router,
  store,
  // idb: idb,
  render: h => h(App)
});
