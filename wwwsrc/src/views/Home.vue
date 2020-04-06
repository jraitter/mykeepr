<template>
  <div class="home container-fluid">
    <div class="row">
      <div class="col-12">
        <keeps />
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
      keepForm: false
    };
  },
  components: {
    Keeps
  }
};
</script>
