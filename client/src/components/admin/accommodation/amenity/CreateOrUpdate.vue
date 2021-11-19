<template>
<div>
    <v-container class="p-0">
        <v-layout row wrap>
            <v-flex xs12>
                <v-form>
                    <v-text-field
                    box
                    v-if="isUpdate"
                    v-on:keyup.enter="updateAmenity"
                    label="Amenity"            
                    v-model="amenity.Name"></v-text-field>

                    <v-text-field
                    box
                    v-if="!isUpdate"
                    v-on:keyup.enter="addAmenity"
                    label="Amenity"            
                    v-model="amenity.Name"></v-text-field>

                    <div class="text-right">
                      <v-btn
                      round
                      v-if="isUpdate"
                      color="primary"
                      @click="updateAmenity"
                      >Edit</v-btn>

                      <v-btn
                      round
                      v-if="!isUpdate"
                      color="primary"
                      @click="addAmenity"
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
  props: ["isUpdate", "amenityId","amenityTypeId"],
  data() {
    return {
      amenity: {},
      acccommodationByCity: [],
      isCitySelected: false
    };
  },
  methods: {
    ...mapMutations({
      loader:"COMMIT_LOADER"
    }),
    //GETTERS
    getAmenity() {
      this.loader(true);
      this.$store.state.services.amenitiesService.get(this.amenityId).then(response => {
        this.amenity = response.data;
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    addAmenity() {
      this.loader(true);
      this.amenity.AmenityTypeId=this.amenityTypeId;
      console.log(this.amenityTypeId)
      this.$store.state.services.amenitiesService.add(this.amenity).then(response => {
        this.amenity = response.data;
        this.$emit("createNew");
        this.amenity={};
        this.isUpdate=false;
      })
      .finally(()=>{
        this.loader(false);
      });
    },

    //UPDATE
    updateAmenity() {
      this.loader(true);
      this.amenity.AmenityId=this.amenityId;
      this.$store.state.services.amenitiesService.update(this.amenity).then(response => {
        this.$emit("createNew");
        this.amenity={};
        this.isUpdate=false;
      })
      .finally(()=>{
        this.loader(false);
      });
    },
    editAmenity() {
      this.$emit("editAmenity", this.amenity.AmenityId);
    }
  },
  watch: {
    amenityId(newValue,old) {
      if(newValue==null)
        this.amenity.Name='';
      else
        this.getAmenity();
    },
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
