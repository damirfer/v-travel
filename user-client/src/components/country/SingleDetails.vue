<template>
  <div class="country-details">
    <div class="countries-header">
      <div
        @click="$router.push({ name: 'TourInfo' })"
        class="back-arrow-container"
      >
        <img
          @click="$router.push({ name: 'TourInfo' })"
          class="back-arrow"
          src="https://img.icons8.com/ios/50/0284a8/left-filled.png"
        />
      </div>
      <img :src="country.FlagUrl" alt="Country flag" class="flag" />
      <div class="flag-overlay"></div>
      <h1>{{ country.Name }}</h1>
    </div>

    <div class="title">General info</div>
    <div class="title-line"></div>
    <div class="content general-info">
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/skyscrapers.png" />
        <span class="gen-info-title">Capital city:</span>
        <span>{{ country.CapitalCity }}</span>
      </div>
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/ruler.png" />
        <span class="gen-info-title">Area:</span>
        <span>{{ country.Area }}km²</span>
      </div>
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/money-bag.png" />
        <span class="gen-info-title">Currency:</span>
        <span>{{ country.Currency }}</span>
      </div>
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/calendar-11.png" />
        <span class="gen-info-title">National day:</span>
        <span>{{ country.NationalDay }}</span>
      </div>
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/language.png" />
        <span class="gen-info-title">Language:</span>
        <span>{{ country.OfficialLanguage }}</span>
      </div>
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/us-capitol.png" />
        <span class="gen-info-title">Polity:</span>
        <span>{{ country.Polity }}</span>
      </div>
      <div class="general-info-item">
        <img src="https://img.icons8.com/ios/50/0284a8/demographics.png" />
        <span class="gen-info-title">Population:</span>
        <span>{{ country.Population }}</span>
      </div>
    </div>

    <div class="content">{{ country.GeneralInfo }}</div>
    <div class="country-sections">
      <div class="title">Additional info</div>
      <div class="title-line"></div>
      <app-country-section
        class="section"
        v-for="section in country.Sections"
        :key="section.Title"
        :section="section"
      ></app-country-section>
    </div>
    <div class="cities">
      <div class="title">Cities</div>
      <div class="title-line"></div>
      <app-city-single
        class="section"
        v-for="city in country.Cities"
        :key="city.CityId"
        :city="city"
      ></app-city-single>
    </div>
  </div>
</template>
<script>
import countrySection from "../Section";
import citySingle from "../city/Single";
export default {
  components: {
    appCountrySection: countrySection,
    appCitySingle: citySingle,
  },

  data() {
    return {
      country: {},
    };
  },
  methods: {
    getCountryInfo() {
      this.$store.state.services.countryService
        .getInfo(this.$route.params.id)
        .then((respose) => {
          this.country = respose.data;
        });
    },
  },
  created() {
    this.getCountryInfo();
  },
};
</script>
<style scoped>
.back-arrow-container {
  height: 35px;
  width: 35px;
  position: absolute;
  top: 20px;
  left: 20px;
  z-index: 50;
  background-color: white;
  border-radius: 50%;
}

.back-arrow {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 70%;
  transform: translate(-50%, -50%);
  z-index: 55;
}

.title {
  font-size: 22px;
  margin: 0;
  padding: 15px;
  color: #0284a8;
  text-transform: uppercase;
  font-weight: 600;
  margin-top: 35px;
  font-family: "Montserrat", sans-serif;
}
.title-line {
  width: 60px;
  height: 3px;
  border-radius: 10px;
  background-color: #0284a8;
  margin-top: -11px;
  margin-left: 15px;
  margin-bottom: 35px;
}
.content {
  padding: 10px 15px;
  line-height: 140%;
  font-weight: 300;
}
.back-btn {
  position: absolute;
  top: 10px;
  left: 15px;
  width: 20px;
}
.country-details {
  position: relative;
  height: 100vh;
  overflow-y: scroll;
  padding-bottom: 10vh;
}

.countries-header {
  position: relative;
  height: 100vh;
  object-fit: cover;
  text-align: center;
  margin-bottom: 35px;
  overflow: hidden;
}

.flag-overlay {
  position: absolute;
  height: 100%;
  width: 100%;
  background: rgba(0, 0, 0, 0.4);
}
.flag {
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  height: 100%;
  display: block;
}

h1 {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 97%;
  transform: translate(-50%, -50%);
  text-transform: uppercase;
  color: white;
  text-shadow: 0 0 15px black;
}

.general-info-item {
  display: grid;
  grid-template-columns: 1fr 2fr 4fr;
  justify-content: center;
  align-items: center;
  margin: 25px 0;
}

.general-info-item img {
  width: 50%;
}

.gen-info-title {
  color: #0284a8;
  font-size: 13px;
  font-family: "Montserrat", sans-serif;
}

.general-info {
  padding-top: 0;
}

.general-info p span:nth-child(1) {
  font-weight: bold;
}
/* SECTIONS */
.section:nth-child(even) {
  background-color: rgba(139, 5, 130, 0.01);
}
</style>
