<template>
<div>
    <v-dialog v-model="dayDialog" max-width="45%" style="right:0;">
        <v-card>
            <v-card-title class="card-title">
                <p v-if="isUpdate" class="primary-c py-2">Edit day</p>
                <p v-if="!isUpdate" class="primary-c py-2">Create day</p>
            </v-card-title>
            <v-card-text class="p-0">
                <app-day 
                class="p-0"
                :isDialogClosed="dayDialog" 
                @updateDay="updateDay" 
                @closeDialog="closeDialog" 
                :dayId="selectedDay" 
                :tourId="tour.TourId" 
                :isUpdate="isUpdate"></app-day>
            </v-card-text>
        </v-card>
    </v-dialog>
    <v-container>
        <v-layout row wrap>
          <div class="days-slider">
            <v-carousel height="auto" hide-delimiters :continuous="false" :cycle="false" @input="carouselToggle">
              <v-carousel-item
                v-for="(item,i) in tour.Days"
                :key="i">
                <span class="overview-title">Day {{ i + 1 }}</span>
                </v-carousel-item>
            </v-carousel>
          </div>
          <v-carousel v-model="carouselIdex" id="days-carousel" height="auto" :show-arrows="false" hide-delimiters :continuous="false" :cycle="false">
            <v-carousel-item
              v-for="(item,i) in tour.Days"
              :key="i">
                <app-day-tabs
                  class="day-tab"
                  :day="item"
                  @editDay="editDay"></app-day-tabs>
            </v-carousel-item>
          </v-carousel>
          

        </v-layout>
    </v-container>
</div>
</template>

<script>
import {mapMutations} from "vuex";
import DayTabs from "./day/Tabs";
import dayCU from "./day/CreateOrUpdate";
export default {
  components: {
    appDay: dayCU,
    appDayTabs: DayTabs
  },
  data() {
    return {
      tour: {},
      dayDialog: false,
      isUpdate: false,
      selectedDay: null,
      carouselIdex: 0,
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    getTour() {
      this.loader(true);
      this.$store.state.services.tourService
        .get(this.$route.params.tourId)
        .then(response => {
          this.tour = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    editDay(dayId) {
      this.isUpdate = true;
      this.dayDialog = true;
      this.selectedDay = dayId;
    },
    createDay() {
      this.isUpdate = false;
      this.dayDialog = true;
    },
    closeDialog() {
      this.dayDialog = false;
    },
    updateDay(dayId) {
      this.loader(true);
      var updatedDay = {};
      this.$store.state.services.dayService.getListItem(dayId).then(response => {
        updatedDay = response.data;
        this.isUpdate = false;
        var i = 0;
        for (i; i < this.tour.Days.length; i++) {
          if (this.tour.Days[i].DayId == dayId) {
            this.tour.Days[i] = updatedDay;
            break;
          }
        }
      })
      .finally(()=>{
        this.loader(false);
      });

    },
    carouselToggle(index) {
      this.carouselIdex = index;
    }
  },
  created() {
    this.getTour();
  }
};
</script>

<style scoped>
.overview-title {
  font-family: "Montserrat", sans-serif;
  margin: 20px 0 30px 0;
  color: #0284a8 !important;
  text-transform: uppercase;
  font-size: 1.7rem;
}
.card-title{
  padding: 0 !important;
  margin: 0 !important;
}
.card-title p{
  margin: 5px !important;
  text-align: center;
  width: 100%;
  font-size: 20px;
  text-transform: uppercase;
}
.v-card__text{
  padding: 0 !important;
}

/* DAYS CAROUSEL */
#days-carousel /deep/ .v-carousel__prev, #days-carousel /deep/ .v-carousel__next {
  display: none!important;
}
#days-carousel {
  box-shadow: none!important;
}
.day-tab {
  margin: 10px auto;
}
.days-slider {
  width: 30%;
  margin: .5rem auto;
}
.days-slider /deep/ .v-carousel {
  box-shadow: none!important;
}
.days-slider /deep/ i {
  color: #0284a8 !important;
}
</style>
