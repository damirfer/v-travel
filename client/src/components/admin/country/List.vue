<template>
<div>
    <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                class="ml-3 left" 
                label="Search by name"
                v-model="searchCountryName"
                v-on:keyup.enter="getCountriesByName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
              <v-btn v-if="!isSearchActive" :icon="true" :round="true" color="primary" @click="getCountriesByName"><v-icon>search</v-icon></v-btn>
                <v-btn 
                v-else
                color="primary"
                icon="true"
                round="true"
                @click="clearSearch">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-flex>
            <v-flex xs6></v-flex>
            <v-flex class="create-search-button text-xs-right" xs2>
                <v-btn color="primary" :icon="true" :round="true"  @click="$router.push('/AdminHome/Countries/CreateNew')"><v-icon>add</v-icon></v-btn>
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
    <v-data-table :headers="headers" :items="countries" hide-actions class="elevation-1">
        <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.Name }}</td>
            <td class="text-xs-left">{{ props.item.CapitalCity }}</td>
            <td class="text-xs-left">{{ props.item.Currency }}</td>
            <td class="text-xs-right">
                
            <v-icon @click="$router.push('/AdminHome/Countries/Edit/' + props.item.CountryId)" color="primary">edit</v-icon>
                
            </td>
        </template>
    </v-data-table>
    <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getCountriesByIndex" 
        v-model="page" 
        :length="countriesCount"></v-pagination>
    </div>
</div>
</template>

<script>
import {mapMutations} from "vuex";
export default {
  data() {
    return {
      countries: [],
      searchCountryName: "",
      isSearchActive: false,
      infoMessage: "",
      infoMessageShow: false,
      headers: [
        {
          text: "Country name",
          align: "left",
          value: "countryName",
        },
        {
          text: "Capital city",
          value: "capitalCity"
        },
        {
          text: "Currency",
          value: "currency"
        },
        {
          text: " ",
          value: "actions"
        }
      ],
      //pagination
      page: 1,
      countriesCount: 0
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    getCountriesCount() {
      this.$store.state.services.countryService
        .getCountriesCount()
        .then(response => {
          this.countriesCount = response.data;
        });
    },
    getCountriesByIndex() {
      this.loader(true);
      this.$store.state.services.countryService
        .getByIndex(this.page - 1)
        .then(response => {
          this.setInfoMessage("", false);
          this.countries = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getCountriesByName() {
      this.loader(true);
      if (this.searchCountryName == "") {
        this.loader(false);
        return;
      }
      this.setInfoMessage("Loading...", true);
      this.isSearchActive = true;
      this.$store.state.services.countryService
        .getByName(this.searchCountryName)
        .then(response => {
          this.setInfoMessage("", false);
          this.countries = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.searchCountryName = "";
      this.getCountriesByIndex();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    }
  },
  created() {
    this.getCountriesByIndex();
    this.getCountriesCount();
  }
};
</script>

<style scoped>


</style>
