<template>
  <div class="keep col">
    <div class="card m-4" style="width: 18rem;">
      <img :src="keepData.img" class="card-img-top" />
      <div class="card-body border border-dark mt-1">
        <h5 class="card-title">{{ keepData.name }}</h5>
      </div>
      <div class="text-center d-flex justify-content-around mt-2">
        <button
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

        <button class="btn btn-sm btn-primary" @click="nothing">Share</button>
        <button class="btn btn-sm btn-success" @click="setActive">View</button>
        <button v-if="keepData.isPrivate" class="btn btn-sm btn-danger" @click="deleteKeep">Delete</button>
      </div>
      <div class="text-center d-flex justify-content-around">
        <p class="text-info">K:{{keepData.keeps}}</p>
        <p class="text-primary">S:{{keepData.shares}}</p>
        <p class="text-success">V:{{keepData.views}}</p>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
export default {
  name: "Keep",
  //NOTE child components 'catch' information in props
  props: ["keepData", "keepIndex"],
  computed: {
    vaults() {
      return this.$store.state.Vaults;
    }
  },
  methods: {
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keepData.id);
    },
    setActive() {
      this.$store.dispatch("setActiveKeep", this.keepData);
    },
    addKeepToVault(keepId, vaultId) {
      let payload = {
        keepId: keepId,
        vaultId: vaultId
      };
      this.$store.dispatch("createVaultKeep", payload);
    },
    nothing() {
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
