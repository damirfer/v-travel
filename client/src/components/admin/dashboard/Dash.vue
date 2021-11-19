<template>
  <div id="pageDashboard">
    <v-container grid-list-xl fluid>
      <v-layout row wrap>
        <!-- mini statistic start -->
        <v-flex lg3 sm6 xs12>
          <mini-statistic
            icon="supervised_user_circle"
            :title="Dashboard.TotalTravelers"
            sub-title="Total travelers"
            color="secondary"
            class="mini-statistic"
          >
          </mini-statistic>
        </v-flex>
        <v-flex lg3 sm6 xs12>
          <mini-statistic
            icon="calendar_today"
            :title="Dashboard.ThisMonthsBookings"
            sub-title="Bookings this month"
            color="alternative1"
            class="mini-statistic"
          >
          </mini-statistic>
        </v-flex>
        <v-flex lg3 sm6 xs12>
          <mini-statistic
            icon="next_week"
            :title="Dashboard.UpcommingBookings"
            sub-title="Upcomming bookings"
            color="primary"
            class="mini-statistic"
          >
          </mini-statistic>
        </v-flex>
        <v-flex lg3 sm6 xs12>
          <mini-statistic
            icon="map"
            :title="Dashboard.ActiveTours"
            sub-title="Tours"
            color="alternative2"
            class="mini-statistic"
          >
          </mini-statistic>
        </v-flex>
        <!-- mini statistic  end -->
        <v-flex lg8 sm12 xs12>
          <v-widget
            style="box-shadow: 0 10px 35px -10px rgba(95, 158, 160, 0.6)"
            title="Bookings by month"
            content-bg="white"
          >
            <v-btn icon slot="widget-header-action">
              <v-icon class="text--secondary">refresh</v-icon>
            </v-btn>
            <div slot="widget-content">
              <line-chart
                :legend="false"
                :data="[
                  { name: 'Bookings by month', data: this.Dashboard.Data },
                ]"
              >
              </line-chart>
            </div>
          </v-widget>
        </v-flex>
        <v-flex lg4 sm12 xs12>
          <v-widget
            style="box-shadow: 0 10px 35px -10px rgba(95, 158, 160, 0.6)"
            title="Top Locations"
            content-bg="white"
          >
            <div slot="widget-content">
              <pie-chart :data="Dashboard.BookingData" :donut="true">
              </pie-chart>
            </div>
          </v-widget>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import MiniStatistic from "@/components/widgets/statistic/MiniStatistic";
import VWidget from "@/components/VWidget";
export default {
  components: {
    MiniStatistic,
    VWidget,
  },
  data: () => ({
    Dashboard: [],
  }),
  computed: {},
  methods: {
    getData() {
      this.$store.state.services.dashboardService.get().then((response) => {
        this.Dashboard = response.data;
      });
    },
  },
  created() {
    this.getData();
  },
};
</script>
<style>
.mini-statistic {
  border-radius: 10px;
  box-shadow: 0 10px 35px -10px rgba(95, 158, 160, 0.6);
  overflow: hidden;
  margin-bottom: 20px;
}
.mini-statistic .headline {
  color: white !important;
}
.mini-statistic .caption {
  text-transform: uppercase;
  font-size: 11px !important;
}
</style>
