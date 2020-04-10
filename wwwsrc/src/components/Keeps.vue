<template>
  <div class="keeps row">
    <!-- NOTE Props are data passed from parent to child component -->
    <div class="col-12 d-flex">
      <div v-if="currentView == 'Home'">
        <keep
          v-for="(keepObj, index) in keeps"
          :key="keepObj.id"
          :keepData="keepObj"
          :keepIndex="index"
          :currentView="currentView"
        />
      </div>
      <div v-if="currentView == 'Dash'">
        <keep
          v-for="(keepObj, index) in mykeeps"
          :key="keepObj.id"
          :keepData="keepObj"
          :keepIndex="index"
          :currentView="currentView"
        />
      </div>
      <div v-if="currentView == 'Vault'">
        <keep
          v-for="(keepObj, index) in vaultkeeps"
          :key="keepObj.id"
          :keepData="keepObj"
          :keepIndex="index"
          :currentView="currentView"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "./Keep";
export default {
  name: "Keeps",
  props: ["currentView"],
  //NOTE Computed's are watching the store for changes (observers) and are reactive to change
  //NOTE Computeds can be treated like getters (variable properties) and should always be used when getting data from the state
  computed: {
    //NOTE Computeds must always return a value
    keeps() {
      let data = this.$store.state.Keeps;
      return data;
    },
    mykeeps() {
      let data = this.$store.state.myKeeps;
      return data;
    },
    vaultkeeps() {
      let data = this.$store.state.VaultKeeps;
      return data;
    }
  },
  components: {
    Keep
  }
};
</script>

<style></style>
