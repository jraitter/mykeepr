<template>
  <div id="app">
    <navbar />
    <router-view />
  </div>
</template>

<script>
import Navbar from "@/components/navbar";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "App",
  async beforeCreate() {
    this.$store.dispatch("getKeeps");
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
    if (this.$auth.isAuthenticated) {
      this.$store.dispatch("setAuthUser", this.$auth.user);
      this.$store.dispatch("getMyKeeps");
      this.$store.dispatch("getVaults");
    } else {
      console.log("auth.isAuthenticaten if false at this point in App.vue");
    }
  },
  components: {
    Navbar
  }
};
</script>

<style lang="scss">
@import "./assets/_variables.scss";
@import "bootstrap";
@import "./assets/_overrides.scss";
</style>
