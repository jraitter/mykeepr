<template>
  <div class="vaults row">
    <vault v-for="vault in vaults" :key="vault.id" :vaultData="vault" />
  </div>
</template>

<script>
import Vault from "./Vault";
export default {
  name: "Vaults",
  //NOTE Computed's are watching the store for changes (observers) and are reactive to change
  //NOTE Computeds can be treated like getters (variable properties) and should always be used when getting data from the state
  computed: {
    //NOTE Computeds must always return a value
    vaults() {
      let data = this.$store.state.Vaults;
      return data;
    }
  },
  components: {
    Vault
  }
};
</script>

<style></style>
