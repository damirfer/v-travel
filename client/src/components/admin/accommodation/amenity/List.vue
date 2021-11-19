<template>
<div>
  <v-dialog v-model="amenityDialog" max-width="500px">
        <v-card>
            <v-card-title class="modal-header">
              <h2 
              v-if="isUpdate" 
              class="modal-title">Edit amenity</h2>
              <h2 
              v-else 
              class="modal-title">Create amenity</h2>
            </v-card-title>
            <v-card-text class="p-0 mb-0 modal-contet">
                <app-amenity              
                @createNew="newAmenity" 
                :amenityId="selectedAmenity"
                :amenityTypeId="amenityTypeId" 
                :isUpdate="isUpdate"></app-amenity>
            </v-card-text>
        </v-card>
    </v-dialog>
    <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                class="ml-3 left" 
                label="Search by name"
                v-on:keyup.enter="getAmenityByName"
                v-model="amenityName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
                <v-btn
                :icon="true"
                :rounded="true"
                class="create-search-button"
                v-if="!isSearchActive"
                color="primary"
                @click="getAmenityByName">
                    <v-icon>search</v-icon>
                </v-btn>
                <v-btn 
                :icon="true"
                :rounded="true"
                class="create-search-button"
                v-else
                color="primary"
                @click="clearSearch">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-flex>
            <v-flex xs6></v-flex>
            <v-flex class="create-search-button" xs2>
                <v-btn class="create-search-button" :icon="true" :rounded="true" @click='createAmenity()' color="primary"><v-icon>add</v-icon></v-btn>
            </v-flex>
        </v-layout>
    </div>
    <v-alert
    fixed
    top
    type="info"
    :value="infoMessageShow">
    {{ infoMessage }}
    </v-alert>
    <v-data-table :headers="headers" :items="amenites" hide-actions class="elevation-1">
        <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.Name }}</td>
            <td class="text-xs-right">
              <v-icon  @click="editAmenity(props.item.AmenityId);isUpdate=true;" color="primary">edit</v-icon>
                <!-- <v-btn color="secondary" @click="editAmenity(props.item.AmenityId);isUpdate=true;" >Edit</v-btn> -->
            </td>
        </template>
    </v-data-table>
    <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getAmenityByIndex" 
        v-model="page" 
        :length="amenityCount"></v-pagination>
    </div>
</div>
</template>

<script>
import { mapMutations } from "vuex";
import AmenityCU from "./CreateOrUpdate";
export default {
  components: {
    appAmenity: AmenityCU,
  },
  data() {
    return {
      amenites: [],
      amenityName: "",
      isSearchActive: false,
      infoMessage: "",
      infoMessageShow: false,
      amenityDialog: false,
      selectedAmenity:null,
      amenityTypeId:0,
      headers: [
        {
          text: "Amenity",
          align: "left",
          value: "AmenityName"
        },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          align: "right"
        }
      ],

      isUpdate: false,
      //pagination
      page: 1,
      amenityCount: 0
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    getAmenityCount() {
      this.$store.state.services.amenitiesService
        .getCount(this.amenityTypeId)
        .then(response => {
          this.amenityCount = response.data;
        });
    },
    getAmenityByIndex() {
      this.loader(true);
      this.$store.state.services.amenitiesService
        .getByIndex(this.page - 1,this.amenityTypeId)
        .then(response => {
          this.amenites = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    newAmenity(){

      this.amenityDialog=false;
      this.selectedAmenity = null;
      this.getAmenityByIndex(this.amenityTypeId);
      this.getAmenityCount(this.amenityTypeId);

    },
    editAmenity(amenityId) {
      this.amenityDialog=true;
      this.selectedAmenity=amenityId;
      this.isUpdate=true;
        
      },
      closeDialog(){
        this.amenityDialog=false;
      },
    createAmenity() {
      this.isUpdate = false;
      this.amenityDialog = true;
      this.selectedAmenity=null;
    },
    getAmenityByName() {
      this.loader(true);
      this.isSearchActive = true;
      this.$store.state.services.amenitiesService
        .getByName(this.amenityName,this.amenityTypeId)
        .then(response => {
          this.setInfoMessage("", false);
          this.amenites = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.amenityName = "";
      this.getAmenityByIndex(this.amenityTypeId);
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    }
  
  },
  created() {
    if (this.$route.name === "AccommodationAmenity"){
     this.amenityTypeId=1;
    }
    if(this.$route.name === "RoomAmenity"){
      this.amenityTypeId=2;
    }
    this.getAmenityByIndex(this.amenityTypeId);
    this.getAmenityCount(this.amenityTypeId);
  }
};
</script>

<style scoped>

</style>
