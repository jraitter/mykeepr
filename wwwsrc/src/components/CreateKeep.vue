<template>
  <div class="create-keep border border-dark my-4 py-2">
    <form @submit.prevent="createKeep">
      <input type="text" name="name" placeholder="Name..." v-model="newKeep.name" required />
      <input
        type="text"
        name="description"
        placeholder="description..."
        v-model="newKeep.description"
      />
      <input type="text" name="img" placeholder="imgUrl..." v-model="newKeep.img" required />
      <input type="radio" id="private" name="isPrivate" value="1" v-model="newKeep.isPrivate" />
      <label for="private">private</label>
      <input type="radio" id="public" name="isPrivate" value="0" v-model="newKeep.isPrivate" />
      <label for="public">public</label>
      <button type="submit" class="btn btn-success float-right">Submit</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "CreateKeep",
  data() {
    return {
      newKeep: {}
    };
  },
  methods: {
    createKeep() {
      this.newKeep.isPrivate = Boolean(Number(this.newKeep.isPrivate));
      this.$store.dispatch("createKeep", this.newKeep);
    }
  }
};
</script>

<style></style>
