import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    user: {},
    Keeps: [],
    activeKeep: {},
    myVaults: []
  },
  mutations: {
    setAuthUser(state, authUser) {
      state.user = authUser;
    },
    setKeeps(state, payload) {
      state.Keeps = payload;
    },
    setActiveKeep(state, payload) {
      state.activeKeep = payload;
    },
    setMyVaults(state, payload) {
      state.myVaults = payload;
    },
    addKeep(state, payload) {
      state.Keeps.push(payload);
    },
    removeKeep(state, id) {
      state.Keeps = state.Keeps.filter(k => k.id != id);
    }
  },//endof mutations
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    setAuthUser({ commit }, authUser) {
      commit("setAuthUser", authUser)
    },
    async getKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps");
        commit("setKeeps", res.data);
      }
      catch (error) {
        console.error(error);
        // NOTE on error push route to home
        router.push({ name: "Home" });
      }
    },
    async getMyKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps/mykeeps");
        commit("setKeeps", res.data);
      } catch (error) {
        console.error(error);
        // NOTE on error push route to home
        router.push({ name: "Home" });
      }
    },
    async getKeepById({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId);
        commit("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
        // NOTE on error push route to home
        router.push({ name: "Home" });
      }
    },
    async setActiveKeep({ commit }, keep) {
      try {
        if (keep.userId == this.state.user.sub) {
          let res = await api.put("keeps/" + keep.id, keep);
        }
        commit("setActiveKeep", keep);
        router.push({
          name: "KeepDetails",
          params: { keepId: keep.id }
        });
      } catch (error) {
        console.error(error);
        // NOTE on error push route to home
        router.push({ name: "Home" });
      }
    },
    async getMyVaults({ commit, dispatch }) {
      try {
        let res = await api.get("keeps/myvaults");
        commit("setMyVaults", res.data);
      } catch (error) {
        console.error(error);
        // NOTE on error push route to home
        router.push({ name: "Home" });
      }
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep);
        commit("addKeep", res.data);
        // NOTE after the keep is created, send them to the keep details page for that keep
        router.push({
          name: "KeepDetails",
          params: { keepId: res.data.id }
        });
      } catch (error) {
        console.error(error);
        // NOTE on error push route to home
        router.push({ name: "Home" });
      }
    },
    async deleteKeep({ commit, dispatch }, keepId) {
      try {
        let res = await api.delete(`keeps/${keepId}`);
        commit("removeKeep", keepId);
      } catch (error) {
        console.error(error);
      }
    },

  }//endof actions
});//endof store
