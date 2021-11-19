<template>
  <div>
    <div class="search-header">
      <v-layout row>
        <v-flex xs3 text-xs-left>
          <v-text-field
            class="ml-3 left"
            style="width: 100%"
            v-on:keyup.enter="getCitiesByName"
            label="Search by name"
            v-model="cityName"
          ></v-text-field>
        </v-flex>
        <v-flex class="create-search-button" xs1>
          <v-btn
            class="create-search-button"
            v-if="!isSearchActive"
            :icon="true"
            :rounded="true"
            color="primary"
            @click="getCitiesByName"
          >
            <v-icon>search</v-icon>
          </v-btn>
          <v-btn
            class="create-search-button"
            v-else
            color="primary"
            @click="clearSearch"
          >
            <v-icon>close</v-icon>
          </v-btn>
        </v-flex>
        <v-flex xs6></v-flex>
        <v-flex class="create-search-button" xs2>
          <v-btn
            :icon="true"
            :rounded="true"
            class="create-search-button"
            to="/AdminHome/Cities/CreateNew/false"
            color="primary"
            ><v-icon>add</v-icon></v-btn
          >
        </v-flex>
      </v-layout>
    </div>
    <v-data-table
      :headers="headers"
      :items="cities"
      hide-actions
      class="elevation-1"
    >
      <template slot="items" slot-scope="props">
        <td class="text-xs-left">{{ props.item.Name }}</td>
        <td class="text-xs-left">{{ props.item.Country }}</td>
        <td class="text-xs-right">
          <v-icon
            @click="$router.push('/AdminHome/Cities/Edit/' + props.item.CityId)"
            color="primary"
            >edit</v-icon
          >
          <!-- <v-btn color="secondary" :to="'/AdminHome/Cities/Edit/' + props.item.CityId">Edit</v-btn> -->
        </td>
      </template>
    </v-data-table>

    <div v-if="!isSearchActive" class="text-xs-center pa-3">
      <v-pagination
        @input="getCitiesByIndex"
        v-model="page"
        :length="citiesCount"
      ></v-pagination>
    </div>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
export default {
  data() {
    return {
      cities: [],
      cityName: "",
      isSearchActive: false,
      headers: [
        {
          text: "Name",
          align: "left",
          value: "cityName",
        },
        {
          text: "Country",
          value: "Country",
        },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          align: "right",
        },
      ],
      //pagination
      page: 1,
      citiesCount: 0,
    };
  },

  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getCitiesCount() {
      this.$store.state.services.cityService
        .getCitiesCount()
        .then((response) => {
          this.citiesCount = response.data;
        });
    },
    getCitiesByIndex() {
      this.loader(true);
      this.$store.state.services.cityService
        .getByIndex(this.page - 1)
        .then((response) => {
          this.cities = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getCitiesByName() {
      this.loader(true);
      this.isSearchActive = true;
      this.$store.state.services.cityService
        .getByName(this.cityName)
        .then((response) => {
          this.cities = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.cityName = "";
      this.getCitiesByIndex();
    },
  },
  created() {
    this.getCitiesByIndex();
    this.getCitiesCount();
  },
};
</script>

<style scoped>
.cities-header {
  border-top: 2px solid #116549;
  background-color: white;
  border-bottom: 1px solid #116549;
  margin-bottom: 5px;
}
</style>
