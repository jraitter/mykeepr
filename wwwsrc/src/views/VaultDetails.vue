<template>
  <div class="vault-details container-fluid">
    <div class="row text-center">
      <div class="col">
        <h1>Vault Name: {{activeVault.name}}</h1>
        <h3>Description: {{activeVault.description}}</h3>
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
export default {
  name: "VaultDetails",
  mounted() {},
  computed: {
    activeVault() {
      return this.$store.state.activeVault;
    }
  },
  components: {
    Keeps
  }
};
</script>