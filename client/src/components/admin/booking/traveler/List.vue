<template>
  <div>
    <v-toolbar flat>
      <v-toolbar-title>Travelers</v-toolbar-title>
      <v-divider class="mx-2" inset vertical></v-divider>
      <v-spacer></v-spacer>
      <v-dialog v-model="dialog" max-width="500px">
        <v-btn
          round
          :icon="true"
          :rounded="true"
          slot="activator"
          color="primary"
          @click="isUpdate = false"
          dark
          class="mb-2"
          ><v-icon>add</v-icon></v-btn
        >
        <v-card>
          <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title>

          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12 sm6 md4>
                  <v-text-field
                    v-model="editedItem.FirstName"
                    label="First name"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field
                    v-model="editedItem.LastName"
                    label="Last name"
                  ></v-text-field>
                </v-flex>
                <v-flex v-if="isUpdate" xs12 sm6 md4>
                  <v-text-field
                    v-model="editedItem.Password"
                    label="Access code"
                  ></v-text-field>
                </v-flex>
                <v-flex v-if="isUpdate" xs12 sm6 md4>
                  <v-btn
                    color="primary"
                    @click="editedItem.Password = generatePassword()"
                    >Generate access code</v-btn
                  >
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click.native="close"
              >Cancel</v-btn
            >
            <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-toolbar>
    <v-data-table
      :headers="headers"
      :items="travelers"
      hide-actions
      class="elevation-1"
    >
      <template slot="items" slot-scope="props">
        <td class="text-xs-left">{{ props.item.FirstName }}</td>
        <td class="text-xs-left">{{ props.item.LastName }}</td>
        <td class="text-xs-left">{{ props.item.Username }}</td>
        <td class="text-xs-left">{{ props.item.Password }}</td>
        <td class="text-xs-left">
          <v-icon
            small
            class="mr-2"
            @click="
              editItem(props.item);
              isUpdate = true;
            "
          >
            edit
          </v-icon>
          <v-icon small @click="deleteItem(props.item)"> delete </v-icon>
        </td>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
export default {
  props: ["bookingId"],
  data: () => ({
    dialog: false,
    headers: [
      {
        text: "First name",
        align: "left",
        sortable: false,
        value: "FirstName",
      },
      { text: "Last name", value: "LastName" },
      { text: "Username", value: "Username" },
      { text: "Access code", value: "Password" },
      { text: "Actions", value: "name", sortable: false },
    ],
    travelers: [],
    isUpdate: false,
    editedIndex: -1,
    editedItem: {
      FirstName: "",
      LastName: "",
      Username: "",
      Password: "",
      TravelerId: 0,
      BookingTraveler: [],
    },
    defaultItem: {
      FirstName: "",
      LastName: "",
      Username: "",
      Password: "",
      BookingTraveler: [],
    },
    deletedItem: {
      TravelerId: 0,
      BookingId: 0,
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Traveler" : "Edit Traveler";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  created() {
    this.initialize();
  },

  methods: {
    ...mapMutations({
      loader: "COMMIT_LOADER",
    }),
    initialize() {
      this.loader(true);
      this.$store.state.services.travelerService
        .getByBooking(this.$route.params.bookingId)
        .then((response) => {
          this.travelers = response.data;
        })
        .finally(() => {
          this.loader(false);
        });
    },
    generatePassword() {
      return Math.random().toString(36).substr(2, 8);
    },
    editItem(item) {
      this.editedIndex = this.travelers.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.travelers.indexOf(item);
      this.deletedItem.BookingId = this.$route.params.bookingId;
      this.deletedItem.TravelerId = item.TravelerId;
      if (confirm("Are you sure you want to delete this item?")) {
        this.travelers.splice(index, 1);
        this.$store.state.services.travelerService.remove(this.deletedItem);
      }
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    save() {
      this.loader(true);
      if (this.editedIndex > -1) {
        this.editedItem.Username =
          this.editedItem.FirstName.toLowerCase() +
          "." +
          this.editedItem.LastName.toLowerCase();
        Object.assign(this.travelers[this.editedIndex], this.editedItem);
        this.$store.state.services.travelerService
          .update(this.editedItem)
          .finally(() => {
            this.loader(false);
          });
      } else {
        this.editedItem.Password = this.generatePassword();
        this.editedItem.Username =
          this.editedItem.FirstName.toLowerCase() +
          "." +
          this.editedItem.LastName.toLowerCase();
        this.travelers.push(this.editedItem);
        this.editedItem.BookingTraveler.push({
          BookingId: this.$route.params.bookingId,
        });
        this.$store.state.services.travelerService
          .add(this.editedItem)
          .finally(() => {
            this.loader(false);
          });
      }
      this.close();
    },
  },
};
</script>

<style scoped>
.language-header {
  border-top: 2px solid #116549;
  background-color: white;
  border-bottom: 1px solid #116549;
  margin-bottom: 5px;
}
.modal-header {
  background-color: white;
  border-bottom: 2px solid #116549;
  margin-bottom: 5px;
}
</style>
