import App from './App.vue'
import Vue from 'vue'
import VueRouter from 'vue-router'
import Customers from './components/Customers.vue'
import Register from './components/Register.vue'
import Login from './components/Login.vue'
import Assets from './components/Assets.vue'
import UserProfile from './components/UserProfile.vue'
import Tickets from './components/Tickets.vue'
import Vuex from 'vuex'
import * as axios from "axios";
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { fas } from '@fortawesome/free-solid-svg-icons'

library.add(fas)

Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.use(Vuex);
Vue.use(VueRouter);

const router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: "/",
      redirect: {
        name: "login"
      }
    },
    {
      path: '/customers',
      name: 'customers',
      component: Customers
    },
    {
      path: '/user/register',
      name: 'register',
      component: Register
    },
    {
      path: '/user/login',
      name: 'login',
      component: Login
    },
    {
      path: '/assets/:id',
      name: 'assets',
      props: true,
      component: Assets
    },
    {
      path: '/user/profile',
      name: 'userProfile',
      component: UserProfile
    },
    {
      path: '/tickets',
      name: 'tickets',
      component: Tickets
    },
  ]
});

const store = new Vuex.Store(
  {
    state: {
      authenticated: false
    },
    mutations: {
      setAuthentication(state, status) {
        state.authenticated = status;
      }
    }
  }
)

router.beforeEach((to, from, next) => {
  if (to.name !== "login" && to.name !== "register" && !store.state.authenticated) {
    axios
      .get(`http://localhost:8030/user/profile`, {
        withCredentials: true,
      })
      .then(() => {
        store.commit("setAuthentication", true);
        next();
      })
      .catch(() => {
        next({ name: 'login' });
      });
  } else {
    next();
  }
})

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
  store: store,
  router: router,
}).$mount('#app')
