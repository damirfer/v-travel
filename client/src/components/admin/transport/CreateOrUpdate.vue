<template>
  <div id="transport-container">
    <v-layout row wrap>
      <v-flex xs12 md6 class="pr-md-1">
        <v-text-field
          box
          v-model="model.Name"
          label="Name"
          required
        ></v-text-field>

        <v-select
          box
          :items="cities"
          v-model="model.CityId"
          label="Select city"
          item-text="Name"
          item-value="CityId"
          required
        ></v-select>
      </v-flex>
      <v-flex xs12 md6 class="pl-md-1">
        <v-text-field
          box
          v-model="model.Phone"
          label="Phone"
          required
        ></v-text-field>

        <v-select
          box
          :items="transportTypes"
          v-model="model.TransportTypeId"
          label="Select transport type"
          item-text="Name"
          item-value="TransportTypeId"
          required
        ></v-select>
      </v-flex>
    </v-layout>

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

    <v-btn round color="primary" v-if="isUpdate" @click="handleSubmit"
      >Update transport</v-btn
    >
    <v-btn round color="primary" v-else @click="handleSubmit"
      >Create new transport</v-btn
    >
  </div>
</template>

<script>
import { mapMutations } from "vuex";
export default {
  data() {
    return {
      model: {
        WebsiteUrl: "",
      },
      cities: [],
      transportTypes: [],
      imagePreview: null,
    };
  },
  computed: {
    isUpdate() {
      return this.$route.params.id !== undefined;
    },
  },
  created() {
    this.getCities();
    this.getTransportTypes();
    if (this.$route.params.id) {
      this.getTransport();
    }
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getTransport() {
      this.$store.state.services.transportService
        .get(this.$route.params.id)
        .then((response) => {
          this.model = response.data;
          this.model.CityId = parseInt(response.data.City);
          this.imagePreview = response.data.PhotoUrl;
        });
    },
    getCities() {
      this.$store.state.services.cityService
        .getAll()
        .then((response) => {
          this.cities = response.data;
        })
        .finally(() => {});
    },
    getTransportTypes() {
      this.$store.state.services.transportTypeService
        .getAll()
        .then((response) => {
          this.transportTypes = response.data;
        })
        .finally(() => {});
    },
    handleSubmit() {
      this.loader(true);
      if (this.$route.params.id) {
        this.$store.state.services.transportService
          .update(this.model, this.$refs.file.files[0])
          .then((res) => {
            this.$router.push({ name: "TransportList" });
          })
          .finally(() => {
            this.loader(false);
          });
      } else {
        this.model.PhotoUrl = "";
        this.$store.state.services.transportService
          .add(this.model, this.$refs.file.files[0])
          .then((res) => {
            this.$router.push({ name: "TransportList" });
          })
          .finally(() => {
            this.loader(false);
          });
      }
    },
    onFileEdit() {
      this.$refs.file.click();
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
      this.model.PhotoUrl = formData;
    },
  },
};
</script>

<style scoped>
#transport-container {
  width: 75%;
  margin: 20px auto;
}
.guideImage {
  width: 30%;
  display: block;
}
.fileBtn {
  display: block;
}
</style>