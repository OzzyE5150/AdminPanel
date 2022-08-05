<template>
<div class="side-menu-container">
  <img id="imgLogo" alt="Admin Panel logo" src="../assets/images/logo.png">
  <h1>Management Portal</h1>
  <admin-button 
  id="menuInventory"
  :link="'/inventory'"
  >
    Inventory
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
