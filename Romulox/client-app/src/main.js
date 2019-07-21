import Vue from 'vue';
import App from './App.vue';
import router from './router';
import Vuetify from 'vuetify';

Vue.use(Vuetify);
import 'vuetify/dist/vuetify.min.css'

Vue.config.productionTip = false;

router.beforeEach((to, from, next) => {
  document.title = to.meta.title;
  next();
});

new Vue({
  render: h => h(App),
  router
}).$mount('#app');
