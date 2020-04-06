<template>
  <div class="keep-details container-fluid">
    <div class="row">
      <div class="col-2"></div>
      <div class="col-8">
        <div v-if="details.id">
          <div class="card mt-4">
            <img class="card-img-top" :src="details.img" alt="Card image cap" />
            <div class="card-body">
              <h5 class="card-title">{{details.name}}</h5>
              <p class="card-text">{{details.description}}</p>
            </div>
          </div>
        </div>
      </div>
      <div class="col-2"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "KeepDetails",
  mounted() {
    // NOTE if we have no cars in state
    if (!this.$store.state.Keeps.length) {
      // NOTE Go get the keep by its id
      this.$store.dispatch("getKeepById", this.$route.params.keepId);
    } else {
      // NOTE otherwise, set the car as the active car, based on its ID
      this.$store.dispatch(
        "setActiveKeep",
        this.$store.state.Keeps.find(k => k.id == this.$route.params.keepId)
      );
    }
  },
  computed: {
    details() {
      return this.$store.state.activeKeep;
    }
  },
  methods: {}
};
</script>

<style>
.cust-size {
  height: 50vh;
  width: auto;
}
</style>
