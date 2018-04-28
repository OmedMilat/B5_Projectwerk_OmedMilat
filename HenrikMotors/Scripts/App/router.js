import Vue from 'vue'
import VueRouter from 'vue-router'
//import AppAdmin from './Admin/AppAdmin.vue'
//import EditPage from './Admin/EditPage.vue'

Vue.use(VueRouter)

export default new VueRouter({
    routes: [
        {
            path: '/overview', component: require('./Admin/AppAdmin.vue')
        },
        {
            path: '/editpage/:id', component: require('./Admin/EditPage.vue')
        }
    ]
})