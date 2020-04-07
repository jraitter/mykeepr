<template>
  <div class="create-vault border border-dark my-4 py-2">
    <form @submit.prevent="createVault">
      <div class="form-row">
        <div class="col-md-3">
          <input
            type="text"
            class="form-control ml-1"
            name="name"
            placeholder="Name..."
            v-model="newVault.name"
            required
          />
        </div>
        <div class="col-md-7">
          <input
            type="text"
            class="form-control"
            name="description"
            placeholder="description..."
            v-model="newVault.description"
          />
        </div>
        <div class="col-md-2">
          <button type="submit" class="form-control btn btn-success float-right mr-1">Submit</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "CreateVault",
  data() {
    return {
      newVault: {}
    };
  },
  methods: {
    createVault() {
      this.$store.dispatch("createVault", this.newVault);
      this.$emit("toggleVaultForm", false);
    }
  }
};
</script>

<style></style>
