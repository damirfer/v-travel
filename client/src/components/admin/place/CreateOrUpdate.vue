<template>
    <div id="placeCreateOrUpdate">
        <v-alert
        fixed
        top
        type="info"
        :value="infoMessageShow">
        {{ infoMessage }}
        </v-alert>
        <v-form ref="form" v-model="valid">
          <v-layout row wrap>
            <v-flex xs12>
              <v-text-field
              :rules="nameRules"
              v-model="Place.Name"
              label="Place name"
              required
              box
              ></v-text-field>
            </v-flex>
            <v-flex xs12 md6 class="pr-md-1">
              <v-select
                box
                label="Select country"
                :items="countries"
                @change="setIsCountrySelected"
                deletable-chips
                chips
                v-model="Place.CountryId"
                item-text="Name"
                item-value="CountryId"></v-select>

              <v-select
                box
                label="Select type"
                :items="placeTypes"
                deletable-chips
                chips
                v-model="Place.PlaceTypeId"
                item-text="PlaceTypeName"
                item-value="PlaceTypeId"
                ></v-select>
              
            </v-flex>
            <v-flex xs12 md6 class="pl-md-1">
              
              <v-select
                box
                :disabled="!Place.CountryId"
                label="Select city"
                id="citySelect"
                deletable-chips
                chips
                :items="citiesByCountry"
                @focus="getCitiesByCountry"
                v-model="Place.CityId"
                item-text="Name"
                item-value="CityId"></v-select>

              

                <v-text-field
                v-model="Place.WorkingHours"
                label="Working hours"
                required
                box
                ></v-text-field>
            </v-flex>
          </v-layout>

            <v-textarea
            box
            v-model="Place.Description"
            label="Description"
            ></v-textarea>
          
            <img 
            class="placeImage"
            :src="imagePreview" 
            v-if="imagePreview != null"/>

            <v-btn 
            round
            color="primary"
            class="fileBtn"
            @click="onFileEdit">Choose image</v-btn>

            <input 
            type="file" 
            ref="file" 
            @change="takeImagePath($event)" 
            style="display:none"/>

            <v-btn round color="primary" :disabled="!valid" v-if="isUpdate" @click="updatePlace"> Update place </v-btn>
            <v-btn round color="primary" :disabled="!valid" v-else @click="postPlace"> Create new place </v-btn>
        </v-form>
    </div>
</template>
<script>
import {mapMutations} from "vuex";
export default {
  props: ["id"],
  data() {
    return {
      valid:true,
      isUpdate: true,
      Place: {},
      placeTypes:[],
      imagePreview: null,
      countries:[],
      citiesByCountry: [],
      placeTypes:[],
      isCountrySelected: false,
      //Main alert
      infoMessage: "",
      infoMessageShow: false,
      nameRules: [
      v => !!v || 'Name is required',
    ],
    languageRules:[
      v => !!v || 'Language is required',
      v => (v && v.length > 0) || 'Language is requiered'
    ]
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    getPlace() {
      this.loader(true);
      this.$store.state.services.placeService
        .get(this.$route.params.id)
        .then(response => {
          this.setInfoMessage("", false);
          this.Place = response.data;
          this.imagePreview = this.Place.PhotoUrl;
          if (this.$route.name == "PlaceEdit") {
            this.getCitiesByCountry();
          }

        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getPlaceType(){
      this.loader(true);
      return this.$store.state.services.placeTypeService.getAll().then(response=>{
        this.placeTypes=response.data;
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    postPlace() {
      this.loader(true);
      this.$store.state.services.placeService
        .add(this.Place, this.$refs.file.files[0])
        .then(response => {
          //Redirect to country list
          this.$router.push({ name: "PlaceList" });
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    updatePlace() {
      this.loader(true);
      this.$store.state.services.placeService
        .update(this.Place, this.$refs.file.files[0])
        .then(response => {
          this.$router.push({ name: "PlaceList" });
        })
        .finally(()=>{
          this.loader(false);
        });
    },    
    getCountries() {
      this.loader(true);
      this.$store.state.services.countryService
        .getAll()
        .then(response => {
          this.countries = response.data;
        })
        .finally(()=>{
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
      this.Place.PhotoUrl = formData;
    },
    onFileEdit() {
      this.$refs.file.click();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    },
    setIsCountrySelected() {
      if (this.countries != undefined) {
        if (this.countries.length > 0) {
          this.isCountrySelected = true;
          this.getCitiesByCountry();
        } else {
          this.isCountrySelected = false;
        }
      }
    },
    getCitiesByCountry() {
      this.loader(true);
      this.isCountrySelected = true;
      var urlParams = [];
      urlParams.push(this.Place.CountryId);

      this.$store.state.services.cityService
        .getByCountry(urlParams)
        .then(response => {
          this.citiesByCountry = response.data;})
          .finally(()=>{
            this.loader(false);
          });


          
    },
  },
  created() {
    if (this.$route.name == "PlaceEdit") {
      this.getPlace();
      this.isUpdate = true;
    } else {
      this.isUpdate = false;
    }
    this.getCountries();
    this.getPlaceType();
  },
  beforeRouteLeave(to, from, next) {
    if (this.$route.name == "PlaceEdit") {
      this.isUpdate = false;
      this.Place = {};
      this.imagePreview = "";
    }
    next();
  }
};
</script>
<style scoped>
.placeImage {
  width: 30%;
  display: block;
}
.fileBtn {
  display: block;
}
#placeCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}
</style>
