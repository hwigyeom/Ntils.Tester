import Vue from 'vue';
import App from './components/App.vue';
import store from './vuex/store';

import './scss/site.scss';

const app = new Vue({
  store,
  ...App
});

app.$mount('#app');