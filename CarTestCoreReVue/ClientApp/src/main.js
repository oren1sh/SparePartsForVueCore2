import Vue from 'vue'
import './plugins/vuetify'
import './plugins/vuetify'
import App from './App.vue'
import './registerServiceWorker'
import axios from 'axios'
// import 'font-awesome/css/font-awesome.css'
//import 'mdi'

// import vuetifyloader from '../node_modules/vuetify-loader/dev/src/main'

Vue.use(axios)

Vue.config.productionTip = false

new Vue({
  render: h => h(App)
}).$mount('#app')
