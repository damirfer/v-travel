import Vue from 'vue'
import App from './App.vue'
import VueChartkick from 'vue-chartkick'
import Chart from 'chart.js'


// Vue.use(Vuetify, {
//   theme: {
//     /*primary: '#00796B',*/
//     //primary: '#0284A8',
//     primary: '#8b0582',
//     /*secondary: '#008668',*/
//     secondary: '#027595',
//     accent: '#8BC34A',
//     error: '#E64A19',
//     info: '#0097A7',
//     success: '#388E3C',
//     warning: '#FBC02D',
//     alternative1: '#0293bb',
//     alternative2: '#02a2cf'
//   }
// });

import Vuetify from 'vuetify';
import { theme } from './plugins/vuetify'

// GLOBAL COMPONENTS

import MainHeader from '@/components/admin/MainHeader.vue'

Vue.component('main-header', MainHeader);

Vue.use(Vuetify,{
    theme: {
      primary: '#0284a8',
      secondary: '#000057',
      accent: '#8BC34A',
      error: '#E64A19',
      info: '#0097A7',
      success: '#388E3C',
      warning: '#FBC02D',
      alternative1: '#0293bb',
      alternative2: '#02a2cf'
    }
  });

Vue.use(VueChartkick,{
  adapter: Chart
});

//Router
import { router } from './router'

//Vuex
import store from './store'

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
