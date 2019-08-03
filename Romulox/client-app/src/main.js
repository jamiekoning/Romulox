import Vue from 'vue';
import App from './App.vue';
import router from './router';
import Vuetify from 'vuetify';


const opts = {
  theme: {
    dark: true,
  }
};

Vue.use(Vuetify);
import 'vuetify/dist/vuetify.min.css'

Vue.config.productionTip = false;

router.beforeEach((to, from, next) => {
  document.title = to.meta.title;
  next();
});

new Vue({
  vuetify: new Vuetify(opts),
  render: h => h(App),
  router
}).$mount('#app');
