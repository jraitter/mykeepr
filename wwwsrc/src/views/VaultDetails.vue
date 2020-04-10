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
        <div class="card-columns">
          <keeps :currentView="currentView" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Keeps from "../components/Keeps";
export default {
  name: "VaultDetails",
  mounted() {
    if (JSON.stringify(this.$store.state.activeVault.length) == 0) {
      let activeVaultData = JSON.parse(
        window.localStorage.getItem("activeVaultData")
      );
      if (activeVaultData == null) {
        console.error("error retrieving activeVaultData from local storage");
      }
      this.$store.dispatch("setActiveVault", activeVaultData);
      console.log("activeVault in store: ", this.$store.state.activeVault);
    }
    if (JSON.stringify(this.$store.state.VaultKeeps.length) == 0) {
      let currentVaultKeeps = JSON.parse(
        window.localStorage.getItem("currentVaultKeeps")
      );
      if (currentVaultKeeps == null) {
        console.error("error retrieving currentVaultKeeps from local storage");
      }
      this.$store.dispatch("setVaultKeeps", currentVaultKeeps);
      console.log("activeVault in store: ", this.$store.state.VaultKeeps);
    }
    // this.$store.dispatch("getKeepsByVaultId", this.$store.state.activeVault.id);
  },
  updated() {
    window.localStorage.setItem(
      "activeVaultData",
      JSON.stringify(this.$store.state.activeVault)
    );
    window.localStorage.setItem(
      "currentVaultKeeps",
      JSON.stringify(this.$store.state.VaultKeeps)
    );
  },
  data() {
    return {
      currentView: "Vault"
    };
  },
  computed: {
    activeVault() {
      return this.$store.state.activeVault;
    },
    vaultKeeps() {
      return this.$store.state.VaultKeeps;
    }
  },
  components: {
    Keeps
  }
};
</script>