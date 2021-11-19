<template>
  <div>
    <div class="search-header">
      <v-layout row>
        <v-flex xs3 text-xs-left>
          <v-text-field
            style="width: 100%"
            class="ml-3 left"
            label="Search by name"
            v-model="bookingName"
            v-on:keyup.enter="getBookingByName"
          ></v-text-field>
        </v-flex>
        <v-flex class="create-search-button" xs1>
          <v-btn
            class="create-search-button"
            v-if="!isSearchActive"
            color="primary"
            :icon="true"
            :rounded="true"
            @click="getBookingByName"
          >
            <v-icon>search</v-icon>
          </v-btn>
          <v-btn
            class="create-search-button"
            v-else
            color="primary"
            :icon="true"
            :rounded="true"
            @click="clearSearch"
          >
            <v-icon>close</v-icon>
          </v-btn>
        </v-flex>
        <v-flex xs6></v-flex>
        <v-flex class="create-search-button" xs2>
          <v-btn
            :icon="true"
            :rounded="true"
            class="create-search-button"
            to="/AdminHome/Booking/CreateNew"
            color="primary"
            ><v-icon>add</v-icon></v-btn
          >
        </v-flex>
      </v-layout>
    </div>
    <v-data-table
      :headers="headers"
      :items="bookings"
      hide-actions
      class="elevation-1"
    >
      <template slot="items" slot-scope="props">
        <td class="text-xs-left">{{ props.item.Name }}</td>
        <td class="text-xs-left">{{ props.item.DateFrom }}</td>
        <td class="text-xs-left">{{ props.item.GuideName }}</td>
        <!-- <td class="text-xs-left">{{props.item.TravelersCount}}</td> -->
        <td class="text-xs-right">
          <v-icon
            class="edit-icons"
            @click="
              $router.push(
                '/AdminHome/Booking/Travelers/' + props.item.BookingId
              )
            "
            color="primary"
            >supervised_user_circle</v-icon
          >

          <v-icon
            class="edit-icons"
            @click="
              $router.push('/AdminHome/Booking/Edit/' + props.item.BookingId)
            "
            color="primary"
            >card_travel</v-icon
          >

          <v-icon
            class="edit-icons"
            @click="$router.push('/AdminHome/Tour/Edit/' + props.item.TourId)"
            color="primary"
            >map</v-icon
          >

          <v-icon
            class="edit-icons-last"
            @click="
              $router.push('/AdminHome/Tour/overview/' + props.item.TourId)
            "
            color="primary"
            >near_me</v-icon
          >
        </td>
      </template>
    </v-data-table>
    <div class="text-xs-center pa-3">
      <v-pagination
        @input="getBookingsByIndex"
        v-model="page"
        :length="bookingCount"
      ></v-pagination>
    </div>
  </div>
</template>
<script>
import { mapMutations } from "vuex";
export default {
  data() {
    return {
      headers: [
        {
          text: "Booking name",
          align: "left",
          sortable: false,
        },
        {
          text: "Start date",
          align: "left",
          sortable: false,
          value: "sDate",
        },
        { text: "Tour leader", value: "tLeader", sortable: false },
        { text: "Actions", value: "actions", align: "right", sortable: false },
      ],
      bookingName: "",
      isSearchActive: false,
      bookings: [],
      //pagination
      page: 1,
      bookingCount: 0,
    };
  },

  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    getBookingsCount() {
      this.loader(true);
      this.$store.state.services.bookingService
        .getBookingsCount()
        .then((response) => {
          this.bookingCount = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getBookingsByIndex() {
      this.loader(true);
      this.$store.state.services.bookingService
        .getByIndex(this.page - 1)
        .then((response) => {
          this.bookings = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    getBookingByName() {
      this.loader(true);
      console.log(this.bookingName);
      this.isSearchActive = true;
      this.$store.state.services.bookingService
        .getByName(this.bookingName)
        .then((response) => {
          this.bookings = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    clearSearch() {
      this.isSearchActive = false;
      this.page = 1;
      this.bookingName = "";
      this.getBookingsByIndex();
    },
    editBooking(bookingId) {
      this.$router.push({
        name: "BookingEdit",
        params: { bookingId: bookingId },
      });
    },
    editTour(tourId) {
      this.$router.push({
        name: "TourEdit",
        params: { tourId: tourId, isUpdate: true },
      });
    },
    tourOverview(tourId) {
      this.$router.push({ name: "TourOverview", params: { tourId: tourId } });
    },
  },
  created() {
    this.getBookingsByIndex();
    this.getBookingsCount();
  },
};
</script>

<style scoped>
.edit-icons {
  margin-left: 0px !important;
}
</style>
