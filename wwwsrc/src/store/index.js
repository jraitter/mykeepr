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
    Vaults: [],
    VaultKeeps: [],
    KeepsByVaultId: []
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
    setVaults(state, payload) {
      state.Vaults = payload;
    },
    addKeep(state, payload) {
      state.Keeps.push(payload);
    },
    addVault(state, payload) {
      state.Vaults.push(payload);
    },
    addVaultKeep(state, payload) {
      state.VaultKeeps.push(payload);
    },
    removeKeep(state, id) {
      state.Keeps = state.Keeps.filter(k => k.id != id);
    },
    removeVault(state, id) {
      state.Vaults = state.Vaults.filter(v => v.id != id);
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
      }
    },
    async getMyKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps/mykeeps");
        commit("setKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getKeepById({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId);
        commit("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    setActiveKeep({ commit }, keep) {
      try {
        commit("setActiveKeep", keep);
        router.push({
          name: "KeepDetails",
          params: { keepId: keep.id }
        });
      } catch (error) {
        console.error(error);
      }
    },
    async updateViews({ commit, dispatch }, keepId) {
      try {
        let res = await api.put("keeps/" + keepId + "/viewcount");
        commit("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async updateShares({ commit, dispatch }, keepId) {
      try {
        //TODO needs to be added to backend
        let res = await api.put("keeps/" + keepId + "/sharecount");
        commit("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async updateKeeps({ commit, dispatch }, keepId) {
      try {
        let res = await api.put("keeps/" + keepId + "/keepcount");
        commit("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getVaults({ commit, dispatch }) {
      try {
        let res = await api.get("vaults");
        commit("setVaults", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep);
        commit("addKeep", res.data);
        //NOTE send to details page
        dispatch("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createVault({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("vaults", newKeep);
        commit("addVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createVaultKeep({ commit, dispatch }, newVaultKeep) {
      try {
        let res = await api.post("vaultkeeps", newVaultKeep);
        commit("addVaultKeep", res.data);
      } catch (error) {
        console.error(error);
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
    async deleteVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.delete(`vaults/${vaultId}`);
        commit("removeVault", vaultId);
      } catch (error) {
        console.error(error);
      }
    }

  }//endof actions
});//endof store
