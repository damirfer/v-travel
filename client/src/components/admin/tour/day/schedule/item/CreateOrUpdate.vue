<template>
  <div>
    <v-form>
      <div>
        <v-menu
          ref="menu"
          :close-on-content-click="false"
          v-model="timePicker"
          :nudge-right="40"
          :return-value.sync="time"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          max-width="290px"
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="time"
            label="Picker in menu"
            prepend-icon="access_time"
            readonly
          ></v-text-field>
          <v-time-picker
            v-if="timePicker"
            v-model="time"
            full-width
            @change="$refs.menu.save(time)"
          ></v-time-picker>
        </v-menu>
      </div>
      <div>
        <v-textarea v-model="selectedSchedule.Content" label="Content"></v-textarea>

        <v-select
          :items="placeTypes"
          v-model="selectedPlaceType"
          label="Select place type"
          item-text="name"
          item-value="typeId"
        ></v-select>

        <v-select
          v-if="selectedPlaceType === 1"
          :items="Places"
          v-model="selectedSchedule.PlaceId"
          label="Visiting place"
          chips
          deletable-chips
          item-text="Name"
          item-value="PlaceId"
        ></v-select>

        <v-select
          v-if="selectedPlaceType === 2"
          :items="Accommodations"
          v-model="selectedSchedule.AccommodationId"
          label="Select accommodation"
          chips
          deletable-chips
          item-text="Name"
          item-value="AccommodationId"
        ></v-select>
      </div>
      <div class="text-right">
        <v-btn round v-if="isUpdate" @click="updateScheduleItem" color="primary">Update</v-btn>

        <v-btn round v-else @click="createScheduleItem" color="primary">Add</v-btn>
      </div>
    </v-form>
  </div>
</template>

<script>
export default {
  props: ["schedule", "isUpdate", "isDialog"],
  data() {
    return {
      placeTypes: [
        {
          typeId: 1,
          name: "Place"
        },
        {
          typeId: 2,
          name: "Accommodation"
        }
      ],
      selectedPlaceType: 1,
      selectedSchedule: {},
      Places: [],
      Accommodations: [],

      //time picker
      timePicker: false,
      time: "08:00"
    };
  },
  methods: {
    updateScheduleItem() {
      this.selectedSchedule.TimeStamp = this.time;
      this.checkPlaceType();
      this.time = "08:00";
      this.$emit("updateScheduleItem", this.selectedSchedule);
    },
    createScheduleItem() {
      this.selectedSchedule.TimeStamp = this.time;
      this.checkPlaceType();
      this.time = "08:00";
      this.$emit("createScheduleItem", this.selectedSchedule);
    },
    checkPlaceType() {
      if (this.selectedPlaceType === 1) {
        delete this.selectedSchedule.AccommodationId;
      } else {
        delete this.selectedSchedule.PlaceId;
      }
    },
    getPlaces() {
      this.$store.state.services.placeService.getAll().then(response => {
        this.Places = response.data;
      });
    },
    getAccommodation() {
      this.$store.state.services.accommodationService
        .getAll()
        .then(response => {
          this.Accommodations = response.data;
        });
    }
  },
  created() {
    this.getPlaces();
    this.getAccommodation();
  },
  watch: {
    schedule() {
      //JSON.stringify() - used to asign prop object - schedule by value not by reference
      this.selectedSchedule = JSON.parse(JSON.stringify(this.schedule));
      this.time = this.schedule.TimeStamp.split(" ")[0];
      console.log("this.selectedSchedule");
      console.log(this.selectedSchedule);
    }
  }
};
</script>

<style scoped>
.btn {
  float: right;
  margin-right: 10px;
}

.schedule-single {
  position: relative;
}

/* FORM */

.time-input {
  margin: 0;
  width: 100%;
}
</style>
