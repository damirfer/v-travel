<template>
  <div class="title-bar">
    <!-- <v-toolbar-side-icon class="toolbar-side-icon" @click.stop="$emit('drawerToggle')"></v-toolbar-side-icon> -->
    <span v-if="!routeParentTitle.meta.isChild" class="title-bar-title">{{
      routeParentTitle.meta.title
    }}</span>
    <router-link
      class="title-bar-title title-bar-title-link"
      v-if="routeParentTitle.meta.isChild"
      :to="routeParentTitle.meta.parentRoute"
      >{{ routeParentTitle.meta.parentName }}</router-link
    >

    <span v-if="routeTitle" class="title-bar-title"> / {{ routeTitle }}</span>

    <div
      style="
        position: absolute;
        right: 15px;
        display: flex;
        align-items: center;
      "
    >
      <span class="header-user-name">{{ userName }}</span>
      <hover-drop-down
        class="layout-change"
        :items="navigationSelector"
        :main-icon="userProfile || ''"
        :isBorderRadius="true"
        @itemSelected="navigationItemChanged"
      ></hover-drop-down>
    </div>
  </div>
</template>
<script>
import HoverDropDown from "../HoverDropDown";

export default {
  components: {
    HoverDropDown,
  },
  data() {
    return {
      userName: "",
      userProfile: "",
      drawer: false,
      navigationSelector: [
        {
          text: "Logout",
          value: "logout",
        },
      ],
    };
  },
  methods: {
    logout() {
      // localStorage.setItem("user-roles", "");
      localStorage.setItem("user-token", "");
      this.$store.state.services.resetAxiosHeader();
      this.$router.push("/");
    },
    navigationItemChanged(item) {
      if (item === "logout") {
        this.logout();
      }
    },
  },
  computed: {
    routeTitle() {
      if (this.$route.meta.isChild) {
        return this.$route.meta.title;
      }
    },
    routeParentTitle() {
      return this.$route;
    },
  },
  created() {
    this.userName = localStorage.getItem("user-name");
    this.userProfile = localStorage.getItem("user-profile");
  },
};
</script>
<style scoped>
.title-bar {
  font-family: "Montserrat", sans-serif;
  position: fixed;
  display: flex;
  align-items: center;
  width: calc(100% - 300px);
  top: 0;
  z-index: 199;
  text-transform: uppercase;
  color: #0284a8;
  padding: 15px 10px;
  padding-left: 1.5rem;
  background-color: white;
  border-bottom: 1px solid rgba(0, 0, 0, 0.15);
  /* border-bottom: 1px solid #0284a8; */
}
.toolbar-side-icon {
  color: #0284a8;
  position: fixed;
  left: 2px;
  top: 4px;
}
.title-bar-title {
  font-size: 19px;
}
.title-bar-title-link {
  text-decoration: none;
  margin-right: 10px;
}
.title-bar-title-link:hover {
  text-decoration: underline;
}
.profile-img-circle {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-image: url("../../assets/profile.png");
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  position: absolute;
  right: 25px;
  top: 50%;
  transform: translateY(-50%);
  cursor: pointer;
  border: 2px solid #fbfbfb;
  margin-right: 1.75rem;
}
.header-profile {
  width: 30px;
  height: 30px;
  border-radius: 50%;
}
.header-user-name {
  line-height: 30px;
  margin-right: 10px;
  vertical-align: top;
}
@media (max-width: 1263px) {
  .title-bar {
    width: 100%;
  }
}
</style>