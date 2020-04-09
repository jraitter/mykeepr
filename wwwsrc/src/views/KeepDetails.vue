<template>
  <div class="keep-details container-fluid">
    <div class="row">
      <div class="col-2"></div>

      <div class="col-8">
        <div class="card m-4">
          <img :src="details.img" class="card-img-top" />
          <div class="text-center d-flex justify-content-around mt-3">
            <p class="text-info">Keeps: {{details.keeps}}</p>
            <p class="text-primary">Shares: {{details.shares}}</p>
            <p class="text-success">Views: {{details.views}}</p>
          </div>
          <div class="card-body border border-dark mx-2 mb-2">
            <p class="card-title">
              <span>
                <b>NAME:</b>
              </span>
              {{ details.name }}
            </p>
            <p class="card-title">
              <span>
                <b>DESCRIPTION:</b>
              </span>
              {{ details.description }}
            </p>
          </div>
          <!-- <div class="text-center d-flex justify-content-around my-2">
            <button class="btn btn-sm btn-info" @click="nothing">Keep</button>
            <button class="btn btn-sm btn-primary" @click="nothing">Share</button>
            <button v-if="details.isPrivate" class="btn btn-sm btn-danger" @click="nothing">Delete</button>
          </div>-->
        </div>
      </div>

      <div class="col-2"></div>
    </div>
    <!-- <div class="row">
      <div class="col-12">
        <keep :keepData="details" />
      </div>
    </div>-->
  </div>
</template>

<script>
import Keep from "../components/Keep";
export default {
  name: "KeepDetails",
  mounted() {
    if (JSON.stringify(this.$store.state.Keeps.length) == 0) {
      let activeKeepData = JSON.parse(
        window.localStorage.getItem("activeKeepData")
      );
      if (activeKeepData == null) {
        console.error("error retrieving activeKeepData from local storage");
      }
      this.$store.dispatch("setActiveKeep", activeKeepData);
      console.log("activeKeep in store: ", this.$store.state.Keeps);
    } else {
      window.localStorage.setItem(
        "activeKeepData",
        JSON.stringify(this.$store.state.Keeps)
      );
    }
    this.$store.dispatch("updateViews", this.$store.state.Keeps.id);
  },
  computed: {
    details() {
      return this.$store.state.Keeps;
    }
  },
  methods: {
    nothing() {}
  },
  components: {
    Keep
  }
};
</script>

<style>
.cust-size {
  height: 50vh;
  width: auto;
}
</style>
