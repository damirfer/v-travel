<template>
  <div id="transport-container">
    <v-text-field box v-model="model.Name" label="Name" required></v-text-field>

    <v-btn round color="primary" v-if="isUpdate" @click="handleSubmit">Update transport</v-btn>
    <v-btn round color="primary" v-else @click="handleSubmit">Create new transport</v-btn>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
export default {
  data() {
    return {
      model: {},
    };
  },
  computed: {
    isUpdate() {
      return this.$route.params.id !== undefined;
    }
  },
  created() {
    if (this.$route.params.id) {
      this.getTransportType();
    }
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER"
    }),
    getTransportType() {
      this.$store.state.services.transportTypeService
        .get(this.$route.params.id)
        .then(response => {
          this.model = response.data;
        });
    },
    handleSubmit() {
      this.loader(true);
      if (this.$route.params.id) {
        this.$store.state.services.transportTypeService
          .update(this.model)
          .then(res => {
            this.$router.push({ name: "TransportTypeList" });
          })
          .finally(() => {
            this.loader(false);
          });
      } else {
        this.$store.state.services.transportTypeService
          .add(this.model)
          .then(res => {
            this.$router.push({ name: "TransportTypeList" });
          })
          .finally(() => {
            this.loader(false);
          });
      }
    }
  }
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