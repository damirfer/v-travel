<template>
  <div id="countryCreateOrUpdate">
    <v-alert fixed top type="info" :value="infoMessageShow">
      {{ infoMessage }}
    </v-alert>
    <v-form ref="form">
      <v-text-field
        box
        v-model="Country.Name"
        label="Country name"
        required
      ></v-text-field>

      <v-textarea
        box
        v-model="Country.GeneralInfo"
        label="General info"
      ></v-textarea>

      <img class="flagImage" :src="imagePreview" v-if="imagePreview != null" />

      <v-btn color="primary" class="fileBtn" round @click="onFileEdit"
        >Choose image</v-btn
      >

      <input
        type="file"
        ref="file"
        @change="takeImagePath($event)"
        style="display: none"
      />

      <div id="country-section-container" class="clearAfterFloats">
        <h2 class="font-weight-light primary-c" id="country-sections-title">
          Country sections
        </h2>
        <div style="text-align: left">
          <v-dialog v-model="sectionDialog" max-width="500">
            <v-btn dark round color="primary" slot="activator" class="ma-2">
              <v-icon>add</v-icon>
              Add new section
            </v-btn>
            <v-spacer></v-spacer>
            <div id="dialog-content">
              <v-alert fixed top type="info" :value="sectionInfoMessageShow">
                {{ infoMessage }}
              </v-alert>
              <v-form>
                <v-text-field
                  v-model="newSection.Title"
                  label="Section title"
                  required
                  box
                ></v-text-field>

                <v-textarea
                  v-model="newSection.Content"
                  label="Section content"
                  box
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
        <app-country-section
          v-for="section in Country.Sections"
          :key="section.CountrySectionId"
          :section="section"
          @deleteSection="deleteSection"
          class="country-section"
        ></app-country-section>
      </div>

      <v-btn round color="primary" v-if="isUpdate" @click="updateCountry">
        Update country
      </v-btn>
      <v-btn round color="primary" v-else @click="postCountry">
        Create new country
      </v-btn>
    </v-form>
  </div>
</template>
<script>
import { mapMutations } from "vuex";
import countrySection from "./Section";
export default {
  props: ["id"],
  components: {
    appCountrySection: countrySection,
  },
  data() {
    return {
      isUpdate: true,
      formatedDate: null,
      Country: {
        Sections: [],
        NationalDay: null,
      },
      newSection: {
        Title: "",
        Content: "",
      },
      sectionDialog: false,
      datePickerDialog: false,
      imagePreview: null,

      //Main alert
      infoMessage: "",
      infoMessageShow: false,

      //sectionAlert
      sectionInfoMessageShow: false,
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getCountry() {
      this.loader(true);
      setTimeout(() => {
        console.log(this.$route.params);
        this.$store.state.services.countryService
          .get(this.$route.params.id)
          .then((response) => {
            this.setInfoMessage("", false);
            this.Country = response.data;
            this.imagePreview = this.Country.FlagUrl;
            this.formatedDate = this.formatDate(
              new Date(this.Country.NationalDay)
            );
          });
      }, 2000);
      this.$store.state.services.countryService
        .get(this.$route.params.id)
        .then((response) => {
          this.setInfoMessage("", false);
          this.Country = response.data;
          this.imagePreview = this.Country.FlagUrl;
          this.formatDate(this.Country.NationalDay);
        })
        .finally(() => {
          this.loader(false);
        });
    },
    formatDate(nationalDay) {
      //Date format for date picker
      //nationalDay = 2018-08-12T01:46:29.0462819 -> formatedDate = 2018-07-12
      var date = new Date(nationalDay);
      var day = date.getDate(),
        month = date.getMonth(),
        year = date.getFullYear();
      if (day < 10) day = "0" + day;
      if (month < 10) month = "0" + month;
      return year + "-" + month + "-" + day;
    },
    deleteSection(sectionTitle) {
      this.Country.Sections = this.Country.Sections.filter((section) => {
        return section.Title != sectionTitle;
      });
    },
    createNewSection() {
      //Checks if new section title already exist
      var sameSectionName = false;
      this.Country.Sections.forEach((section) => {
        if (section.Title == this.newSection.Title) {
          this.sectionInfoMessageShow = true;
          this.infoMessage = "Section with this title already exists";
          sameSectionName = true;
          return;
        }
      });
      //If true break function execution
      if (sameSectionName) return;

      this.Country.Sections.push({
        Title: this.newSection.Title,
        Content: this.newSection.Content,
      });
      this.newSection.Title = "";
      this.newSection.Content = "";
      this.sectionInfoMessageShow = false;
      this.sectionDialog = false;
    },
    postCountry() {
      this.loader(true);
      this.Country.FlagUrl = "";
      this.Country.NationalDay = this.formatedDate;
      this.$store.state.services.countryService
        .add(this.Country, this.$refs.file.files[0])
        .then((response) => {
          //Redirect to country list
          this.$router.push({ name: "CountryList" });
        })
        .finally(() => {
          this.loader(false);
        });
    },
    updateCountry() {
      this.loader(true);
      this.$store.state.services.countryService
        .update(this.Country, this.$refs.file.files[0])
        .then((response) => {
          this.$router.push({ name: "CountryList" });
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
      let fileToUpload = this.$refs.file.files[0];
      let formData = new FormData();
      formData.append("fileToUpload", fileToUpload);
      this.Country.FlagUrl = formData;
    },
    onFileEdit() {
      this.$refs.file.click();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    },
  },
  created() {
    if (this.$route.name == "CountryEdit") {
      this.getCountry();
      this.isUpdate = true;
    } else {
      this.isUpdate = false;
    }
  },
  beforeRouteLeave(to, from, next) {
    if (this.$route.name == "CountryEdit") {
      this.isUpdate = false;
      this.Country = {};
    }
    next();
  },
};
</script>
<style scoped>
.flagImage {
  width: 30%;
  display: block;
}
.fileBtn {
  display: block;
}
#countryCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}
.country-section {
  width: calc(33.3% - 20px);
  float: left;
  margin: 25px 10px;
  padding: 20px;
  cursor: pointer;
}
.country-section:hover {
  background-color: lightgray;
}
#country-sections-title {
  margin: 20px 0 30px 0;
  text-transform: uppercase;
}
#dialog-content {
  background-color: white;
  padding: 20px;
}
</style>
