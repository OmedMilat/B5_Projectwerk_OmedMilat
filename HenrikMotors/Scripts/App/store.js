import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

var apiURL = '/api/'

const state = {
    test: "testo",
    voertuigen: null,
    currentVoertuig: null,
    carrosserietypes: null,
    merken: null,
    uitrustingen: null,
    voertuigUitrusting: [],
}

const getters = {
    test: (state) => {
        return state.test
    },
    voertuigen: (state) => {
        return state.voertuigen
    },
    currentVoertuig: (state) => {
        return state.currentVoertuig
    },
    carrosserietypes: (state) => {
        return state.carrosserietypes
    },
    merken: (state) => {
        return state.merken
    },
    uitrustingen: (state) => {
        return state.uitrustingen
    },
    voertuigUitrustingStore: (state) => {
        return state.voertuigUitrusting
    },
}

const actions = {
    fetchvoertuigen(context) {
        context.commit('fetchvoertuigen');
    },
    fetchVoertuigDetails(context, id) {
        return new Promise((resolve, reject) => {
            //context.commit('fetchVoertuigDetails', id);
            console.log(id);
            fetch(`${apiURL}voertuigen/${id}`)
                .then(res => res.json())
                .then(function (res) {
                    var date = new Date(res.Bouwjaar);
                    var month = ("0" + (date.getMonth() + 1)).slice(-2);
                    res.Bouwjaar = `${date.getFullYear()}-${month}`;
                    res.Prijs = res.Prijs.toLocaleString("de-DE");
                    res.Kilometerstand = res.Kilometerstand.toLocaleString("de-DE");
                    state.currentVoertuig = res;
                    state.voertuigUitrusting = [];
                    resolve();
                    if (state.currentVoertuig.VoertuigUitrusting != 0 && state.currentVoertuig.VoertuigUitrusting != null) {
                        for (var i = 0; i < state.currentVoertuig.VoertuigUitrusting.length; i++) {
                            state.voertuigUitrusting[i] = state.currentVoertuig.VoertuigUitrusting[i].UitrustingId;
                        }
                    }
                }).catch(err => console.error('Fout: ' + err));    


        })    
    },
    fetchcarrosserietypes(context) {
        context.commit('fetchcarrosserietypes');
    },
    fetchMerken(context) {
        context.commit('fetchMerken');
    },
    fetchUitrustingen(context) {
        context.commit('fetchUitrustingen');
    },
    newVoertuig(context) {
        context.commit('newVoertuig');
    },
}
const mutations = {
    fetchvoertuigen(state) {
        fetch(`${apiURL}voertuigen`)
            .then(res => res.json())
            .then(function (res) {
                for (var i = 0; i < res.length; i++) {
                    res[i].Prijs = res[i].Prijs.toLocaleString("de-DE");
                    res[i].Kilometerstand = res[i].Kilometerstand.toLocaleString("de-DE");
                    var d = new Date(res[i].Bouwjaar);
                    if (res[i].Bouwjaar)
                        res[i].Bouwjaar = ("0" + (d.getMonth() + 1)).slice(-2) + "-" + d.getFullYear();
                }
                state.voertuigen = res;
            }).catch(err => console.error('Fout: ' + err));
    },
    fetchVoertuigDetails(state, id) { 
        console.log(id);
        fetch(`${apiURL}voertuigen/${id}`)
            .then(res => res.json())
            .then(function (res) {
                var date = new Date(res.Bouwjaar);
                var month = ("0" + (date.getMonth() + 1)).slice(-2);
                res.Bouwjaar = `${date.getFullYear()}-${month}`;
                state.currentVoertuig = res;
                state.voertuigUitrusting = [];
                if (state.currentVoertuig.VoertuigUitrusting != 0 && state.currentVoertuig.VoertuigUitrusting != null) {
                    for (var i = 0; i < state.currentVoertuig.VoertuigUitrusting.length; i++) {
                        state.voertuigUitrusting[i] = state.currentVoertuig.VoertuigUitrusting[i].UitrustingId;
                    }
                }
            }).catch(err => console.error('Fout: ' + err));    
    },
    fetchcarrosserietypes(state) {
        fetch(`${apiURL}carrosserietypes`)
            .then(res => res.json())
            .then(function (res) {
                state.carrosserietypes = res;
            }).catch(err => console.error('Fout: ' + err));
    },
    fetchMerken(state) {
        console.log("merken");
        fetch(`${apiURL}merken`)
            .then(res => res.json())
            .then(function (res) {
                state.merken = res;
            }).catch(err => console.error('Fout: ' + err));
    },
    fetchUitrustingen(state) {
        fetch(`${apiURL}uitrustingen`)
            .then(res => res.json())
            .then(function (res) {
                state.uitrustingen = res;
            }).catch(err => console.error('Fout: ' + err));
    },
    newVoertuig(state) {
        state.currentVoertuig = {
            Id: 1, MerkId: 1, CarrosserietypeId: 1,
        }
    },
}




export const store = new Vuex.Store({
    state,
    getters,
    actions,
    mutations,
});

 