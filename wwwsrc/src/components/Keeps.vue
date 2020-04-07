<template>
  <div class="keeps row">
    <!-- NOTE Props are data passed from parent to child component -->
    <!-- <div class="col-12">maybe put a search here</div> -->
    <keep
      v-for="(keepObj, index) in keeps"
      :key="keepObj.id"
      :keepData="keepObj"
      :keepIndex="index"
    />
  </div>
</template>

<script>
import Keep from "./Keep";
export default {
  name: "Keeps",
  //NOTE Computed's are watching the store for changes (observers) and are reactive to change
  //NOTE Computeds can be treated like getters (variable properties) and should always be used when getting data from the state
  computed: {
    //NOTE Computeds must always return a value
    keeps() {
      let data = this.$store.state.Keeps;
      return data;
    }
  },
  components: {
    Keep
  }
};
</script>

<style></style>
