<template>
  <div id="schedule" class="clear-floats">
    <transition name="modal">
      <div
        v-if="isModalActive"
        :class="{ open: isModalActive }"
        class="open"
        id="place-modal"
      >
        <place-details
          v-if="isPlaceModalActive"
          @closePlaceModal="closePlaceModal"
          :placeId="placeId"
        ></place-details>

        <accommodation-details
          v-if="isAccommodationModalActive"
          @closePlaceModal="closePlaceModal"
          :accommodationId="accommodationId"
        ></accommodation-details>
      </div>
    </transition>

    <span class="center-line"></span>
    <app-schedule-single
      @placeClicked="placeClicked"
      @accommodationClicked="accommodationClicked"
      v-for="(item, i) in scheduleItems"
      :key="i"
      :item="item"
      :i="i + 1"
    ></app-schedule-single>
  </div>
</template>
<script>
import placeDetails from "../place/SingleDetails";
import accommodationDetails from "../accommodation/SingleDetails";
import ScheduleSingle from "./Single";
export default {
  props: ["scheduleItems"],
  components: {
    appScheduleSingle: ScheduleSingle,
    placeDetails: placeDetails,
    accommodationDetails: accommodationDetails,
  },
  data() {
    return {
      placeId: 0,
      accommodationId: 0,
      isModalActive: false,
      isAccommodationModalActive: false,
      isPlaceModalActive: false,
    };
  },
  methods: {
    placeClicked(placeId) {
      this.placeId = placeId;
      this.isAccommodationModalActive = false;
      this.isPlaceModalActive = true;

      this.isModalActive = true;
    },
    accommodationClicked(accommodationId) {
      this.accommodationId = accommodationId;
      this.isPlaceModalActive = false;
      this.isAccommodationModalActive = true;

      this.isModalActive = true;
    },
    closePlaceModal() {
      this.isModalActive = false;
    },
  },
};
</script>
<style scoped>
#place-modal {
  position: fixed;
  z-index: 30;
  height: auto;
  top: 0;
  height: 94vh;
  width: 100%;
  background-color: #f7f7f7;
  overflow-y: scroll;
  cursor: pointer;
}
#schedule {
  position: relative;
  height: 100vh;
  overflow-y: scroll;
  padding-bottom: 7vh;
}

/* .center-line::after{
  content:'';
    height: 20px;
    width: 20px;
    position: absolute;
    bottom: -10px;
    left: 50%;
    transform: translateX(-50%);
    border-radius: 50%;
    background: #0284a8;
    border: 2px solid #fff;
} */
.modal-enter-active,
.modal-leave-active {
  transition: all 0.5s;
}
.modal-enter, .modal-leave-to /* .list-leave-active below version 2.1.8 */ {
  opacity: 0;
  transform: translateY(30px);
}
</style>
