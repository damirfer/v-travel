<template>
  <div id="guideCreateOrUpdate">
    <v-alert fixed top type="info" :value="infoMessageShow">
      {{ infoMessage }}
    </v-alert>
    <v-form ref="form" v-model="valid">
      <v-layout row wrap>
        <v-flex xs12 md6 class="pr-md-1">
          <v-text-field
            box
            :rules="nameRules"
            :counter="20"
            v-model="Guide.FirstName"
            label="First name"
            required
          ></v-text-field>
        </v-flex>
        <v-flex xs12 md6 class="pl-md-1">
          <v-text-field
            box
            v-model="Guide.LastName"
            :rules="nameRules"
            :counter="20"
            label="Last name"
            required
          ></v-text-field>
        </v-flex>
      </v-layout>
      <v-flex xs12 md12 class="pr-md-1">
        <v-text-field
          box
          :rules="nameRules"
          :counter="20"
          v-model="Guide.Username"
          label="Username"
          required
        ></v-text-field>
      </v-flex>
      <v-layout row wrap>
        <v-flex xs12 md6 class="pr-md-1">
          <v-text-field
            box
            :rules="[nameRules]"
            :counter="20"
            v-model="Guide.PasswordHash"
            label="Password"
            :type="'password'"
            required
          ></v-text-field>
        </v-flex>
        <v-flex xs12 md6 class="pl-md-1">
          <v-text-field
            box
            v-model="repeatPassword"
            :rules="[nameRules, passwordConfirmationRule]"
            :counter="20"
            label="Repeat password"
            required
            :type="'password'"
          ></v-text-field>
        </v-flex>
      </v-layout>

      <v-select
        box
        :items="languages"
        v-model="Guide.GuideLanguage"
        label="Select languages"
        multiple
        chips
        item-text="Name"
        item-value="LanguageId"
        persistent-hint
        required
        :rules="languageRules"
      ></v-select>

      <v-textarea box v-model="Guide.Bio" label="General info"></v-textarea>

      <img class="guideImage" :src="imagePreview" v-if="imagePreview != null" />

      <v-btn round color="primary" class="fileBtn" @click="onFileEdit"
        >Choose image</v-btn
      >

      <input
        type="file"
        ref="file"
        @change="takeImagePath($event)"
        style="display: none"
      />

      <v-btn
        round
        color="primary"
        :disabled="!valid"
        v-if="isUpdate"
        @click="updateGuide"
      >
        Update tour leader
      </v-btn>
      <v-btn round color="primary" :disabled="!valid" v-else @click="postGuide">
        Create new tour leader
      </v-btn>
    </v-form>
  </div>
</template>
<script>
import { mapMutations } from "vuex";
export default {
  props: ["id"],
  data() {
    return {
      repeatPassword: "",
      valid: true,
      isUpdate: true,
      Guide: {
        GuideLanguage: [],
      },
      imagePreview: null,
      languages: [],
      nameRules: [(v) => !!v || "Field is required"],
      //Main alert
      infoMessage: "",
      infoMessageShow: false,
      languageRules: [
        (v) => !!v || "Language is required",
        (v) => (v && v.length > 0) || "Language is requiered",
      ],
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getGuide() {
      this.loader(true);
      this.$store.state.services.guideService
        .get(this.$route.params.id)
        .then((response) => {
          this.setInfoMessage("", false);
          this.Guide = response.data;
          this.imagePreview = this.Guide.PhotoUrl;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    postGuide() {
      this.loader(true);
      this.prepareLanguages();
      this.Guide.PhotoUrl = "";
      console.log(this.Guide);
      this.$store.state.services.guideService
        .add(this.Guide, this.$refs.file.files[0])
        .then((response) => {
          //Redirect to country list
          this.$router.push({ name: "GuideList" });
        })
        .finally(() => {
          this.loader(false);
        });
    },
    updateGuide() {
      this.loader(true);
      this.prepareLanguages();
      this.$store.state.services.guideService
        .update(this.Guide, this.$refs.file.files[0])
        .then((response) => {
          this.$router.push({ name: "GuideList" });
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getLanguages() {
      this.loader(true);
      this.$store.state.services.languageService
        .getAll()
        .then((response) => {
          this.languages = response.data;
          console.log(this.languages);
        })
        .finally(() => {
          this.loader(false);
        });
    },
    prepareLanguages() {
      //Prepared data for model binding
      var temp = this.Guide.GuideLanguage;
      this.Guide.GuideLanguage = [];
      temp.forEach((element) => {
        this.Guide.GuideLanguage.push({ LanguageId: element });
      });
    },
    takeImagePath(event) {
      const file = event.target.files[0];
      let filename = file.name;
      if (filename.lastIndexOf(".") <= 0) {
        return alert("Please select valid file");
      }
      const fileReader = new FileReader();

      fileReader.addEventListener("load", () => {
        this.imagePreview = fileReader.result;
      });
      fileReader.readAsDataURL(file);
      let fileToUpload = this.$refs.file.files[0];
      let formData = new FormData();
      formData.append("fileToUpload", fileToUpload);
      this.Guide.PhotoUrl = formData;
    },
    onFileEdit() {
      this.$refs.file.click();
    },
    setInfoMessage(message, show) {
      this.infoMessageShow = show;
      this.infoMessage = message;
    },
  },
  created() {
    if (this.$route.name == "GuideEdit") {
      this.getGuide();
      this.isUpdate = true;
    } else {
      this.isUpdate = false;
    }
    this.getLanguages();
  },
  computed: {
    passwordConfirmationRule() {
      return () =>
        this.Guide.PasswordHash === this.repeatPassword ||
        "Password must match";
    },
  },
  beforeRouteLeave(to, from, next) {
    if (this.$route.name == "GuideEdit") {
      this.isUpdate = false;
      this.Guide = {};
      this.imagePreview = "";
    }
    next();
  },
};
</script>
<style scoped>
.guideImage {
  width: 30%;
  display: block;
}
.fileBtn {
  display: block;
}
#guideCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}
</style>
