<template>
  <div id="app">
    <div class="app-menu">
      <side-menu />
    </div>
    <div class="app-content">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
// import { Vue } from "vue-property-decorator";
import { Options, Vue } from "vue-class-component";
import SideMenu from "@/components/SideMenu.vue";

@Options({
  name: "App",
  components: { SideMenu },
})
export default class App extends Vue { 

}
</script>


<style lang="scss">
@import "@/scss/global.scss";
@import url('http://fonts.cdnfonts.com/css/esthetique');
#app {
  //font-family: Avenir, Helvetica, Arial, sans-serif;
  // font-family: 'esthetique', sans-serif;
  font-family: -apple-system, BlinkMacSystemFont, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #818181;
  display: flex;

  .app-menu {
    position: fixed;
  }

  .app-content {
    padding: 1.2rem;
    width: 90%;
    margin: 1rem auto 1rem $menu-width;
    text-align: left;
  }
}

* {
  margin: 0;
}

a {
  text-decoration: none;
}

hr {
  border: 1px solid #000;
  margin: 1rem 0;
}
body{
  background-color: #212124;
}
</style>
