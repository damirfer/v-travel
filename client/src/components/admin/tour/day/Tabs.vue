<template>
<div class="day-single clearAfterFloats" id="days-single">
    <v-tabs color="primary" dark slider-color="white" class="fullHeight" style="position:relative;">
        <v-tab id="tab" ripple class="tab-header">
            Day overview
        </v-tab>
        <v-tab-item>
            <app-day-single 
            :day="day"
            @editDay="editDay"></app-day-single>
        </v-tab-item>

        <v-tab ripple class="tab-header" @click="setIsScheduleSelected">
            Day schedule
        </v-tab>
        <v-tab-item class="tab-content">
            <app-schedule 
            :bottom="bottom"
            v-if="isScheduleSelected" 
            :scheduleId="day.ScheduleId"></app-schedule>
        </v-tab-item>
    </v-tabs>
</div>
</template>

<script>
import DaySingle from "./Single";
import Schedule from "./schedule/List";
export default {
  props: ["day"],
  components: {
    appDaySingle: DaySingle,
    appSchedule: Schedule
  },
  data() {
    return {
      tabItemHeight: 0,
      isScheduleSelected: false,

      //FOR SCHEDULE BOTTOM ACTIONS
      bottom: 0
    };
  },
  methods: {
    editDay() {
      this.$emit("editDay", this.day.DayId);
    },
    getTabHeight() {
      this.bottom = document.getElementById("tab").clientHeight;
      // var tabContent = document.getElementsByClassName("tab-content");
      // var daySingleHeight = document.getElementById("days-single").clientHeight;
      // var i = 0;
      // console.log(daySingleHeight);
      // for (i; i < tabContent.length; i++) {
      //   tabContent[i].style.height = daySingleHeight - tabItemHeight + "px";
      // }
    },
    setIsScheduleSelected() {
      this.isScheduleSelected = true;
    }
  },
  mounted() {
    var test = "We fly to Dubrovnik Airport where we meet our local travel leader for further travel to Podgora, the place with pebble beach and crystal clear water. In the marina, towns fishing boats are side by side with visitors sailboats. Along the seafront there are plenty of restaurants, market stands and small shops. On the way to the hotel in Podgora we stop for a simpler dinner. Arrival at our hotel. Check in and overnight. ";
    console.log(test.length);
    this.getTabHeight();
  }
};
</script>

<style scoped>
.day-single {
  position: relative;
  width: calc(70% - 40px);
  min-height: 450px;
  margin: 20px;
  border-radius: 10px;
  background-color: white;
  box-shadow: 0 0 15px rgba(153, 153, 153, 0.35);
}
.day-single /deep/ .v-tabs__container {
  display: flex;
  justify-content: space-around;
}
.tab-header {
  width: 50%;
}
.tab-content{
  height: 100%;
}

</style>
