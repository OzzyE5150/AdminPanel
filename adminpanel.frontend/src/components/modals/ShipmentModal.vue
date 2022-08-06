<template>
  <admin-modal>
    <template v-slot:header>
        Receive Shipment
    </template>
    <template v-slot:body>
        <label for="product">Product Received</label>
        <select v-model="selectedProduct" class="shipmentItems" id="product">
            <option disabled value="">Please select one</option>
            <option v-for="item in inventory" :value="item" :key="item.product.id">
                {{item.product.name}}
            </option>
        </select>
        <label for="qtyReceived">Quantity Received</label>
        <input type="number" id="qtyReceived" v-model="qtyReceived"/>
    </template>
    <template v-slot:footer>
        <admin-button
            type="button"
            @button:click="save"
            aria-label="save new shipment">
            Save Received Shipment
        </admin-button>
        <admin-button
            type="button"
            @button:click="close"
            aria-label="close modal">
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
        components: { AdminButton }
    })

export default class ShipmentModal extends Vue{
    @Prop({required: true, type: Array as () => IProductInventory[]})
    inventory!: IProductInventory[];

    selectedProduct: IProduct = {
        createdOn: new Date(),
        updatedOn: new Date(),
        id: 0,
        description: "",
        isTaxable: false,
        name: "",
        price: 0,
        isArchived: false
    };

    qtyReceived: number = 0;

    close(){
        this.$emit("close");
    }
    save(){
        // let shipment: IShipment ={
        //     productId: this.selectedProduct.id,
        //     adjustment: this.qtyReceived
        // };
        // this.$emit('save:shipment', shipment);
    }
}
</script>

<style scoped> 

</style>