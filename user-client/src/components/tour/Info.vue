<template>
  <div id="tour-info-container">
    <div
      class="header"
      :style="{ backgroundImage: 'url(' + tourInfo.PhotoUrl + ')' }"
    >
      <div class="black-overlay"></div>
      <div class="header-title-date">
        <h1>{{ tourInfo.Name }}</h1>
        <p>{{ tourInfo.DateFrom }} - {{ tourInfo.DateTo }}</p>
      </div>
    </div>
    <div class="general-info-container">
      <div class="general-info">
        <p class="title">General info</p>
        <div class="title-line"></div>
        <p class="content">{{ tourInfo.TourDetails }}</p>
      </div>
      <div class="tour-leader">
        <p class="title">Tour leader</p>
        <div class="title-line"></div>
        <div class="tour-leader-info">
          <div class="tl-image" v-if="tourInfo.Guide">
            <img :src="tourInfo.Guide.PhotoUrl" alt="Guide photo" />
          </div>
          <div class="tl-name" v-if="tourInfo.Guide">
            <p>
              {{ tourInfo.Guide.FirstName + " " + tourInfo.Guide.LastName }}
            </p>
          </div>
          <!-- <div class="tl-contact">
            <a class="tl-contact-phone" href="tel:+38763386287">
              <img
                src="https://img.icons8.com/ios/50/0284a8/phone-filled.png"
              />+387 63 386 287
            </a>
            <a class="tl-contact-email" href="mailto:dizdar.admir@outlook.com">
              <img
                src="https://img.icons8.com/ios/50/0284a8/secured-letter-filled.png"
              />zlatko@lasta.ba
            </a>
          </div> -->
          <div class="tl-bio" v-if="tourInfo.Guide">
            <p>{{ tourInfo.Guide.Bio }}</p>
          </div>
        </div>
      </div>
      <div class="countries">
        <p class="title">Countries</p>
        <div class="title-line countries-title-line"></div>
        <app-country-single
          class="country"
          v-for="country in tourInfo.Countries"
          :key="country.CountryId"
          :country="country"
        ></app-country-single>
      </div>
      <div class="sections">
        <p class="title">Additional Info</p>
        <div class="title-line"></div>
        <app-tour-section
          class="section"
          v-for="section in tourInfo.TourSections"
          :key="section.Title"
          :section="section"
        ></app-tour-section>
      </div>
    </div>
  </div>
</template>
<script>
import CountrySingle from "../country/Single";
import TourSection from "../Section";
export default {
  components: {
    appCountrySingle: CountrySingle,
    appTourSection: TourSection,
  },
  data() {
    return {
      tourInfo: {},
    };
  },
  methods: {
    getTourInfo() {
      this.$store.commit("setLoadingActive", true);
      this.$store.state.services.tourService
        .getInfo(localStorage.getItem("bookingId"))
        .then((response) => {
          this.$store.commit("setLoadingActive", false);
          console.log(response.data);
          this.tourInfo = response.data;
        })
        .catch((error) => {
          this.$store.commit("setLoadingActive", false);
        });
    },
  },
  created() {
    this.getTourInfo();
  },
};
</script>
<style scoped>
.black-overlay {
  position: absolute;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 0;
}
.title {
  font-size: 22px;
  margin: 0;
  margin-top: 10%;
  padding: 15px;
  color: #0284a8;
  text-transform: uppercase;
  font-weight: 600;
  font-family: "Montserrat", sans-serif;
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
  line-height: 140%;
  font-weight: 300;
}
#tour-info-container {
  height: 100vh;
  overflow-y: scroll;
  padding-bottom: 10vh;
}
.header {
  height: 100vh;
  position: relative;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  color: white;
}
.header-title-date {
  position: absolute;
  width: 97%;
  text-align: center;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

/* Tour leader */

.tour-leader-info {
  display: grid;
  grid-template-areas: "image tl-name tl-name" "image tl-contact tl-contact" "tl-bio tl-bio tl-bio";
  align-items: center;
  padding: 0 15px;
  margin-top: 35px;
  margin-bottom: 35px;
  column-gap: 2em;
}

.tl-image {
  grid-area: image;
}

.tl-image img {
  max-width: 90px;
  border-radius: 50%;
  padding-top: 25px;
}

.tl-name {
  grid-area: tl-name;
  font-size: 1.15em;
  font-weight: 600;
  padding-top: 40px;
}

.tl-contact {
  grid-area: tl-contact;
  display: grid;
  grid-template-columns: 1fr;
  font-family: "Montserrat", sans-serif;
  row-gap: 0.8em;
}

.tl-contact img {
  width: 50%;
  /* margin-right: 15px; */
}

.tl-contact a {
  text-decoration: none;
  font-size: 0.8em;
  color: #0284a8;
  font-weight: 600;
}

.tl-contact-phone {
  display: grid;
  grid-template-columns: 1fr 8fr;
  justify-content: center;
  align-items: center;
}

.tl-contact-email {
  display: grid;
  grid-template-columns: 1fr 8fr;
  justify-content: center;
  align-items: center;
}

.tl-bio {
  grid-area: tl-bio;
  line-height: 140%;
  font-weight: 300;
}

.countries-title-line {
  margin-bottom: 35px;
}

/* .tl-header {
  padding: 20px 10x;
  position: relative;
}
.tl-header::after {
  content: "";
  display: block;
  clear: both;
}
.tl-header div {
  float: left;
  text-align: center;
  margin: 10px 0;
}
.tl-header div:nth-child(1) {
  width: 35%;
}
.tl-header div:nth-child(1) img {
  height: 85px;
  border-radius: 50%;
}
.tl-header div:nth-child(2) {
  width: 65%;
  position: relative;
  text-align: left;
  height: 85px;
}
.tl-header div:nth-child(2) p {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  font-weight: bold;
  font-size: 1.3em;
  margin: 0;
} */

@media only screen and (min-width: 700) {
  .general-info-container {
    width: 1000px;
    margin: 7vh auto;
  }
}
</style>
