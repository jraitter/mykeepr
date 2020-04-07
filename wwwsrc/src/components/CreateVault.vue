<template>
  <div class="create-vault border border-dark my-4 py-2">
    <form @submit.prevent="createVault">
      <input type="text" name="name" placeholder="Name..." v-model="newVault.name" required />
      <input
        type="text"
        name="description"
        placeholder="description..."
        v-model="newVault.description"
      />
      <button type="submit" class="btn btn-success float-right">Submit</button>
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
