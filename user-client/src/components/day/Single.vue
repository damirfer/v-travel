<template>
  <div class="day-single">
    <div
      class="header-image"
      :style="{ backgroundImage: 'url(' + day.PhotoUrl + ')' }"
    ></div>
    <div class="buttons" @click="dayContentToggle">
      <p
        @click="viewMoreActive = false"
        class="btn noselect"
        :class="{ activeBtn: !viewMoreActive }"
      >
        Overview
      </p>

      <p
        class="btn noselect"
        v-on:click="viewMoreActive = true"
        :class="{ activeBtn: viewMoreActive }"
      >
        More
      </p>
    </div>
    <div class="header">
      <p>{{ day.Name }}</p>
    </div>
    <div v-if="!viewMoreActive" class="content">{{ day.Description }}</div>
    <div class="more-content" v-else>
      <p class="heading">Accommodation:</p>
      <p class="text-center" style="font-size: 18px">
        {{ day.Accommodation.Name }}
      </p>

      <div class="clear-floats relative">
        <div class="divider"></div>
        <div class="left-half text-center">
          <p class="heading">Hotel amenities:</p>
          <p
            class="amenity"
            v-for="amenity in day.HotelAmenities"
            :key="amenity.AmenityId"
          >
            {{ amenity.Name }}
            <span class="amenity-line r0"></span>
            <span class="circle circle-right"></span>
          </p>
        </div>

        <div class="left-half text-center">
          <p class="heading">Room amenities:</p>
          <p
            class="amenity"
            v-for="amenity in day.RoomAmenities"
            :key="amenity.AmenityId"
          >
            <span>{{ amenity.Name }}</span>
            <span class="amenity-line l0"></span>
            <span class="circle circle-left"></span>
          </p>
        </div>
      </div>

      <p class="heading">Included meals</p>
      <p class="text-center meals">
        <span v-if="day.IncludedMeals.IsBreakfastIncluded">B</span>
        <span v-if="day.IncludedMeals.IsLunchIncluded">L</span>
        <span v-if="day.IncludedMeals.IsDinnerIncluded">D</span>
      </p>
    </div>
  </div>
</template>
<script>
export default {
  props: ["day"],
  data() {
    return {
      viewMoreActive: false,
    };
  },
  methods: {
    dayContentToggle(event) {
      console.log(event);
      // this.viewMoreActive = !this.viewMoreActive
      // this.$parent.$refs.days.scrollTo(0,event.target.offsetTop - event.target.clientHeight - 15);
      this.$parent.$refs.days.scroll({
        top: event.target.offsetTop - event.target.clientHeight - 10,
        left: 0,
        behavior: "smooth",
      });
    },
  },
  created() {},
};
</script>
<style scoped>
.day-single {
  background-color: #f7f7f7;
  margin: 20px 0;
  box-shadow: 0 0 15px rgba(153, 153, 153, 0.35);
  border-radius: 10px;
}
.header p {
  font-family: "Montserrat", sans-serif;
  color: #0284a8;
  text-transform: uppercase;
  font-size: 20px;
  margin: 0;
  padding: 1.5em 10px 0;
  text-align: center;
}
.header-image {
  height: 45vh;
  background-size: cover;
  background-position: center;
  border-radius: 10px 10px 0 0;
}
.buttons {
  border-bottom: 1px solid #f7f7f7;
}
.buttons p {
  width: 50%;
  float: left;
  padding: 10px;
  text-align: center;
  margin: 0;
  font-size: 16px;
}
.content {
  text-align: left;
  padding: 25px 10px 35px;
  font-weight: 300;
  line-height: 140%;
}
.buttons::after {
  content: "";
  display: block;
  clear: both;
}
.btn {
  background: #0284a8; /* Old browsers */
  font-size: 16px;
  font-family: "Montserrat", sans-serif;
  text-transform: uppercase;
  color: white;
}
.more-content {
  padding: 10px;
}
.more-content .heading {
  text-transform: uppercase;
  font-weight: bold;
  font-size: 12px;
  text-align: center;
}
.activeBtn {
  background: #f7f7f7;
  font-weight: bold;
  color: #0284a8;
}
.divider {
  width: 1px;
  background-color: gray;
  height: 100%;
  position: absolute;
  top: 0;
  left: 50%;
  transform: translateX(-50%);
}
.meals span {
  padding-top: 2px;
  display: inline-block;
  width: 25px;
  height: 25px;
  background-color: #0284a8;
  color: white;
  border-radius: 50%;
  text-align: center;
  margin: 5px;
  font-weight: bold;
}

.test {
  position: relative;
}
.amenity {
  position: relative;
}
.amenity-line {
  width: 30px;
  height: 1px;
  background-color: gray;
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
}
.r0 {
  right: 0;
}
.l0 {
  left: 0;
}
.circle {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  width: 12px;
  height: 12px;
  border: 2px solid #f7f7f7;
  border-radius: 50%;
  background-color: #0284a8;
  z-index: 10;
}
.circle-left {
  left: -6px;
}
.circle-right {
  right: -6px;
}

/* DISABLE TEXT SELECT */
.noselect {
  -webkit-touch-callout: none; /* iOS Safari */
  -webkit-user-select: none; /* Safari */
  -khtml-user-select: none; /* Konqueror HTML */
  -moz-user-select: none; /* Firefox */
  -ms-user-select: none; /* Internet Explorer/Edge */
  user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
}
</style>
