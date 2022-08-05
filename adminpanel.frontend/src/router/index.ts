import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import HomeView from "../views/HomeView.vue";
import Inventory from "@/views/Inventory.vue"
import { Vue } from "vue-class-component";


const routes: Array<RouteRecordRaw> = [
    {
      path: '/',
      name: 'home',
      component: Inventory
    },
    {
      path: '/inventory',
      name: 'inventory',
      component: Inventory
    }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
