import Vue from "vue";
import Vuex from "vuex";
import services from "./services";
import { COMMIT_LOADER } from "./mutation-types";

Vue.use(Vuex);

const state = {
  services,
  dataLoader: false,

  //Za day/CreateOrUpdate.vue test
  selectedDayCountry: [],
  breadcrumb: [{}, {}],
};

export default new Vuex.Store({
  state,
  getters: {
    getSelectedDayCountry: state => {
      return state.selectedDayCountry;
    }
  },
  mutations: {
    setSelectedDayCountry: (state, payload) => {
      state.selectedDayCountry = payload;
    },
    [COMMIT_LOADER](state,payload) {
      state.dataLoader = payload;
    },
  }
});
