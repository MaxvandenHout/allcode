import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import VueRouter from 'vue-router'
import router from './router'
import firebase from 'firebase'
import VueTabs from 'vue-nav-tabs'
import 'vue-nav-tabs/themes/vue-tabs.css'


Vue.use(VueTabs)
Vue.use(VueRouter)
Vue.config.productionTip = false
var firebaseConfig = {
  apiKey: "AIzaSyCPyOEtgeeuVBPBP6MajlkLpDqQ48z-Klo",
  authDomain: "design-e941f.firebaseapp.com",
  databaseURL: "https://design-e941f.firebaseio.com",
  projectId: "design-e941f",
  storageBucket: "design-e941f.appspot.com",
  messagingSenderId: "896707205871",
  appId: "1:896707205871:web:09e38e496e99af064a191a",
  measurementId: "G-4F58NTY7EV"
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);
firebase.analytics();
new Vue({
  vuetify,
  router,
  render: h => h(App)
}).$mount('#app')
