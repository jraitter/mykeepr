<template>
  <div class="keep col">
    <div class="card m-4" style="width: 18rem;">
      <router-link :to="{ name: 'KeepDetails', params: { keepId: this.keepData.id } }">
        <img :src="keepData.img" class="card-img-top" />
      </router-link>
      <div class="card-body border border-dark mt-1">
        <h5 class="card-title">{{ keepData.name }}</h5>
      </div>
      <div class="text-center d-flex justify-content-around mt-2">
        <button class="btn btn-sm btn-info" @click="deleteKeep">Keep</button>
        <button class="btn btn-sm btn-primary" @click="deleteKeep">Share</button>
        <button class="btn btn-sm btn-success" @click="deleteKeep">View</button>
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
      this.$store.dispatch("setActiveKeep", this.keepData);
    }
  }
};
</script>

<style scoped>
.car {
  cursor: pointer;
}
</style>
