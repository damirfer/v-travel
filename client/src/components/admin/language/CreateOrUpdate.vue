<template>
  <div>
    <!-- <div style="padding-left: 230px;" class="title-bar">create or update language</div> -->

    <v-container class="p-0">
        <v-layout row wrap>
            <v-flex xs12>
                <v-form ref="form" v-model="valid">
                    <v-text-field
                    class="modal-content"
              v-if="isUpdate"
              v-on:keyup.enter="updateLanguage"
                    :rules="nameRules"
                    :counter="10"
                    label="Language"         
                    v-model="language.Name"></v-text-field>

                    <v-text-field
              class="modal-content"
              v-if="!isUpdate"
              :rules="nameRules"
                    :counter="10"
              v-on:keyup.enter="addLanguage"
              label="Language"
              v-model="language.Name"
            ></v-text-field>

<div class="modal-actions">
                    <v-btn
                    class="modal-buttons"
                    :absolute="true"
                    v-if="isUpdate"
                    color="primary"
                    @click="updateLanguage"
                    :disabled="!valid"
                    >Edit</v-btn>


                    <v-btn
                    class="modal-buttons"
                    v-if="!isUpdate"
                    :absolute="true"
                    color="primary"
                    @click="addLanguage"
                    :disabled="!valid"
                    >Create</v-btn>
</div>
                </v-form>
            </v-flex>
        </v-layout>
    </v-container>
  </div>
</template>
<script>
import { mapMutations } from "vuex";
export default {
  props: ["isUpdate", "languageId"],
  data() {
    return {
      valid:true,
      language: {},
      acccommodationByCity: [],
      isCitySelected: false,
      nameRules: [
      v => !!v || 'Field is required',
    ],
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    //GETTERS
    getLanguage() {
      this.loader(true);
      this.$store.state.services.languageService
        .get(this.languageId)
        .then(response => {
          this.language = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    addLanguage() {
      this.loader(true);
      this.$store.state.services.languageService
        .add(this.language)
        .then(response => {
          this.language = response.data;
          this.$emit("createNew");
          this.language = {};
          this.isUpdate = false;
        })
        .finally(() => {
          this.loader(false);
        });
    },

    //UPDATE
    updateLanguage() {
      this.loader(true);
      this.language.LanguageId = this.languageId;
      this.$store.state.services.languageService
        .update(this.language)
        .then(response => {
          this.$emit("createNew");
          this.language = {};
          this.isUpdate = false;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    editLanguage() {
      this.$emit("editLanguage", this.language.LanguageId);
    }
  },
  watch: {
    languageId(newValue, old) {
      console.log(newValue);
      console.log(old);
      if (newValue == null) this.language.Name = "";
      else this.getLanguage();
    }
  }
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
</style>
