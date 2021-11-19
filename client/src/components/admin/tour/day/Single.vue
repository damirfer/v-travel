<template>
  <div>
    <div class="header clearAfterFloats">
      <div class="image-container" :style="{backgroundImage: 'url(' + day.PhotoUrl + ')'}">
        <div class="test-overlay"></div>
        <h2 class="dayName">
          {{ day.Name }}
          <v-icon @click="editDay" color="white" class="edit-btn">edit</v-icon>
        </h2>
      </div>

      <div class="description">
        <p>
          {{ day.Description != null ? day.Description.substring(0,400) : "" }}
          <!-- {{ day.Description.length > 400 ? "..." : "" }} -->
        </p>
      </div>
      <div class="body clearAfterFloats">
        <span class="accommodation" v-if="day.Accommodation != null">
          <h3 class="text-center">Accommodation</h3>
          <p>{{ day.Accommodation.Name }}</p>
        </span>
        <!-- <div class="countries">
          <h3>Countries</h3>
          <div
            class="country-item clearAfterFloats"
            v-for="country in day.Countries"
            :key="country.CountryId"
          >
            <span>
              <img :src="country.FlagUrl" alt>
            </span>
            <span>{{ country.Name }}</span>
          </div>
        </div>
        <div class="cities">
          <h3>Cities</h3>
          <div class="country-item clearAfterFloats" v-for="city in day.Cities" :key="city.CityId">
            <span class="text-left">{{ city.Name }}</span>
          </div>
        </div> -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["day"],
  methods: {
    editDay() {
      this.$emit("editDay");
    }
  },
  created() {
    console.log(this.day);
  }
};
</script>

<style scoped>
.v-tabs{
  position: relative;
}
.image-container {
  position: relative;
  height: 250px;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

.description {
  text-align: center;
  padding: 25px 15px;
  min-height: 170px;
}

.test-overlay{
  position: absolute;
  height: 100%;
  width: 100%;
  background: black;
  opacity: .6;
}

h2 {
  position: relative;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  text-shadow: 0 0 25px rgba(0,0,0,1);
  /* color: #0284a8; */
  color: white;
  font-family: "Montserrat", sans-serif;
  padding: 10px 0;
  font-weight: 600;
  font-size: 28px;
  text-transform: uppercase;
}
.dayName{
  font-size: 20px;
}
.edit-btn {
  float: right;
  margin-right: 10px;
}
h3 {
  text-transform: uppercase;
  font-weight: 500;
  text-align: left;
}

.body{
  position: relative;
  bottom: 0!important;
  padding: 25px;
}

.body span {
  display: block;
}

.body span h3{
  color: #0284a8;
}

.body span p {
  margin-bottom: 0;
}

.body div {
  margin: 10px;
  float: left;
}

.countries {
  width: calc(60% - 20px);
}

.cities {
  width: calc(40% - 20px);
}

ul li {
  text-align: left;
}

.country-item span {
  display: block;
  float: left;
}

.country-item span:nth-child(1) {
  width: 15%;
  text-align: center;
}

.country-item span:nth-child(1) img {
  width: 100%;
}

.country-item span:nth-child(2) {
  width: 85%;
  text-align: left;
  padding-left: 10px;
}

.cities div {
  float: none;
}

.cities span {
  display: block;
}
</style>
