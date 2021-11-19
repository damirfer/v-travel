<template>
<div>
    <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                class="ml-3 left" 
                label="Search by name"
                v-on:keyup.enter="getAccommodationByName"
                v-model="accommodationName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
                <v-btn
                :icon="true"
                :rounded="true"
                v-if="!isSearchActive"
                color="primary"
                @click="getAccommodationByName">
                    <v-icon>search</v-icon>
                </v-btn>
                <v-btn
                :icon="true"
                :rounded="true"
                v-else
                color="primary"
                @click="clearSearch">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-flex>
            <v-flex xs6></v-flex>
            <v-flex class="create-search-button" xs2>
                <v-btn  to="/AdminHome/Accommodation/CreateNew" :icon="true" :rounded="true" color="primary"><v-icon>add</v-icon></v-btn>
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
    <v-data-table :headers="headers" :items="accommodation" hide-actions class="elevation-1">
        <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.Name }}</td>
            <td class="text-xs-left">{{ props.item.Type }}</td>
            <td class="text-xs-left">{{ props.item.City }}</td>
            <td class="text-xs-left">{{ props.item.Country }}</td>
            <td class="text-xs-right">
               <v-icon  @click="$router.push('/AdminHome/Accommodation/Edit/' + props.item.AccommodationId)" color="primary">edit</v-icon>
                <!-- <v-btn color="secondary" :to="'/AdminHome/Accommodation/Edit/' + props.item.AccommodationId">Edit</v-btn> -->
            </td>
        </template>
    </v-data-table>
    <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getAccommodationByIndex" 
        v-model="page" 
        :length="accommodationCount"></v-pagination>
    </div>
</div>
</template>

<script>
import {mapMutations} from "vuex";
export default {
  data() {
    return {
      accommodation: [],
      accommodationName: "",
      isSearchActive: false,
      infoMessage: "",
      infoMessageShow: false,
      headers: [
        {
          text: "Name",
          align: "left",
          value: "accommodationName"
        },
        {
          text: "Type",
          value: "accommodationType"
        },
        {
          text: "City",
          value: "city"
        },
        {
          text: "Country",
          value: "country"
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
      accommodationCount: 0
    };
  },
  methods: {
    ...mapMutations({
      loader:"COMMIT_LOADER"
    }),
    getAccommodationCount() {
      this.$store.state.services.accommodationService
        .getAccommodationCount()
        .then(response => {
          this.accommodationCount = response.data;
        })
    },
    getAccommodationByIndex() {
      this.loader(true);
      this.$store.state.services.accommodationService
        .getByIndex(this.page - 1)
        .then(response => {
          this.setInfoMessage("", false);
          this.accommodation = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getAccommodationByName() {
      this.loader(true);
      this.isSearchActive = true;
      this.$store.state.services.accommodationService
        .getByName(this.accommodationName)
        .then(response => {
          this.setInfoMessage("", false);
          this.accommodation = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.accommodationName = "";
      this.getAccommodationByIndex();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    }
  },
  created() {
    this.getAccommodationByIndex();
    this.getAccommodationCount();
  }
};
</script>

<style scoped>

</style>
