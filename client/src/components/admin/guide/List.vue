<template>
<div>
    <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                class="ml-3 left" 
                label="Search by name"
                v-on:keyup.enter="getGuideByName"
                v-model="guideName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
                <v-btn
                :icon="true"
                :rounded="true"
                class="create-search-button"
                v-if="!isSearchActive"
                color="primary"
                @click="getGuideByName">
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
                <v-btn class="create-search-button" :icon="true" :rounded="true" to="/AdminHome/Guides/CreateNew" color="primary"><v-icon>add</v-icon></v-btn>
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
    :items="guides" 
    hide-actions 
    class="elevation-1">
        <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.FirstName }}</td>
            <td class="text-xs-left">{{ props.item.LastName }}</td>
            <td class="text-xs-right">
              <v-icon  @click="$router.push('/AdminHome/Guide/Edit/' + props.item.GuideId)" color="primary">edit</v-icon>
                <!-- <v-btn color="secondary" :to="'/AdminHome/Guide/Edit/' + props.item.GuideId">Edit</v-btn> -->
            </td>
        </template>
    </v-data-table>
    <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getGuideByIndex" 
        v-model="page" 
        :length="guideCount"></v-pagination>
    </div>
</div>
</template>

<script>
import {mapMutations} from "vuex";
export default {
  data() {
    return {
      guides: [],
      guideName: "",
      isSearchActive: false,
      infoMessage: "",
      infoMessageShow: false,
      headers: [
        {
          text: "First name",
          align: "left",
          value: "FirstName"
        },
        {
          text: "Last name",
          value: "LastName"
        },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          align: "right"
        }
      ],
      //pagination
      page: 1,
      guideCount: 0
    };
  },
  methods: {
    ...mapMutations({
      loader:"COMMIT_LOADER"
    }),
    getGuideCount() {
      this.$store.state.services.guideService
        .getGuideCount()
        .then(response => {
          this.guideCount = response.data;
        });
    },
    getGuideByIndex() {
      this.loader(true);
      this.$store.state.services.guideService
        .getByIndex(this.page - 1)
        .then(response => {
          this.setInfoMessage("", false);
          this.guides = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    getGuideByName() {
      this.loader(true);
      this.isSearchActive = true;
      this.$store.state.services.guideService
        .getByName(this.guideName)
        .then(response => {
          this.setInfoMessage("", false);
          this.guides = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.guideName = "";
      this.getGuideByIndex();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    }
  },
  created() {
    this.getGuideByIndex();
    this.getGuideCount();
  }
};
</script>

<style scoped>

</style>
