<template>
  <div class="home container-fluid">
    <div class="row text-center">
      <div class="col">
        <h1>Welcome to the KeepR home page</h1>
        <h3>Browse and add any of these keeps to one of your Vaults</h3>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <keeps :currentView="currentView" />
      </div>
    </div>
  </div>
</template>

<script>
import Keeps from "../components/Keeps";
export default {
  name: "Home",
  mounted() {
    //NOTE mounted is fired when the component is 'mounted' to the page
    //NOTE '$' is a reference to the Root instance (main.js)
    this.$store.dispatch("getKeeps");
  },
  data() {
    return {
      keepForm: false,
      currentView: "Home"
    };
  },
  components: {
    Keeps
  }
};
</script>
