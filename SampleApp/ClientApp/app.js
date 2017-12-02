import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'

var signalR = require('./signalr-client.min.js');

Vue.prototype.$http = axios;
Vue.prototype.$signalR = signalR;

sync(store, router);

const app = new Vue({
    store,
    router,
    ...App
});

export {
    app,
    router,
    store
}
