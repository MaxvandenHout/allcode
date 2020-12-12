import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import firebase from 'firebase'
import axios from 'axios'
// Required for side-effects
require("firebase/firestore");

var firebaseConfig = {
  apiKey: "AIzaSyARJXB75g9s-32IrWNoecih4ywyVPMmR44",
  authDomain: "chatter-35056.firebaseapp.com",
  databaseURL: "https://chatter-35056.firebaseio.com",
  projectId: "chatter-35056",
  storageBucket: "chatter-35056.appspot.com",
  messagingSenderId: "1039807056345",
  appId: "1:1039807056345:web:23c0f271e864dbd47671c8",
  measurementId: "G-C8LSK8F6NP"
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);
firebase.analytics();
var db = firebase.firestore();
Window.db=db;
Vue.prototype.$axios = axios;
Vue.config.productionTip = false
let app;
export default db
new Vue({
      router,
      store,
      render: h => h(App)
    }).$mount('#app')


