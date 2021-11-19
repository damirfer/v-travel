<template>
<v-app id="inspire">
  <app-admin-nav></app-admin-nav>
</v-app>
</template>

<script>
import AdminNavigation from "./AdminNavigation";
export default {
  components: {
    appAdminNav: AdminNavigation
  }
};
</script>
