import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);

// TYPES
const MAIN_SET_NETWORK = 'MAIN_SET_NETWORK'

// STATE
const state = {
    network: 'testnet'
}

// MUTATIONS
const mutations = {
    [MAIN_SET_NETWORK](state, obj) {
        state.network = obj.network
    }
}

// ACTIONS
const actions = ({
    setNetwork({ commit }, obj) {
        commit(MAIN_SET_NETWORK, obj)
    }
})

export default new Vuex.Store({
    state,
    mutations,
    actions
});
