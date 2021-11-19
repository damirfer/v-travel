<template>
    <div>
      <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                class="ml-3 left" 
                label="Search by name"
                v-on:keyup.enter="getToursByName"
                v-model="tourName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
                <v-btn
                class="create-search-button"
                :icon="true"
                :rounded="true"
                v-if="!isSearchActive"
                color="primary"
                @click="getToursByName">
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
                <v-btn class="create-search-button" to="/AdminHome/Tour/CreateNew/false" :icon="true" :rounded="true" color="primary"><v-icon>add</v-icon></v-btn>
            </v-flex>
        </v-layout>
      </div>
      <v-data-table
      :headers="headers"
      :items="tours"
      hide-actions
      class="elevation-1">
        <template slot="items" slot-scope="props">
          <td class="text-xs-left">{{ props.item.Name }}</td>
          <td class="text-xs-left">{{ props.item.Duration }} days</td>
          <td class="text-xs-right">
            <v-icon class="edit-icons" @click="$router.push('/AdminHome/Tour/Edit/' + props.item.TourId)" color="primary">edit</v-icon>
            <v-icon class="edit-icons-last" @click="$router.push('/AdminHome/Tour/overview/' + props.item.TourId)" color="primary">map</v-icon>
            <!-- <v-btn 
            @click="editTour(props.item.TourId)"
            color="secondary">Edit</v-btn>

            <v-btn
            dark
            @click="tourOverview(props.item.TourId)"
            color="alternative1">Overview</v-btn> -->
          </td>
        </template>
    </v-data-table>
      <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getToursByIndex" 
        v-model="page" 
        :length="toursCount"></v-pagination>
    </div>
    </div>
</template>
<script>
import {mapMutations} from "vuex";
export default {
  data() {
    return {
      headers: [
        {
          text: "Tour name",
          align: "left",
          sortable: false,
          value: "name"
        },
        { text: "Duration", value: "duration" },
        { text: "Actions", value: "actions", align: "right", sortable: false }
      ],
      tours: [],
      tourName: "",
      //pagination
      page: 1,
      toursCount: 0,
      //search
      isSearchActive:false,
      tourName:""
    };
  },
  created() {
    this.getToursByIndex();
    this.getTourCount();
  },
  methods: {
    ...mapMutations({
      loader:"COMMIT_LOADER"
    }),
    getTours() {
      this.loader(true);
      this.$store.state.services.tourService.getAll().then(response => {
        this.tours = response.data;
        console.log(this.tours);
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    editTour(tourId) {
      this.$router.push({
        name: "TourEdit",
        params: { tourId: tourId, isUpdate: true }
      });
    },
    tourOverview(tourId) {
      this.$router.push({ name: "TourOverview", params: { tourId: tourId } });
    },
    getTourCount() {
        this.$store.state.services.tourService.getTourCount().then(response => {
            this.toursCount = response.data;
        });
    },
    getToursByIndex() {
      this.loader(true);
        this.$store.state.services.tourService.getByIndex(this.page - 1).then(response => {
            this.tours = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getToursByName() {
      this.loader(true);
        this.isSearchActive = true;
        this.$store.state.services.tourService.getByName(this.tourName).then(response => {
            this.tours = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch(){
        this.isSearchActive = false;
        this.page = 1;
        this.tourName = "";
        this.getToursByIndex();
    }

  }
};
</script>

<style scoped>

</style>
