<template>
  <div id="schedule-container" class="clear-floats">
    <div class="clear-floats">
      <span class="circle"></span>
      <span class="center-line"></span>
      <div v-if="i % 2 != 0">
        <div></div>
        <div class="text-right absolute-top schedule schedule-time">
          <p>{{ item.TimeStamp }}h <span>――</span></p>
        </div>
        <div class="text-left description">
          <p>{{ item.Content }}</p>
          <div
            v-if="item.PlaceId !== null"
            class="place-link"
            @click="placeClicked(item.PlaceId, false)"
          >
            SEE MORE
          </div>
          <div
            v-else-if="item.AccommodationId !== null"
            class="place-link"
            @click="placeClicked(item.AccommodationId, true)"
          >
            SEE MORE
          </div>
        </div>
      </div>
      <div v-else>
        <div class="text-right description">
          <p>{{ item.Content }}</p>
          <div
            v-if="item.PlaceId !== null"
            class="place-link"
            @click="placeClicked(item.PlaceId, false)"
          >
            SEE MORE
          </div>
          <div
            v-else-if="item.AccommodationId !== null"
            class="place-link"
            @click="placeClicked(item.AccommodationId, true)"
          >
            SEE MORE
          </div>
        </div>
        <div class="text-left time-stamp schedule-time left-50">
          <p><span>――</span> {{ item.TimeStamp }}h</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["i", "item"],
  data() {
    return {
      test: 0,
    };
  },
  methods: {
    placeClicked(placeId, isAccommodation) {
      if (isAccommodation) {
        this.$emit("accommodationClicked", placeId);
      } else {
        this.$emit("placeClicked", placeId);
      }
    },
  },
  mounted() {},
};
</script>

<style scoped>
#schedule-container div {
  position: relative;
}

#schedule-container div::after {
  content: "";
  display: block;
  clear: both;
}

#schedule-container div div div {
  width: 50%;
  float: left;
  margin: 0;
  padding-top: 20px;
  padding-bottom: 20px;
  position: relative;
}
.absolute-top {
  position: absolute !important;
  top: 50%;
  transform: translateY(-50%);
}

.schedule-time {
  font-weight: bold;
  color: #666;
  font-family: "Montserrat", sans-serif;
}

.schedule-time span {
  font-weight: 300;
  color: #0284a8;
  letter-spacing: -2px;
}
.time-stamp {
  position: absolute !important;
  top: 50%;
  transform: translateY(-50%);
}

.text-left {
  text-align: left;
  padding-left: 20px;
  padding-right: 8px;
}

.right-50 {
  right: 50%;
}

.text-right {
  text-align: right;
  padding-right: 20px;
  padding-left: 8px;
}

.left-50 {
  left: 50%;
}

.circle {
  width: 14px;
  height: 14px;
  border-radius: 50%;
  background-color: #0284a8;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  border: 2px solid white;
  z-index: 3;
}
.center-line {
  height: 100%;
  width: 0.15em;
  background-color: #0284a8;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  z-index: 2;
}
/*
#schedule-container::after{
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

.description p {
  font-size: 16px;
  margin-bottom: 0;
  font-weight: 600;
  color: #222;
}

.place-link {
  display: block;
  width: 100% !important;
  padding-top: 10px !important;
  text-transform: uppercase;
  color: #0284a8;
  text-decoration: none;
  font-size: 11px;
  font-family: "Monsterrat", sans-serif;
}
</style>
