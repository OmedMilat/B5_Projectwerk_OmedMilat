import Vue from 'vue'
import VueRouter from 'vue-router'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'
//import AppAdmin from './Admin/AppAdmin.vue'
//import EditPage from './Admin/EditPage.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/', component: require('./Views/App.vue')
    },
    {
        path: '/Voertuigen', component: require('./Views/Voertuigen.vue')
    },
    {
        path: '/VoertuigDetail/:id', component: require('./Views/VoertuigDetail.vue')
    },
    {
        path: '/Contact', component: require('./Views/Contact.vue')
    },
    {
        path: '/Login', component: require('./Views/Admin/Login.vue')
    },
    {
        path: '/Admin', component: require('./Views/Admin/AppAdmin.vue')
    },
    {
        path: '/Editpage/:id', component: require('./Views/Admin/EditPage.vue')
    }
]

export const router = new VueRouter({
    routes,
    mode: "history",
})



router.beforeEach((to, from, next) => {
    NProgress.start()
    next()
})
router.afterEach(() => {
    NProgress.done()
})