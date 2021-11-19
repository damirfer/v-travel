<template>
  <div>
    <v-menu open-on-click bottom offset-y>
      <img
        slot="activator"
        @click="dateModal = true"
        class="action-icon-lg"
        :src="mainIcon"
        :class="{ 'border-radius': isBorderRadius }"
        alt="Profile image"
      />
      <!-- <img
        slot="activator"
          @click="dateModal = true"
          class="action-icon-lg"
          src="../assets/icons/filter.png"
      alt="Filter"/>-->
      <v-list>
        <v-list-tile
          v-for="(item, index) in items"
          :key="index"
          @click="itemSelected(item.value)"
        >
          <v-list-tile-title>
            <v-icon>{{ item.icon }}</v-icon>
            <span>{{ item.text }}</span>
          </v-list-tile-title>
        </v-list-tile>
      </v-list>
    </v-menu>
  </div>
</template>

<script>
export default {
  props: ["items", "main-icon"],
  props: {
    items: {
      default: null,
    },
    mainIcon: {
      default: "",
    },
    isBorderRadius: {
      default: false,
    },
  },
  methods: {
    itemSelected(value) {
      this.$emit("itemSelected", value);
    },
  },
};
</script>

<style scoped>
.icon {
  font-size: 42px;
}
img {
  margin-top: -10px;
}
.border-radius {
  border-radius: 50%;
}
</style>
