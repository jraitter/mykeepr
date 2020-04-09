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
      <div class="col-12 text-right">
        <button
          v-if="!vaultForm"
          @click="vaultForm = true"
          class="btn btn-success mt-2"
        >Create-Vault</button>
        <button v-else @click="vaultForm = false" class="btn btn-danger mt-2">cancel</button>
      </div>
      <div class="col-12">
        <create-vault v-on:toggleVaultForm="vaultForm=$event" v-if="vaultForm" />
      </div>
    </div>

    <div class="row">
      <div class="col-12">
        <vaults />
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
import CreateKeep from "@/components/CreateKeep";
import CreateVault from "@/components/CreateVault";
import Vaults from "../components/Vaults";

export default {
  name: "Dashboard",
  mounted() {
    //NOTE mounted is fired when the component is 'mounted' to the page
    //NOTE '$' is a reference to the Root instance (main.js)
    this.$store.dispatch("getMyKeeps");
    this.$store.dispatch("getVaults");
  },
  data() {
    return {
      keepForm: false,
      vaultForm: false,
      currentView: "Dash"
    };
  },
  components: {
    Keeps,
    CreateKeep,
    CreateVault,
    Vaults
  }
};
</script>
