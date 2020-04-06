<template>
  <div class="dashboard container-fluid">
    <div class="row">
      <div class="col-12 text-right">
        <button v-if="!keepForm" @click="keepForm = true" class="btn btn-success mt-2">Create-Keep</button>
        <button v-else @click="keepForm = false" class="btn btn-danger mt-2">cancel</button>
      </div>
      <div class="col-12">
        <create-keep v-if="keepForm" />
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <keeps />
      </div>
    </div>
  </div>
</template>

<script>
import Keeps from "../components/Keeps";
import CreateKeep from "@/components/CreateKeep";
// import CreateVault from "@/components/CreateVault";

export default {
  name: "Dashboard",
  mounted() {
    //NOTE mounted is fired when the component is 'mounted' to the page
    //NOTE '$' is a reference to the Root instance (main.js)
    this.$store.dispatch("setAuthUser", this.$auth.user);

    this.$store.dispatch("getMyKeeps");
  },
  data() {
    return {
      keepForm: false
    };
  },
  components: {
    Keeps,
    CreateKeep
    // CreateVault
  }
};
</script>
