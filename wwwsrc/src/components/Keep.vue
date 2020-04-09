<template>
  <div class="keep col">
    <div class="card m-4" style="width: 18rem;">
      <img :src="keepData.img" class="card-img-top" />
      <div class="card-body border border-dark mt-1 mx-1">
        <h6 class="card-title mb-0">{{ keepData.name }}</h6>
      </div>
      <div class="text-center d-flex justify-content-around">
        <p class="text-info mb-0">K:{{keepData.keeps}}</p>
        <p class="text-primary mb-0">S:{{keepData.shares}}</p>
        <p class="text-success mb-0">V:{{keepData.views}}</p>
      </div>
      <div class="text-center d-flex justify-content-around mt-2">
        <button
          v-if="this.$auth.isAuthenticated"
          class="btn btn-sm btn-info dropdown-toggle float-left"
          type="button"
          id="dropdownMenuButton"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false"
        >Keep</button>
        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
          <a
            @click="addKeepToVault(keepData.id, vault.id)"
            v-for="(vault) in vaults"
            :key="vault.id"
            class="dropdown-item"
          >{{vault.name}}</a>
        </div>
        <button class="btn btn-sm btn-primary" @click="sweet">Share</button>
        <button class="btn btn-sm btn-success" @click="setActive">View</button>
      </div>
      <div class="d-flex justify-content-around my-2">
        <button
          v-if="currentView == 'Vault'"
          class="btn btn-sm btn-info"
          @click="removeKeepfromVault(keepData.id, activeVault.id )"
        >Remove</button>
        <button v-if="keepData.isPrivate" class="btn btn-sm btn-danger" @click="deleteKeep">Delete</button>
        <button v-if="keepData.isPrivate" class="btn btn-sm btn-warning" @click="makePublic">Public</button>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
export default {
  name: "Keep",
  //NOTE child components 'catch' information in props
  props: ["keepData", "keepIndex", "currentView"],
  mounted() {},
  computed: {
    vaults() {
      return this.$store.state.Vaults;
    },
    activeVault() {
      return this.$store.state.activeVault;
    }
  },
  methods: {
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keepData.id);
    },
    setActive() {
      this.$store.dispatch("setActiveKeep", this.keepData);
    },
    makePublic() {
      this.$store.dispatch("makePublic", this.keepData.id);
    },
    addKeepToVault(keepId, vaultId) {
      let payload = {
        keepId: keepId,
        vaultId: vaultId
      };
      this.$store.dispatch("createVaultKeep", payload);
    },
    removeKeepfromVault(keepId, vaultId) {
      let payload = {
        keepId: keepId,
        vaultId: vaultId
      };
      this.$store.dispatch("removeKeepfromVault", payload);
    },
    sweet() {
      Swal.fire(
        "This KEEP was just shared to a random social media site of our choice.  Thanks for sharing."
      );
      this.$store.dispatch("updateShares", this.keepData.id);
    }
  }
};
</script>

<style scoped>
</style>
