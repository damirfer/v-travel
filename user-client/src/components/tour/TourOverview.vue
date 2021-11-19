<template>
  <div
    v-touch:swipe="swipeHandler"
    id="tour-overview"
    class="full-height"
    :style="{ width: tourOverviewWidthPx, left: leftInPx }"
  >
    <div :style="{ left: navLeftInPx }" class="header-click-navigation">
      <div @click="swipeHandler('right')"></div>
      <div></div>
      <div @click="swipeHandler('left')"></div>
    </div>
    <div id="header-navigation" :style="{ left: navLeftInPx }">
      <div class="header-item"></div>
      <div class="header-item item-active">Map</div>
      <div class="header-item item-active active-page">Tour info</div>
      <div
        class="header-item"
        :class="{ 'item-active': i == 0 }"
        v-for="(schedule, i) in schedules"
        :key="i"
      >
        Day {{ i + 1 }}
      </div>
      <div class="header-item"></div>

      <div class="header-line"></div>
    </div>

    <app-tour-map
      :tourCities="tourCities"
      :isMapTriggered="isMapTriggered"
      class="left full-screen"
    ></app-tour-map>

    <app-day-list
      id="days"
      :days="this.tour"
      class="left full-screen"
    ></app-day-list>

    <app-schedule-list
      class="day-schedule full-screen left"
      v-for="schedule in schedules"
      :key="schedule.ScheduleId"
      :scheduleItems="schedule.ScheduleItems"
    ></app-schedule-list>

    <app-footer-navigation :navLeftInPx="navLeftInPx"></app-footer-navigation>
  </div>
</template>

<script>
import DayList from "../day/List";
import ScheduleList from "../schedule/List";
import Navigation from "../Navigation";
import TourMap from "./Map";
export default {
  components: {
    appDayList: DayList,
    appScheduleList: ScheduleList,
    appFooterNavigation: Navigation,
    appTourMap: TourMap,
  },
  data() {
    return {
      tour: [],
      schedules: [],
      tourOverviewWidth: 0,
      tourOverviewWidthPx: 0,
      left: 0,
      navLeft: 0,
      leftInPx: "",
      navLeftInPx: "",
      isMapTriggered: false,

      isLoading: true,
      loadingLeft: 540,

      //For google maps - map where are all cities displayed and connected
      tourCities: [],
    };
  },
  methods: {
    getTourByBooking() {
      this.$store.commit("setLoadingActive", true);
      this.$store.state.services.tourService
        .getTourByBooking(localStorage.getItem("tourId"))
        .then((response) => {
          this.$store.commit("setLoadingActive", false);
          this.tour = response.data;
          this.tourOverviewWidth =
            window.innerWidth * 2 + window.innerWidth * this.tour.length;
          this.tourOverviewWidthPx = this.tourOverviewWidth + "px";
          this.tour.forEach((day) => {
            this.schedules.push(day.Schedule);
            day.Cities.forEach((city) => {
              this.tourCities.push(city);
            });
          });
          //Removes duplicate cities from array of objects
          this.tourCities = this.tourCities.filter(
            (city, index, self) =>
              index ===
              self.findIndex(
                (c) => c.CityId === city.CityId && c.Name === city.Name
              )
          );
        })
        .catch((err) => {
          this.$store.commit("setLoadingActive", false);
        });
    },
    swipeHandler(direction) {
      if (direction == "left") {
        this.left -= window.innerWidth;
        //Checks if swipeing in no content area
        if (this.left < -this.tourOverviewWidth + window.innerWidth) {
          this.left += window.innerWidth;
          return;
        }
        this.navLeft += window.innerWidth;
        this.navigationMove("right");
      } else if (direction == "right") {
        this.left += window.innerWidth;
        if (this.left > 0) {
          this.left -= window.innerWidth;
          return;
        }
        this.navLeft -= window.innerWidth;
        this.navigationMove("left");
      }
      this.leftInPx = this.left + "px";
      this.navLeftInPx = this.navLeft + "px";
    },
    navigationMove(direction) {
      var navItem = document.getElementsByClassName("header-item");
      var i = 0;
      for (i; i < navItem.length; i++) {
        if (navItem[i].classList.contains("item-active")) {
          if (direction == "right") {
            //Header navigation active items
            navItem[i].classList.remove("item-active");
            navItem[i + 3].classList.add("item-active");

            //Header navigation focused item
            navItem[i + 1].classList.remove("active-page");
            navItem[i + 2].classList.add("active-page");

            return;
          } else {
            navItem[i - 1].classList.add("item-active");
            navItem[i + 2].classList.remove("item-active");

            navItem[i + 1].classList.remove("active-page");
            navItem[i].classList.add("active-page");
            return;
          }
        }
      }
    },
  },
  created() {
    //Sets tour overview as first view
    this.left = -window.innerWidth;
    this.leftInPx = this.left + "px";

    //Moving both navigations across the screens
    this.navLeft = window.innerWidth;
    this.navLeftInPx = this.navLeft + "px";

    this.getTourByBooking();
  },
  watch: {
    left() {
      if (this.left == 0) {
        this.isMapTriggered = true;
      }
    },
  },
};
</script>

<style scoped>
.left {
  float: left;
}
.full-height {
  height: 100vh;
}
.full-screen {
  height: 100vh;
  width: 100vw;
}
.active-page {
  font-size: 18px;
  font-weight: bold;
}
#tour-overview {
  overflow: hidden;
  position: relative;
  transition: left 0.4s;
}
#header-navigation {
  background-color: #f7f7f7; /*da ne otvara onaj kurac*/
  height: 7vh;
  width: 100vw;
  position: absolute;
  top: -1px;
  z-index: 20;
  transition: left 0.4s;
  box-shadow: 0px 2px 25px -10px rgba(0, 0, 0, 0.4);
}
.header-click-navigation {
  height: 7vh;
  width: 100%;
  position: absolute;
  top: 0;
  z-index: 21;
  display: flex;
}
.header-click-navigation div {
  width: 33.333vw;
  height: 100%;
}
.header-line {
  position: absolute;
  width: 55px;
  height: 2px;
  background-color: #0284a8;
  bottom: 0;
  left: 50%;
  transform: translateX(-50%);
}
.header-item {
  width: 33.3%;
  height: 100%;
  float: left;
  display: none;
  text-align: center;
  text-transform: uppercase;
  padding-top: 15px;
}
.item-active {
  display: block;
}
#days {
  overflow-y: scroll;
  padding: 10vh 10px;
}
.day-schedule {
  padding-top: 7vh;
}

#tour-overview::after {
  content: "";
  display: block;
  clear: both;
}

@media (min-width: 1024) {
  #days {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 2em;
    padding: 10vh 1em;
    box-sizing: border-box;
  }
  #tour-overview {
    padding-bottom: 10vh;
  }
  .full-height {
    padding-bottom: 10vh;
  }
}
</style>
