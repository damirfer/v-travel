<template>
<div>
  <v-dialog v-model="languageDialog" max-width="500px">
        <v-card>
            <v-card-title class="modal-header">
              <img src="/src/assets/logo-bijela.png" class="modal-logo">
              <h2 
              class="modal-title"
              v-if="isUpdate" 
              >Edit language</h2>
              
              <h2 
              class="modal-title"
              v-else 
              >Create language</h2>
              <v-icon @click.stop="languageDialog=false;" class="modal-close-icon" color="white" align="right">close</v-icon>
            </v-card-title>
            <v-card-text class="p-0">
                <app-language              
                @createNew="newLanguage" 
                :languageId="selectedLanguage" 
                :isUpdate="isUpdate"></app-language>
            </v-card-text>
            <!-- <v-card-actions class="modal-actions">
                <v-btn
                    class="modal-buttons"
                    v-if="isUpdate"
                    color="primary"
                    :absolute="true"
                    @click="updateLanguage"
                    >Edit</v-btn>

                    <v-btn
                    class="modal-buttons"
                    v-if="!isUpdate"
                    color="primary"
                    :absolute="true"
                    @click="addLanguage"
                    >Create</v-btn>
            </v-card-actions> -->
        </v-card>
    </v-dialog>
    <div class="search-header">
        <v-layout row>
            <v-flex xs3 text-xs-left>
                <v-text-field 
                style="width: 100%;"
                v-on:keyup.enter="getLanguageByName"
                class="ml-3 left" 
                label="Search by name"
                v-model="languageName"></v-text-field>
            </v-flex>
            <v-flex class="create-search-button" xs1>
                <v-btn
                :icon="true"
                :rounded="true"
                class="create-search-button"
                v-if="!isSearchActive"
                color="primary"
                @click="getLanguageByName">
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
                <v-btn :icon="true" :rounded="true" class="create-search-button" @click='createLanguage()' color="primary"><v-icon>add</v-icon></v-btn>
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
    <v-data-table :headers="headers" :items="languages" hide-actions class="elevation-1">
        <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.Name }}</td>
            <td class="text-xs-right">
              <v-icon @click="editLanguage(props.item.LanguageId);isUpdate=true;" color="primary">edit</v-icon>
              <!-- <v-icon @click="$router.push('/AdminHome/Languages/' + props.item.TourId)" color="primary">edit</v-icon> -->
                <!-- <v-btn color="secondary" @click="editLanguage(props.item.LanguageId);isUpdate=true;" >Edit</v-btn> -->
            </td>
        </template>
    </v-data-table>
    <div
     v-if="!isSearchActive"   
     class="text-xs-center pa-3">
        <v-pagination 
        @input="getLanguageByIndex" 
        v-model="page" 
        :length="languageCount"></v-pagination>
    </div>
</div>
</template>

<script>
import {mapMutations} from "vuex";
import LanguageCU from "./CreateOrUpdate";
export default {
  components: {
    appLanguage: LanguageCU,
  },
  data() {
    return {
      languages: [],
      languageName: "",
      isSearchActive: false,
      infoMessage: "",
      infoMessageShow: false,
      languageDialog: false,
      selectedLanguage:null,
      headers: [
        {
          text: "Language",
          align: "left",
          value: "LanguageName"
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
      languageCount: 0
    };
  },
  methods: {
    ...mapMutations({
      loader:"COMMIT_LOADER"
    }),
    getLanguageCount() {
      this.$store.state.services.languageService
        .getCount()
        .then(response => {
          this.languageCount = response.data;
        });
    },
    getLanguageByIndex() {
      this.loader(true);
      this.$store.state.services.languageService
        .getByIndex(this.page - 1)
        .then(response => {
          this.setInfoMessage("", false);
          this.languages = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    newLanguage(){

      this.languageDialog=false;
      this.selectedLanguage=null;
      this.getLanguageByIndex();
      this.getLanguageCount();

    },
    editLanguage(languageId) {
      this.languageDialog=true;
      this.selectedLanguage=languageId;
      this.isUpdate=true;
        
      },
      closeDialog(){
        this.languageDialog=false;
      },
    createLanguage() {
      this.isUpdate = false;
      this.languageDialog = true;
      this.selectedLanguage=null;
    },
    getLanguageByName() {
      this.loader(true);
      this.isSearchActive = true;
      this.$store.state.services.languageService
        .getByName(this.languageName)
        .then(response => {
          this.setInfoMessage("", false);
          this.languages = response.data;
        })
        .finally(()=>{
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.languageName = "";
      this.getLanguageByIndex();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    }
  
  },
  created() {
    this.getLanguageByIndex();
    this.getLanguageCount();
  },
};
</script>

<style scoped>

</style>
