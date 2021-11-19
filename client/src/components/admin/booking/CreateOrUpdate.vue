<template>
  <v-container fluid pt-5>
    <v-layout row wrap>
      <v-flex xs12 lg6 mb-4>
        <v-date-picker
          landscape
          color="primary"
          v-model="selectedDate"
        ></v-date-picker>
      </v-flex>
      <v-flex xs12 lg6>
        <v-layout>
          <v-flex xs12 lg8>
            <v-form ref="form">
              <v-text-field
                v-model="booking.Name"
                label="Booking name"
                required
                box
              ></v-text-field>

              <v-select
                :items="tours"
                v-if="!isUpdate"
                v-model="booking.TourId"
                item-text="Name"
                item-value="TourId"
                label="Select tour you want to activate"
                box
              ></v-select>

              <v-select
                v-model="booking.GuideId"
                :items="guides"
                item-text="FirstName"
                item-value="GuideId"
                label="Select tour leader for the trip"
                box
              ></v-select>
              <v-layout flex justify-end>
                <v-btn
                  round
                  color="primary"
                  class="height-auto py-3 px-4"
                  v-if="!isUpdate"
                  @click="createBooking"
                  >Create booking</v-btn
                >
                <v-btn
                  round
                  depressed
                  color="primary"
                  class="height-auto py-3 px-4"
                  v-else
                  @click="updateBooking"
                  >Update booking</v-btn
                >
              </v-layout>
            </v-form>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
    <!-- <app-traveler-list
      class="py-3 px-4"
      v-if="isUpdate"
      :bookingId="this.$route.params.bookingId"
    >
    </app-traveler-list> -->
  </v-container>
  <!-- <div id="bookingCreateOrUpdate">

    <v-alert
    fixed
    top
    type="info"
    :value="infoMessageShow">
    {{ infoMessage }}
    </v-alert>

    <v-form ref="form">
      <v-text-field
      v-model="booking.Name"
      label="Booking name"
      required
      ></v-text-field>

      <v-menu
      ref="menu"
      :close-on-content-click="false"
      v-model="dateMenu" :nudge-right="40"
      :return-value.sync="selectedDate"
      lazy
      transition="scale-transition"
      offset-y
      full-width
      min-width="290px">

      <v-text-field
      slot="activator"
      v-model="selectedDate"
      label="Picker in menu"
      prepend-icon="event"
      readonly></v-text-field>

      <v-date-picker
      v-model="selectedDate"
      no-title scrollable
      @input="$refs.menu.save(selectedDate)"
      >
          <v-spacer></v-spacer>
          <v-btn flat color="primary" @click="dateMenu = false">Cancel</v-btn>
          <v-btn flat color="primary" @click="$refs.menu.save(selectedDate)">OK</v-btn>
      </v-date-picker>
      </v-menu>

      <v-select
      v-model="booking.TourId"
      :items="tours"
      item-text="Name"
      item-value="TourId"
      label="Select tour you want to activate"
      ></v-select>

      <v-select
      v-model="booking.GuideId"
      :items="guides"
      item-text="FirstName"
      item-value="GuideId"
      label="Select tour leader for the trip"
      ></v-select>

      <v-btn v-if="!isUpdate" @click="createBooking">Create booking</v-btn>
      <v-btn v-else @click="updateBooking">Update booking</v-btn>


    </v-form>
    <app-traveler-list
    v-if="isUpdate"
    :bookingId="this.$route.params.bookingId"
    >
    </app-traveler-list>
</div> -->
</template>

<script>
import { mapMutations } from "vuex";
import TravelerList from "./traveler/List";
export default {
  components: {
    appTravelerList: TravelerList,
  },
  data() {
    return {
      booking: {},
      dateMenu: false,
      selectedDate: null,
      tours: [],
      guides: [],
      isUpdate: true,
      infoMessage: "",
      infoMessageShow: false,
    };
  },
  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getBooking() {
      this.loader(true);
      this.$store.state.services.bookingService
        .get(this.$route.params.bookingId)
        .then((response) => {
          console.log(response);
          this.booking = response.data;
          this.selectedDate = this.booking.DateFrom;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getTours() {
      this.loader(true);
      this.$store.state.services.tourService
        .getAll()
        .then((response) => {
          this.tours = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getGuides() {
      this.loader(true);
      this.$store.state.services.guideService
        .getAll()
        .then((response) => {
          this.guides = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    createBooking() {
      this.loader(true);
      this.booking.DateFrom = this.selectedDate;
      this.$store.state.services.bookingService
        .add(this.booking)
        .then((response) => {
          this.$router.push({ name: "BookingIndex" });
        })
        .finally(() => {
          this.loader(false);
        });
    },
    updateBooking() {
      this.loader(true);
      this.booking.DateFrom = this.selectedDate;
      this.$store.state.services.bookingService
        .update(this.booking)
        .then((response) => {
          this.$router.push({ name: "BookingIndex" });
        })
        .finally(() => {
          this.loader(false);
        });
    },
  },
  created() {
    this.getTours();
    this.getGuides();

    if (this.$route.name == "BookingCreate") {
      this.isUpdate = false;
    } else {
      this.isUpdate = true;
      this.getBooking();
    }
  },
  beforeRouteLeave(to, from, next) {
    if (to.name == "BookingCreate") {
      this.isUpdate = false;
      this.booking = {};
      this.selectedDate = "";
    }
    next();
  },
};
</script>

<style scoped>
#bookingCreateOrUpdate {
  width: 75%;
  margin: 20px auto;
}
</style>
