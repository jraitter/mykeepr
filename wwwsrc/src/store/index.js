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
    activeVault: [],
    Vaults: [],
    VaultKeeps: []
  },
  mutations: {
    setAuthUser(state, authUser) {
      state.user = authUser;
    },
    setKeeps(state, payload) {
      state.Keeps = payload;
    },
    setActiveKeep(state, payload) {
      state.Keeps = payload;
    },
    setActiveVault(state, payload) {
      state.activeVault = payload;
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
    setUpdatedKeep(state, payload) {
      state.Keeps = state.Keeps.map(x => (x.id === payload.id) ? payload : x);
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
        commit("setKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getKeepsByVaultId({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps");
        commit("setKeeps", res.data);
        router.push({
          name: "VaultDetails"
        });
      } catch (error) {
        console.error(error);
      }
    },
    async updateViews({ commit, dispatch }, keepId) {
      try {
        let res = await api.put("keeps/" + keepId + "/viewcount");
        commit("setKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async updateShares({ commit, dispatch }, keepId) {
      try {
        let res = await api.put("keeps/" + keepId + "/sharecount");
        commit("setUpdatedKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async updateKeeps({ commit, dispatch }, keepId) {
      try {
        let res = await api.put("keeps/" + keepId + "/keepcount");
        commit("setUpdatedKeep", res.data);
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
    async getByVaultId({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId);
        commit("setActiveVault", res.data);
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
        dispatch("updateKeeps", newVaultKeep.keepId);

      } catch (error) {
        console.error(error);
      }
    },
    async makePublic({ commit, dispatch }, keepId) {
      try {
        let getres = await api.get("keeps/" + keepId);
        getres.data.isPrivate = false;
        let putres = await api.put("keeps/" + keepId, getres.data);
        commit("setUpdatedKeep", putres.data);
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
    },
    async removeKeepfromVault({ commit, dispatch }, payload) {
      try {
        let res = await api.put("vaultkeeps/vaultkeep", payload);
        commit("removeKeep", res.data.keepId);
      } catch (error) {
        console.error(error);
      }
    }

  }//endof actions
});//endof store
