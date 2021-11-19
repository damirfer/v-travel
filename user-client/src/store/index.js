import Vue from "vue";
import Vuex from "vuex";
import services from "./services";
import { stat } from "fs";

Vue.use(Vuex);

const state = {
  services,

  loadingActive: false,
  //Za day/CreateOrUpdate.vue test
  selectedDayCountry: [],
  bookingData: {}
};

export default new Vuex.Store({
  state,
  getters: {
    getBookingId: state => {
      return state.bookingData.BookingId;
    },
    getTourId: state => {
      return state.bookingData.TourId;
    },
    getSelectedDayCountry: state => {
      return state.selectedDayCountry;
    },
    getServicesTest: state => {
      return state.services.countryService.test();
    },
    getIsLoadingActive: state => {
      return state.loadingActive;
    }
  },
  mutations: {
    setSelectedDayCountry: (state, payload) => {
      state.selectedDayCountry = payload;
    },
    setBookingData: (state, payload) => {
        state.bookingData = payload;
    },
    setLoadingActive: (state, payload) => {
      state.loadingActive = payload;
    }
  }
});
