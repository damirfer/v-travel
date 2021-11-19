<template>
    <div id="accCreateOrUpdate">
        <v-alert
        fixed
        top
        type="info"
        :value="infoMessageShow">
        {{ infoMessage }}
        </v-alert>
        <v-form>
          <v-layout row wrap>
            <v-flex xs12 md6 class="pr-md-1">
            <v-text-field
            box
            v-model="accommodation.Name"
            label="Name"></v-text-field>

            <v-text-field
            box
            v-model="accommodation.Telephone"
            label="Telephone"></v-text-field>

            <v-select
            box
            :items="cities"
            v-model="accommodation.CityId"
            label="Select city"
            item-text="Name"
            item-value="CityId"></v-select>

          </v-flex>
          <v-flex xs12 md6 class="pl-md-1">
            <v-text-field
            box
            v-model="accommodation.Address"
            label="Address"></v-text-field>

            <v-text-field
            box
            v-model="accommodation.WebsiteUrl"
            label="Website URL"></v-text-field>

            <v-select
            box
            :items="accommodationType"
            v-model="accommodation.AccommodationTypeId"
            label="Select accomodation type"
            item-text="Name"
            item-value="AccommodationTypeId"></v-select>
          </v-flex>
          </v-layout>

            <v-select
            box
            :items="roomAmenities"
            v-model="accommodation.AccommodationAmenity"
            label="Select room amenities"
            multiple
            chips
            item-text="Name"
            item-value="AmenityId"
            persistent-hint
            ></v-select>

            <v-select
            box
            :items="hotelAmenities"
            v-model="accommodation.AccommodationAmenity"
            label="Select hotel amenities"
            multiple
            chips
            item-text="Name"
            item-value="AmenityId"
            persistent-hint
            ></v-select>

            <img 
            class="accImage"
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
            
            <v-textarea
            box
            v-model="accommodation.Description"
            label="Description"></v-textarea>

            <v-btn round color="primary" v-if="!isUpdate" @click.prevent="postAccommodation"> Create </v-btn>
            <v-btn round color="primary" v-else @click.prevent="updateAccommodation"> Update </v-btn>

        </v-form>
    </div>
</template>
<script>
import {mapMutations} from "vuex";
export default {
  data() {
    return {
      accommodation: {
        CityId: 0,
        AccommodationAmenity: []
      },
      cities: [],
      accommodationType: [],
      roomAmenities: [],
      hotelAmenities: [],
      isUpdate: true,
      imagePreview: null,
      infoMessage: "",
      infoMessageShow: false

    };
  },
  methods: {
    //GET methods
    ...mapMutations({
      loader:"COMMIT_LOADER"
    }),
    getCities() {
      this.loader(true);
      return this.$store.state.services.cityService.getAll().then(response => {
        this.cities = response.data;
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    getAccommodationType() {
      this.loader(true);
      return this.$store.state.services.accommodationTypeService
        .getAll()
        .then(response => {
          this.accommodationType = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getAmenities() {
      this.loader(true);
      this.$store.state.services.amenitiesService.getAll().then(response => {
        this.roomAmenities = response.data.filter(amenity => {
          return amenity.AmenityType.AmenityTypeId == 2;
        });
        this.hotelAmenities = response.data.filter(amenity => {
          return amenity.AmenityType.AmenityTypeId == 1;
        });
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    getAccommodation() {
      this.loader(true);
      this.$store.state.services.accommodationService
        .get(this.$route.params.id)
        .then(response => {
          this.accommodation = response.data;
          this.imagePreview=this.accommodation.PhotoUrl;
        })
        .finally(()=>{
          this.loader(false);
        });
    },

    //POST and UPDATE
    postAccommodation() {
      this.loader(true);
      this.prepareAmenities();
      this.$store.state.services.accommodationService
        .add(this.accommodation,this.$refs.file.files[0])
        .then(response => {
          this.$router.push({ name: "AccommodationIndex"});
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    updateAccommodation() {
      this.loader(true);
      this.prepareAmenities();
      this.$store.state.services.accommodationService
        .update(this.accommodation,this.$refs.file.files[0])
        .then(response => {
          this.$router.push({ name: "AccommodationIndex" });
        })
        .finally(()=>{
          this.loader(false);
        });    
    },

    //HELPERS
    prepareAmenities() {
      //Prepared data for model binding
      var temp = this.accommodation.AccommodationAmenity;
      this.accommodation.AccommodationAmenity = [];
      temp.forEach(element => {
        this.accommodation.AccommodationAmenity.push({ AmenityId: element });
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
    }
  },
  created() {
    if (this.$route.name == "AccommodationEdit") {
      this.getAccommodation();
      this.isUpdate = true;
    } else {
      this.isUpdate=false;
    }
    this.getCities();
    this.getAccommodationType();
    this.getAmenities();
  },
  beforeRouteLeave(to, from, next) {
    if (this.$route.name == "AccommodationEdit") {
      this.isUpdate = false;
      this.accommodation = {};
    }
    next();
  }

};
</script>
<style scoped>
.accImage {
  width: 20%;
  display: block;
}
.fileBtn {
  display: block;
}
#accCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}

</style>
