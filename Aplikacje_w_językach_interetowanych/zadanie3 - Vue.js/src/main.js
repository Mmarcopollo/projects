import '@babel/polyfill'
import 'mutationobserver-shim'
import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'


Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')

Vue.filter('delete-square-brackets-from-array', function(value){
	var i;
	var text =""
	for (i = 0; i < value.length; i++) {
		if(i == value.length - 1) {
			text += value[i];
		}
		else {
			text += value[i] + ", ";
		}
	} 
	return text;
});
