<template>
  <admin-modal>
    <template v-slot:header>
        Add New Product
    </template>
    <template v-slot:body>
        <ul class="newProduct">
            <li>
                <label for="isTaxable">Is this product taxable?</label>
                <input
                    type="checkbox"
                    id="isTaxable"
                    v-model="newProduct.isTaxable"
                    />
            </li>
            <li>
                <label for="productName">Name</label>
                <input type="text" id="productName" v-model="newProduct.name"/>
            </li>

            <li>
                <label for="productDesc">Description</label>
                <input
                    type="text"
                    id="productDesc"
                    v-model="newProduct.description"
                    />
            </li>
            <li>
                <label for="productPrice">Price (EUR)</label>
                <input type="number" id="productPrice" v-model="newProduct.price" />
            </li>
        </ul>
    </template>

   <template v-slot:footer>
      <admin-button
        type="button"
        @click="save"
        aria-label="save new item"
      >
        Save Product
      </admin-button>
      <admin-button
        type="button"
        @click="close"
        aria-label="close modal"
      >
        Close
      </admin-button>
      </template>
  </admin-modal>
</template>

<script lang="ts">
    import { IProduct, IProductInventory } from "@/src/types/Product";
    import { Options, Vue } from "vue-class-component";
    import { Prop } from "vue-property-decorator";
    import AdminButton from "../AdminButton.vue";
    import AdminModal from "./AdminModal.vue";

     @Options({
        components: { AdminButton, AdminModal }
    })

export default class NewProductModal extends Vue {
    newProduct: IProduct = {
        createdOn: new Date(),
        updatedOn: new Date(),
        id: 0,
        description: "",
        isTaxable: false,
        name: "",
        price: 0,
        isArchived: false
    };
    close() {
    this.$emit("close");
  }
  save() {
    this.$emit("save:product", this.newProduct);
  }


}
</script>

<style scoped lang="scss">
.newProduct {
  list-style: none;
  padding: 0;
  margin: 0;
  input {
    width: 100%;
    height: 1.8rem;
    margin-bottom: 1.2rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }
  label {
    font-weight: bold;
    display: block;
    margin-bottom: 0.3rem;
  }
}
</style>