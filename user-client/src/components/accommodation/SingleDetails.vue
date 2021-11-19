<template>
  <div class="place">
    <div @click="closePlaceModal" class="back-arrow-container">
      <img
        @click="closePlaceModal"
        class="back-arrow"
        src="https://img.icons8.com/ios/50/0284a8/left-filled.png"
      />
    </div>
    <div
      class="header"
      :style="{ backgroundImage: 'url(' + accommodation.PhotoUrl + ')' }"
    >
      <div class="black-overlay"></div>
      <div class="header-title">
        <h1>{{ accommodation.Name }}</h1>
        <p>{{ accommodation.AccommodationType }}</p>
        <!-- <p>Working hours: <span>{{ place.WorkingHours }}</span></p> -->
      </div>
    </div>
    <div>
      <div class="clear-floats relative">
        <div class="divider"></div>
        <div class="left-half text-center">
          <p class="heading">Hotel amenities:</p>
          <p
            class="amenity"
            v-for="amenity in accommodation.HotelAmenities"
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
            v-for="amenity in accommodation.RoomAmenities"
            :key="amenity.AmenityId"
          >
            <span>{{ amenity.Name }}</span>
            <span class="amenity-line l0"></span>
            <span class="circle circle-left"></span>
          </p>
        </div>
      </div>
      <div class="location-icon">
        <img src="https://img.icons8.com/material/35/000000/marker.png" />
      </div>
      <p class="text-center">{{ accommodation.Address }}</p>
      <p class="title">Description</p>
      <div class="title-line"></div>
      <div class="content">{{ accommodation.Description }}</div>

      <a :href="'tel:' + accommodation.Telephone" class="link-action">
        <img src="https://img.icons8.com/ios/50/000000/phone-filled.png" />
        <p>click to call</p>
        <p>{{ accommodation.Telephone }}</p>
      </a>

      <a :href="accommodation.WebsiteUrl" class="link-action">
        <img src="https://img.icons8.com/ios/50/000000/globe.png" />
        <p>click to visit</p>
        <p>{{ accommodation.WebsiteUrl }}</p>
      </a>
    </div>
  </div>
</template>

<script>
export default {
  props: ["accommodationId"],
  data() {
    return {
      accommodation: {},
    };
  },
  methods: {
    getAccommodation() {
      this.$store.state.services.accommodationService
        .getForMobile(this.accommodationId)
        .then((response) => {
          this.accommodation = response.data;
        });
    },
    closePlaceModal() {
      this.$emit("closePlaceModal");
    },
  },
  created() {
    this.getAccommodation();
  },
};
</script>

<style scoped>
.back-arrow-container {
  height: 35px;
  width: 35px;
  position: absolute;
  top: 0px;
  left: 0px;
  z-index: 1000;
  background-color: white;
  border-radius: 50%;
  margin: 20px 35px 35px 20px;
}

.back-arrow {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 70%;
  transform: translate(-50%, -50%);
  z-index: 1000;
}
.place {
  position: relative;
  padding-bottom: 35px;
}
.black-overlay {
  position: absolute;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 0;
}
.title {
  font-family: "Montserrat", sans-serif;
  font-size: 20px;
  margin: 0;
  color: white;
  padding: 15px;
  color: #0284a8;
  text-transform: uppercase;
  font-weight: 600;
  margin-top: 45px;
}

.title-line {
  width: 60px;
  height: 3px;
  border-radius: 10px;
  background-color: #0284a8;
  margin-top: -11px;
  margin-left: 15px;
  margin-bottom: 15px;
}

.content {
  padding: 10px 15px;
  font-weight: 300;
  line-height: 140%;
}

.header {
  height: 100vh;
  position: relative;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  color: white;
}

.header-title {
  position: absolute;
  width: 100%;
  text-align: center;
  top: 50%;
  transform: translateY(-50%);
}
.header-title p {
  font-size: 18px;
}
.heading {
  text-transform: uppercase;
  font-weight: bold;
  font-size: 12px;
  text-align: center;
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
.location-icon {
  padding-top: 20px;
  text-align: center;
}
.location-icon img {
  width: 35px;
}
.link-action {
  display: block;
  margin-top: 40px;
  text-align: center;
  text-decoration: none;
  color: black;
}
.link-action img {
  width: 35px;
}
.link-action p:nth-child(2) {
  margin: 0;
  font-size: 14px;
}
.link-action p:nth-child(3) {
  margin: 5px 0 0 0;
  font-size: 17px;
  font-weight: bold;
}
</style>
