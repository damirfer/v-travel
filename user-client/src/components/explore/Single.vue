<template>
  <div>
    <p class="cityName">{{ placesByCity.CityName }}</p>
    <div
      v-for="place in placesByCity.PlaceList"
      :key="place.Name"
      class="explore-item"
    >
      <div
        class="header-image"
        :style="{ backgroundImage: 'url(' + place.PhotoUrl + ')' }"
      ></div>
      <div class="content">
        <p class="place-name">{{ place.Name }}</p>
        <p class="description">{{ place.Description }}</p>
        <div class="working-hours">
          <img
            src="https://img.icons8.com/material-outlined/24/000000/clock.png"
          />
          <p>{{ place.WorkingHours }}</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["placesByCity"],
  data() {
    return {};
  },
  created() {},
};
</script>
<style scoped>
.cityName {
  display: inline-block;
  margin: 15px auto;
  position: relative;
  left: 50%;
  transform: translateX(-50%);
  text-transform: uppercase;
  font-size: 17px;
}
.cityName::after {
  content: "";
  display: block;
  margin-left: -10%;
  width: 120%;
  height: 1px;
  background-color: #0284a8;
}
.explore-item {
  box-shadow: 0 0 15px rgba(153, 153, 153, 0.35);
  margin-bottom: 20px;
  border-radius: 10px 10px 10px 10px;
}
.header-image {
  height: 40vh;
  background-size: cover;
  background-position: center;
  border-radius: 10px 10px 0 0;
}
.content {
  padding: 25px 10px;
  padding-top: 10px;
  border-radius: 10px 10px 0 0;
}
.place-name {
  font-size: 18px;
  font-weight: 700;
  margin-bottom: 1em;
  text-align: center;
}
.content::after {
  content: "";
  display: block;
  clear: both;
}
.content span {
  width: 50%;
  display: block;
  float: left;
  color: #444;
  font-weight: 600;
  font-family: "Montserrat", sans-serif;
}
.working-hours {
  text-align: center;
}
.working-hours img {
  width: 25px;
}
.working-hours p {
  margin: 0;
  margin-top: 5px;
}
</style>
