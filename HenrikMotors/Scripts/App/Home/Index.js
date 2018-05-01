import Vue from 'vue'
import { router } from '../router'
import { store }  from '../store'
import { mapActions } from 'vuex'
import { mapGetters } from 'vuex'
import App from '../Views/App.vue'
import AppAdmin from '../Admin/AppAdmin.vue'




new Vue({
    el: '#app',
    store,
    router,
    computed: {
    },
    components: {
        App,
        AppAdmin,
    },
    created() {
        this.$store.dispatch('fetchvoertuigen');
        this.$store.dispatch('fetchcarrosserietypes');
        this.$store.dispatch('fetchMerken');
        this.$store.dispatch('fetchUitrustingen');
    },
    methods:{
    }
});
new Vue({
    el: 'nav',
    router,
});