<template>
    <div id="tourCreateOrUpdate">
        <v-form>
          <v-layout row>
            <v-flex xs12 lg6 px-2>
               <v-text-field
                label="Tour name"
                v-model="Tour.Name"
                box></v-text-field>

                <v-text-field
                label="Number of days"
                v-model="Tour.Duration"
                type="number"
                box
                ></v-text-field>
            </v-flex>
            <v-flex xs12 lg6 px-2>
              

              	<v-btn color="primary"
                round
                class="fileBtn"
                @click="onFileEdit">Choose image</v-btn>

                <input type="file" ref="file" 
                @change="takeImagePath($event)" style="display:none"/>
              
                <img 
                class="tourImage ml-2" 
                :src="imagePreview" 
                v-if="imagePreview != null"/>

                
            </v-flex>
            
          </v-layout>
          <v-layout row wrap>
            <v-flex xs12 px-2>
              <v-select
                :items="countries"
                v-model="Tour.TourCountry"
                label="Select tour countries"
                multiple
                chips
                item-text="Name"
                item-value="CountryId"
                persistent-hint
                box
                ></v-select>

              <v-textarea
              v-model="Tour.Description"
              label="Description"
              box
              ></v-textarea>
            </v-flex>
          </v-layout>

            

            

            <div id="tour-section-container" class="clearAfterFloats">
                <h2 class="font-weight-light secondary--text" id="tour-sections-title">Tour sections</h2>
                <div style="text-align:left;">
                    <v-dialog
                    :style="{'z-index': '3000!important'}"
                    v-model="sectionDialog"
                    max-width="500"
                    >
                    <v-btn
                      dark
                      depressed
                      round
                      color="primary"
                      slot="activator"
                      class="ma-2">
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
                            v-model="newSection.Content"
                            box
                            label="Section content"
                            ></v-textarea>
                            <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn
                                color="primary"
                                
                                round
                                @click="createNewSection">
                                Create
                            </v-btn>
                            </v-card-actions>
                        </v-form>
                    </div>
                    </v-dialog>
                </div>
                <app-tour-section
                v-for="section in Tour.Sections"
                :key="section.TourSectionId"
                :section="section"
                @deleteSection="deleteSection"
                class="tour-section"></app-tour-section>
            </div>


            <v-btn v-if="!isUpdate" round depressed color="primary" class="height-auto py-3 px-4" @click="postTour">Create tour</v-btn>
            <v-btn v-else round depressed color="primary" class="height-auto py-3 px-4" @click="editTour">Update tour</v-btn>

        </v-form>
    </div>
</template>

<script>
import {mapMutations} from "vuex";
import tourSection from "./Section";
export default {
  props:["id"],
  components:{
    appTourSection:tourSection
  },
  data() {
    return {
      Tour: {
        TourCountry: [],
        Sections: []
      },
      dialog:false,
      newSection: {
        Title: "",
        Content: ""
      },
      imagePreview: null,
      countries: [],
      isUpdate: false,
      sectionDialog: false,
      sectionInfoMessageShow: false
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    postTour() {
      this.loader(true);
      this.prepareCountries();
      this.$store.state.services.tourService
      .add(this.Tour,this.$refs.file.files[0])
      .then(response => {
        this.$router.push({ name: "TourIndex" });
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    editTour() {
      this.loader(true);
      this.prepareCountries();
      this.$store.state.services.tourService
        .update(this.Tour,this.$refs.file.files[0])
        .then(response => {
          this.$router.push({ name: "TourIndex" });
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getTour() {
      this.loader(true);
      this.$store.state.services.tourService
        .get(this.$route.params.tourId)
        .then(response => {
          this.Tour = response.data;
          this.imagePreview=this.Tour.PhotoUrl;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getCountries() {
      this.loader(true);
      this.$store.state.services.countryService.getAll().then(response => {
        this.countries = response.data;
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    prepareCountries() {
      //Prepared data for model binding
      var temp = this.Tour.TourCountry;
      this.Tour.TourCountry = [];
      temp.forEach(element => {
        this.Tour.TourCountry.push({ CountryId: element });
      });
    },
    deleteSection(id) {
      this.Tour.Sections = this.Tour.Sections.filter(section => {
        //TODO -> Add validation for section title
        return section.Title != id;
      });
    },
    createNewSection() {
      var sameSectionName = false;
      this.Tour.Sections.forEach(section => {
        if (section.Title == this.newSection.Title) {
          this.sectionInfoMessageShow = true;
          this.infoMessage = "Section with this title already exists";
          sameSectionName = true;
          return;
        }
      });
      //If true break function execution
      if (sameSectionName) return;

      this.Tour.Sections.push({
        Title: this.newSection.Title,
        Content: this.newSection.Content
      });
      this.newSection.Title = "";
      this.newSection.Content = "";
      this.sectionInfoMessageShow = false;
      this.sectionDialog = false;
    },
    takeImagePath(event){
      const file = event.target.files[0];
      let filename = file.name;
      if(filename.lastIndexOf(".") <= 0){
        return alert("Please select valid file");
      }

      const fileReader = new FileReader();

      fileReader.addEventListener("load", () => {
        this.imagePreview = fileReader.result;
      })

      fileReader.readAsDataURL(file);
      this.Tour.PhotoUrl  = file.name;
      console.log(this.Tour.PhotoUrl);
    },




    onFileEdit(){
      this.$refs.file.click();
    }
  },
  created() {
    if (this.$route.name == "TourEdit") {
      this.isUpdate = true;
      this.getTour();
    }else{
      this.isUpdate=false;
    }
    this.getCountries();
  },
  beforeRouteLeave(to, from, next) {
    if (to.params.isUpdate != true) {
      this.Tour = {};
      this.isUpdate = false;
    }
    next();
  }
};
</script>

<style scoped>
.tourImage {
  width: 30%;
  display: block;
}
.fileBtn {
  display: block;
}
#tourCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}
.tour-section {
  width: calc(33.3% - 20px);
  float: left;
  margin: 25px 10px;
  padding: 20px;
  cursor: pointer;
}
.tour-section:hover {
  background-color: lightgray;
}
#tour-sections-title {
  font-family: "Montserrat", sans-serif;
  margin: 20px 0 30px 0;
  color: #0284a8 !important;
  text-transform: uppercase;
}
body /deep/ .v-dialog__content {
  z-index: 3000!important;
}

#dialog-content {
  background-color: white;
  padding: 20px;
}
</style>
<!--
TODO -> Prepraviti input za dodavanje slike 
-->