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
  mounted() {},
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
