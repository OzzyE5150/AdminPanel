<template>
  <div class="btn-link">
      <button 
                        v-if="visitRoute"
                        @click="visitRoute" 
                        :class="['admin-button', {'full-width' : isFullWidth}]"
                        type="button"
                        >
          <slot></slot>
      </button>

      <button
        v-else
        @click="onClick"
        :class="['solar-button', {'full-width': isFullWidth}]"
        type="button">
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

    @Prop({required: false, type: Boolean, default: false})
    isFullWidth?: boolean;

    onClick(){
        this.$emit('button:click');
    }

    visitRoute() {
        this.$router.push(this.link!);
    }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
@import url('http://fonts.cdnfonts.com/css/esthetique');

    .admin-button{
        font-family: 'esthetique', sans-serif;
        background: lighten($admin-blue, 10%);
        color: #f8f7f9;
        padding: 0.8rem;
        transition: background-color 0.5s;
        margin: 0.3rem 0.2rem;
        display: inline-block;
        cursor: pointer;
        font-size: 1rem;
        min-width: 100px;
        border: none;
        border-bottom: 2px solid darken($admin-blue, 20%);
        border-radius: 3px;

        &:hover{
            background: lighten($admin-blue, 20%);
            transition: background-color 0.5s;
        }
        &:disabled{
            background: lighten($admin-blue, 15%);
            border-bottom: 2px solid lighten($admin-blue, 20%);
        }
        &:active{
            background: $admin-yellow;
            border-bottom: 2px solid lighten($admin-yellow, 20%);
        }
    }

    .full-width{
        display: block;
        width: 100%;
    }
</style>