<template>
  <div class="keep col">
    <div class="card m-4" style="width: 18rem;">
      <img :src="keepData.img" class="card-img-top" />
      <div class="card-body border border-dark mt-1">
        <h5 class="card-title">{{ keepData.name }}</h5>
      </div>
      <div class="text-center d-flex justify-content-around mt-2">
        <button class="btn btn-sm btn-info" @click="nothing">Keep</button>
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
export default {
  name: "Keep",
  //NOTE child components 'catch' information in props
  props: ["keepData", "keepIndex"],
  methods: {
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keepData.id);
    },
    setActive() {
      this.keepData.views++;
      this.$store.dispatch("setActiveKeep", this.keepData);
    },
    nothing() {}
  }
};
</script>

<style scoped>
</style>
