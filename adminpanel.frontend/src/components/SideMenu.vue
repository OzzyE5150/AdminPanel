<template>
<div class="side-menu-container">
  <router-link to="/">
  <img 
    id="imgLogo" 
    alt="Admin Panel logo" 
    src="../assets/images/logo.png"/>
  </router-link>
  <h1>Management Portal</h1>
  <admin-button 
  id="menuInventory"
  :link="'/inventory'"
  is-full-width
  >
    Inventory
  </admin-button>
  <admin-button 
  id="menuCustomers"
  :link="'/customers'"
  is-full-width
  >
   Manage Customers
  </admin-button>
<admin-button 
  id="menuInvoice"
  :link="'/invoice'"
  is-full-width
  >
    New Invoice
  </admin-button>
  <admin-button 
  id="menuOrders"
  :link="'/orders'"
  is-full-width
  >
    Orders
  </admin-button>

</div>
</template>

<script lang="ts">
// import { Vue } from "vue-property-decorator";
import { Options, Vue } from "vue-class-component";
import AdminButton from "@/components/AdminButton.vue"

@Options({
    components: {
    AdminButton,
  },
})
export default class SideMenu extends Vue {}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.side-menu-container {
  background-color: #fcfcfc;
  height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 0.8rem;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 0 1px 1px rgba(0, 0, 0, 0.24);
  box-sizing: border-box;
}

#imgLogo {
  width: 100%;
}
h1{
  font-size: 1.2rem;
  margin: 1rem 0;
}
</style>
