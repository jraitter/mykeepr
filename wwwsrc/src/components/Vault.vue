<template>
  <div class="vault col-6">
    <ul>
      <li>
        <div class="d-flex">
          <button class="btn btn-sm btn-danger" @click="deleteVault">X</button>
          <h5 class="point ml-3" @click="getKeepsByVaultId">{{ vaultData.name }}</h5>
        </div>
      </li>
    </ul>
  </div>
</template>


<script>
export default {
  name: "vault",
  props: ["vaultData"],
  data() {
    return {};
  },
  computed: {},
  methods: {
    getKeepsByVaultId() {
      this.$store.dispatch("getKeepsByVaultId", this.vaultData.id);
      this.$store.dispatch("getByVaultId", this.vaultData.id);
    },
    deleteVault() {
      this.$store.dispatch("deleteVault", this.vaultData.id);
    }
  },
  components: {}
};
</script>


<style scoped>
.point {
  cursor: pointer;
}
</style>