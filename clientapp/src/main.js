import Vue from 'vue'
import App from './App.vue'
import Vuelidate from 'vuelidate'

Vue.config.productionTip = false

Vue.use(Vuelidate)
window.axios = require('axios')

new Vue({
  render: h => h(App),
}).$mount('#app')
