<template>
    <div class="section-container" 
        @mouseenter="isHover = !isHover" 
        @mouseleave="isHover = !isHover">

        <h3 style="color: #4d8182; margin-bottom: 15px;">{{ section.Title }}</h3>
        <p>{{ section.Content }}</p>

        <div 
        id="remove-country-section"
        v-if="isHover">
            <v-icon id="delete-icon" @click.stop="dialog = true, selectedSection=section.Title">delete</v-icon>
        </div>
        <v-dialog
      v-model="dialog"
      max-width="500"
    >
      <v-card>
        <v-card-title class="headline"><v-icon color="warning" style="display: inline-block; margin-right: 10px;">warning</v-icon>You are about to delete a section</v-card-title>

        <v-card-text>
          Are you sure you want to proceed and delete this section?
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            round
            color="primary"
            @click="dialog = false"
          >
            Abort
          </v-btn>

          <v-btn
            round
            color="primary"
            @click="dialog = false, deleteSection(selectedSection)"
          >
            Yes, proceed
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
          
</template>
<script>
export default {
    props: ['section'],
    data() {
        return {
            isHover: false,
            dialog: false,
            selectedSection: ""
        }
    },
    methods: {
        deleteSection(id){
            this.$emit("deleteSection",id);
        }
    }
}
</script>
<style scoped>
.section-container {
  position: relative;
  height: 250px;
  border-radius: 10px;
  box-shadow: 0 6px 15px rgba(0, 0, 0, 0.3);
  margin-bottom: 20px;
  overflow: hidden;
}
#remove-country-section {
  position: absolute;
  top: 5px;
  right: 5px;
  cursor: pointer;
}
#delete-icon:hover {
  color: red;
}
</style>
