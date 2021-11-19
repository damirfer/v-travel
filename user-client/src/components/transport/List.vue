<template>
    <div class="transport-container">
        <app-transport-single
        v-for="transport in transports"
        :key="transport.TransportId"
        :transports="transport"></app-transport-single>
        <div style="height: 8vh"></div>
    </div>
</template>
<script>
import TransportSingle from "./Single";
export default {
  components: {
    appTransportSingle: TransportSingle
  },
  data() {
    return {
      transports: [],
      selectedCity: 0,
    };
  },
  methods: {
    getTransportsByTour() {
      this.$store.commit("setLoadingActive", true);
      this.$store.state.services.transportService
        .getByTour(localStorage.getItem("tourId"))
        .then(response => {
          this.$store.commit("setLoadingActive", false);
          this.transports = response.data;
        }).catch(err => {
          this.$store.commit("setLoadingActive", false);
        });
    },
  },
  created() {
    this.getTransportsByTour();
  },
};
</script>
<style scoped>
.transport-container{
  padding: 0 15px;
  height: 100vh;
  overflow-y: scroll;
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
