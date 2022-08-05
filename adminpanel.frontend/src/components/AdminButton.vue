<template>
  <div class="btn-link">
      <button @click="visitRoute">
          <slot></slot>
      </button>
  </div>
</template>

<script lang="ts">
    // import Vue from 'vue';
    import {Options, Vue} from 'vue-class-component';
    import {Prop} from 'vue-property-decorator';

    @Options({
        components: {}
    })
export default class AdminButton extends Vue {
    @Prop({required: false, type: String})
    link?: string;

    visitRoute() {
        this.$router.push(this.link!);
    }
}
</script>

<style scoped>

</style>