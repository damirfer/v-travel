<template>
<div>
    <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                class="ml-3 left" 
                label="Search by name"
                v-on:keyup.enter="getPlaceByName"
                v-model="placeName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
                <v-btn
                :icon="true"
                :rounded="true"
                class="create-search-button"
                v-if="!isSearchActive"
                color="primary"
                @click="getPlaceByName">
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
                <v-btn :icon="true" :rounded="true" class="create-search-button" to="/AdminHome/Places/CreateNew" color="primary"><v-icon>add</v-icon></v-btn>
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
    <v-data-table 
    :headers="headers" 
    :items="places" 
    hide-actions 
    class="elevation-1">
        <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.Name }}</td>
            <td class="text-xs-right">
              <v-icon  @click="$router.push('/AdminHome/Places/Edit/' + props.item.PlaceId)" color="primary">edit</v-icon>
                <!-- <v-btn color="secondary" :to="'/AdminHome/Places/Edit/' + props.item.PlaceId">Edit</v-btn> -->
            </td>
        </template>
    </v-data-table>
    <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getPlaceByIndex" 
        v-model="page" 
        :length="placeCount"></v-pagination>
    </div>
</div>
</template>

<script>
import {mapMutations} from "vuex";
export default {
  data() {
    return {
      places: [],
      placeName: "",
      isSearchActive: false,
      infoMessage: "",
      infoMessageShow: false,
      headers: [
        {
          text: "Place name",
          align: "left",
          value: "FirstName"
        },
        {
          text: " ",
          value: "actions",
          sortable: false,
          align: "right"
        }
      ],
      //pagination
      page: 1,
      placeCount: 0
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    getPlaceCount() {
      this.$store.state.services.placeService
        .getPlaceCount()
        .then(response => {
          this.placeCount = response.data;
        });
    },
    getPlaceByIndex() {
      this.loader(true);
      this.$store.state.services.placeService
        .getByIndex(this.page - 1)
        .then(response => {
          this.setInfoMessage("", false);
          this.places = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getPlaceByName() {
      this.loader(true);
      this.isSearchActive = true;
      this.$store.state.services.placeService
        .getByName(this.placeName)
        .then(response => {
          this.setInfoMessage("", false);
          this.places = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.placeName = "";
      this.getPlaceByIndex();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    }
  },
  created() {
    this.getPlaceByIndex();
    this.getPlaceCount();
  }
};
</script>

<style scoped>

</style>
