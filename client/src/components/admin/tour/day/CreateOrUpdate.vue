<template>
<div>
    <v-container class="p-0">
        <v-layout row wrap>
            <v-flex xs12>
                <v-form>
                      <v-layout row wrap>
                        <v-flex xs6 pa-3>
                            <v-text-field label="Day" v-model="day.Name"></v-text-field>

                            <v-select label="Select country" :items="countriesByTour" multiple chips v-model="day.Countries" item-text="Name" item-value="CountryId"></v-select>

                            <v-select label="Select city" id="citySelect" :items="citiesByCountry" multiple chips v-model="day.Cities" item-text="Name" item-value="CityId"></v-select>

                            <img class="dayImage" :src="imagePreview" v-if="imagePreview != null">
                            <v-btn round color="primary" class="fileBtn" @click="onFileEdit">Choose image</v-btn>

                            <input type="file" ref="file" @change="takeImagePath($event)" style="display:none">

                        </v-flex>
                        <v-flex xs6 pa-3>
                            <v-select label="Select hotel" id="accommodationSelect" :items="acccommodationByCity" v-model="day.AccommodationId" item-text="Name" item-value="AccommodationId"></v-select>

                            <p>Included meals:</p>
                            <v-layout row>
                                <v-flex d-flex justify-center>
                                    <v-checkbox class="text-center" color="primary" v-model="Meals" label="B" value="b"></v-checkbox>

                                    <v-checkbox class="inline-block" color="primary" v-model="Meals" label="L" value="l"></v-checkbox>

                                    <v-checkbox class="inline-block" color="primary" v-model="Meals" label="D" value="d"></v-checkbox>

                                </v-flex>
                            </v-layout>

                           
                            <v-textarea label="Description" v-model="day.Description"></v-textarea>
                        </v-flex>
                    </v-layout>
                    <v-flex xs12 text-xs-right>
                      <v-btn v-if="isUpdate" color="primary" round @click="updateDay">Save</v-btn>
                      <v-btn v-if="!isUpdate" color="primary" round>Create</v-btn>
                    </v-flex>
                </v-form>
            </v-flex>
        </v-layout>
    </v-container>
</div>
</template>

<script>
import {
    mapMutations
} from "vuex";
export default {
    props: ["isUpdate", "tourId", "dayId", "isDialogClosed"],
    data() {
        return {
            day: {
                IsBIncluded: null,
                IsLIncluded: null,
                IsDIncluded: null,
            },
            Meals: [],
            countriesByTour: [],
            citiesByCountry: [],
            imagePreview: null,
            isCountrySelected: false,
            acccommodationByCity: [],
            isCitySelected: false
        };
    },
    methods: {
        ...mapMutations({
            loader: "COMMIT_LOADER"
        }),
        //GETTERS
        getDay() {
            // this.loader(true);
            this.$store.state.services.dayService.get(this.dayId).then(response => {
                this.day = response.data;
                var temp = [];
                if (this.day.IsBIncluded) temp.push("b");
                if (this.day.IsLIncluded) temp.push("l");
                if (this.day.IsDIncluded) temp.push("d");
                this.Meals = temp;
                this.imagePreview = this.day.PhotoUrl;
            });
            // .finally(()=>{
            //   this.loader(false);
            // });
        },
        getCountriesByTour() {
            this.$store.state.services.countryService
                .getByTour(this.tourId)
                .then(response => {
                    this.countriesByTour = response.data;
                });
        },

        //TEMP SOLUTION
        getCities() {
            this.$store.state.services.cityService.getAll().then(response => {
                this.citiesByCountry = response.data;
            });
        },
        getAccomodations() {
            this.$store.state.services.accommodationService
                .getAll()
                .then(response => {
                    console.log(response.data);
                    this.acccommodationByCity = response.data;
                });
        },
        //UPDATE
        updateDay() {
            this.loader(true);
            if (this.Meals.includes("b")) {
                this.day.IsBIncluded = true;
            } else {
                this.day.IsBIncluded = false;
            }
            if (this.Meals.includes("l")) {
                this.day.IsLIncluded = true;
            } else {
                this.day.IsLIncluded = false;
            }
            if (this.Meals.includes("d")) {
                this.day.IsDIncluded = true;
            } else {
                this.day.IsDIncluded = false;
            }
            this.prepareCountries();
            this.prepareCities();
            this.$store.state.services.dayService
                .update(this.day, this.$refs.file.files[0])
                .then(response => {
                    this.$emit("updateDay", this.day.DayId);
                    this.$emit("closeDialog");
                })
                .finally(() => {
                    this.loader(false);
                });
        },

        //HELPERS
        prepareCountries() {
            var temp = this.day.Countries;
            this.day.Countries = [];
            this.day.DayCountry = [];
            temp.forEach(element => {
                this.day.DayCountry.push({
                    CountryId: element
                });
            });
        },
        prepareCities() {
            var temp = this.day.Cities;
            this.day.Cities = [];
            this.day.DayCity = [];
            temp.forEach(element => {
                this.day.DayCity.push({
                    CityId: element
                });
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
    watch: {
        tourId() {
            this.getCountriesByTour();
            this.getCities();
            this.getAccomodations();
        },
        dayId() {
            this.getDay();
        },
        isDialogClosed() {
            if (!this.isDialogClosed) {
                this.getDay();
            }
        }
    }

    // getCitiesByCountry() {
    //   var oldArray = this.citiesByCountry;
    //   this.isCountrySelected = true;
    //   var temp = [];
    //   var urlParams = [];
    //   this.day.Countries.forEach(country => {
    //     if (country.CountryId != undefined) urlParams.push(country.CountryId);
    //     else urlParams.push(country);
    //   });
    //   this.$store.state.services.cityService
    //     .getByCountry(urlParams)
    //     .then(response => {
    //       this.citiesByCountry = response.data;

    //       if (oldArray.length != 0) {
    //         if (this.citiesByCountry.length > oldArray.length) {
    //           temp = this.citiesByCountry.filter(function(obj) {
    //             return !oldArray.some(function(obj2) {
    //               return obj.CountryId == obj2.CountryId;
    //             });
    //           });
    //         } else {
    //           var t = this.citiesByCountry;
    //           temp = oldArray.filter(function(obj) {
    //             return !t.some(function(obj2) {
    //               return obj.CountryId == obj2.CountryId;
    //             });
    //           });
    //         }
    //         temp.forEach(element => {
    //           var el = element.CountryId;
    //           if (el != undefined && this.day.Cities != undefined) {
    //             if (this.day.Cities.includes(el)) {
    //               var elPos = this.day.Cities.indexOf(element.CountryId);
    //               this.day.Cities.splice(elPos, 1);
    //             }
    //           }
    //         });
    //       }
    //       temp = [];
    //       oldArray = this.citiesByCountry;
    //     });
    // },
    // getAccommodationByCity() {
    //   this.loader(true);
    //   this.isCitySelected = true;
    //   var urlParams = [];
    //   this.day.Cities.forEach(city => {
    //     if (city.CityId != undefined) urlParams.push(city.CityId);
    //     else urlParams.push(city);
    //   });
    //   this.$store.state.services.accommodationService
    //     .getByCity(urlParams)
    //     .then(response => {
    //       console.log(response.data);
    //       this.acccommodationByCity = response.data;
    //     })
    //     .finally(()=>{
    //       this.loader(false);
    //     });
    // },

    //SETTERS
    // setIsCountrySelected() {
    //   if (this.day.Countries != undefined) {
    //     if (this.day.Countries.length > 0) {
    //       this.isCountrySelected = true;
    //       this.getCitiesByCountry();
    //     } else {
    //       this.isCountrySelected = false;
    //     }
    //   }
    // },
    // setIsCitySelected() {
    //   if (this.day.Cities != undefined) {
    //     if (this.day.Cities.length > 0) {
    //       this.isCitySelected = true;
    //       this.getAccommodationByCity();
    //     } else {
    //       this.isCitySelected = false;
    //     }
    //   }
    // },
};
</script>

<style scoped>
.dayImage {
    width: 50%;
    display: block;
}

.fileBtn {
    display: block;
}
/* .inline-block {
    display: inline-block!important;
} */
</style>
