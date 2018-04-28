import Vue from 'vue'
import router from '../router'
import Card from '../Components/Card.vue'
import AppAdmin from '../Admin/AppAdmin.vue'
import { store }  from '../store'
import { mapActions } from 'vuex'
import { mapGetters } from 'vuex'

Vue.component("Card", Card);


new Vue({
    el: '#app',
    store,
    router,
    computed: {
        ...mapGetters(['voertuigen'])
    },
    components: {
        Card,
        AppAdmin,
    },
    created() {
        this.$store.dispatch('fetchvoertuigen');
        //this.$store.dispatch('fetchVoertuigDetails', 2);
        this.$store.dispatch('fetchcarrosserietypes');
        this.$store.dispatch('fetchMerken');
        this.$store.dispatch('fetchUitrustingen');
    },
    methods:{
    }
});