import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import HomeView from "../views/HomeView.vue";
import Inventory from "@/views/Inventory.vue"
import { Vue } from "vue-class-component";
import Customers from "@/views/Customers.vue";
import Orders from "@/views/Orders.vue";
import CreateInvoice from "@/views/CreateInvoice.vue";


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
    },
    {
    path: '/customers',
    name: 'customers',
    component: Customers
    },
    {
      path: '/orders',
      name: 'orders',
      component: Orders
    },
    {
      path: '/invoice',
      name: 'invoice',
      component: CreateInvoice
    }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
