<template>
  <div id="cityCreateOrUpdate">
    <v-alert fixed top type="info" :value="infoMessageShow">
      {{ infoMessage }}
    </v-alert>

    <v-form ref="form">
      <v-layout row wrap>
        <v-flex xs12 md6 pr-1>
          <v-text-field
            box
            v-model="City.Name"
            @blur="takeCityLatLong"
            label="City name"
            required
          ></v-text-field>
        </v-flex>
        <v-flex xs12 md6 pl-1>
          <v-select
            box
            :items="Countries"
            v-model="City.CountryId"
            label="Located in"
            item-text="Name"
            item-value="CountryId"
          ></v-select>
        </v-flex>
      </v-layout>

      <v-textarea
        box
        v-model="City.GeneralInfo"
        label="General info"
      ></v-textarea>

      <img class="cityImage" :src="imagePreview" v-if="imagePreview != null" />

      <v-btn color="primary" class="fileBtn" round @click="onFileEdit"
        >Choose image</v-btn
      >

      <input
        type="file"
        ref="file"
        @change="takeImagePath($event)"
        style="display: none"
      />

      <div id="city-section-container" class="clearAfterFloats">
        <h2 class="font-weight-light primary-c" id="city-sections-title">
          City sections
        </h2>
        <div style="text-align: left">
          <v-dialog v-model="dialog" max-width="500">
            <v-btn dark round color="primary" slot="activator" class="ma-2">
              <v-icon>add</v-icon>
              Add new section
            </v-btn>
            <v-spacer></v-spacer>
            <div id="dialog-content">
              <v-form>
                <v-text-field
                  v-model="newSection.Title"
                  label="Section title"
                  required
                  box
                ></v-text-field>

                <v-textarea
                  box
                  v-model="newSection.Content"
                  label="Section content"
                ></v-textarea>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="primary" round @click="createNewSection">
                    Create
                  </v-btn>
                </v-card-actions>
              </v-form>
            </div>
          </v-dialog>
        </div>
        <app-city-section
          v-for="section in City.Sections"
          :key="section.CitySectionId"
          :section="section"
          @deleteSection="deleteSection"
          class="city-section"
        ></app-city-section>
      </div>

      <v-btn round color="primary" v-if="isUpdate" @click="updateCity">
        Update city
      </v-btn>
      <v-btn round color="primary" v-else @click="postCity">
        Create new city
      </v-btn>
    </v-form>
  </div>
</template>
<script>
import { mapMutations } from "vuex";
import citySection from "./Section";
export default {
  props: ["id"],
  components: {
    appCitySection: citySection,
  },
  data() {
    return {
      Countries: [],
      Country: {},
      City: {
        Sections: [],
        Latitude: "",
        Longitude: "",
      },
      dialog: false,
      newSection: {
        Title: "",
        Content: "",
      },
      isUpdate: true,
      imagePreview: null,
      infoMessage: "",
      infoMessageShow: false,
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getCountries() {
      this.loader(true);
      this.$store.state.services.countryService
        .getAll()
        .then((response) => {
          this.Countries = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getCity() {
      this.loader(true);
      this.$store.state.services.cityService
        .get(this.$route.params.cityId)
        .then((response) => {
          this.City = response.data;
          this.imagePreview = this.City.PhotoUrl;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    deleteSection(id) {
      this.City.Sections = this.City.Sections.filter((section) => {
        //TODO -> Add validation for section title
        return section.Title != id;
      });
    },
    createNewSection() {
      this.City.Sections.push({
        Title: this.newSection.Title,
        Content: this.newSection.Content,
      });
      this.newSection.Title = "";
      this.newSection.Content = "";
      this.dialog = false;
    },
    postCity() {
      this.loader(true);
      this.$store.state.services.cityService
        .add(this.City, this.$refs.file.files[0])
        .then((response) => {
          window.location.href = "/AdminHome/Cities/";
        })
        .finally(() => {
          this.loader(false);
        });
    },
    updateCity() {
      this.loader(true);
      this.$store.state.services.cityService
        .update(this.City, this.$refs.file.files[0])
        .then((response) => {
          window.location.href = "/AdminHome/Cities/";
        })
        .finally(() => {
          this.loader(false);
        });
    },
    takeImagePath(event) {
      const file = event.target.files[0];
      let filename = file.name;
      if (filename.lastIndexOf(".") <= 0) {
        return alert("Please select valid file");
      }

      const fileReader = new FileReader();

      fileReader.addEventListener("load", () => {
        this.imagePreview = fileReader.result;
      });

      fileReader.readAsDataURL(file);
    },
    onFileEdit() {
      this.$refs.file.click();
    },
    takeCityLatLong() {
      var geocoder = new google.maps.Geocoder();
      let self = this;
      geocoder.geocode({ address: this.City.Name }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
          self.City.Latitude = results[0].geometry.location.lat();
          self.City.Longitude = results[0].geometry.location.lng();
        } else {
          alert(
            "We could't find latitude and longitude for given city name. Please enter them manually." +
              status
          );
        }
      });
    },
  },
  created() {
    if (this.$route.name == "CityEdit") {
      this.getCity();
      this.isUpdate = true;
    } else {
      this.isUpdate = false;
    }
    this.getCountries();
  },
  beforeRouteLeave(to, from, next) {
    if (to.params.isUpdate == "false") {
      this.isUpdate = false;
      this.City = {};
    }
    next();
  },
};
</script>
<style scoped>
.cityImage {
  width: 30%;
  display: block;
}
.fileBtn {
  display: block;
}
#cityCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}
.city-section {
  width: calc(33.3% - 20px);
  float: left;
  margin: 25px 10px;
  padding: 20px;
  cursor: pointer;
}
.city-section:hover {
  background-color: lightgray;
}
#city-sections-title {
  margin: 20px 0 30px 0;
  text-transform: uppercase;
}

#dialog-content {
  background-color: white;
  padding: 20px;
}
</style>
