<template>
    <div id="places-container">
        <app-place-single
        v-for="place in places"
        :key="place.CityName"
        :placesByCity="place"></app-place-single>
        <div style="height: 8vh"></div>
    </div>
</template>
<script>
import PlaceSingle from "./Single";
export default {
  components: {
    appPlaceSingle: PlaceSingle
  },
  data() {
    return {
      places: [],
    };
  },
  created(){
    this.getPlacesByTour();
  },
  methods: {
    getPlacesByTour() {
      this.$store.commit("setLoadingActive", true);
      this.$store.state.services.placeService
        .getByTour(localStorage.getItem("tourId"))
        .then(response => {
          this.$store.commit("setLoadingActive", false);
          console.log(response.data)
          this.places = response.data;
        }).catch(err => {
          this.$store.commit("setLoadingActive", false);
        });
    },
  },
};
</script>
<style scoped>
#places-container {
  height: 100vh;
  padding: 10px;
  overflow-y: scroll !important;
  position: relative;
  padding-bottom: 8vh;
}
.cities-dropdown {
  padding: 10px;
}
.cities-dropdown select {
  background-color: transparent;
  border: none;
  border-bottom: 1px solid black;
  display: block;
  width: 85%;
  margin: 10px auto;
}
</style>
