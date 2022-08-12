import { config } from "@vue/test-utils";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import global from "./store/global-store";
import filters from "./helpers/filters";
import VueApexCharts from "vue3-apexcharts";
import Vuex from "vue3-apexcharts"
import store from "./store";

const app = createApp(App);
//app.config.globalProperties.$apexcharts = VueApexCharts;

app.config.globalProperties.$filters = filters;

app.config.globalProperties.$store = global;

//app.component("apexchart", VueApexCharts)

app.provide("$store", store)
.use(store)
.use(Vuex)
.use(VueApexCharts)
.use(router)
.mount("#app");

