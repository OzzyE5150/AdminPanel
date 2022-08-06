<template>
  <div class="inventory-container">
      <h1 id="inventoryTitle">
      Inventory Dashboard
      </h1>
      <hr/>

      <div class="inventory-actions">
        <admin-button @click="showNewProductModal" id="addNewBtn">
          Add New Item
        </admin-button>
        <admin-button @click="showShipmentModal" id="receiveShipmentBtn">
          Receive Shipment
        </admin-button>
      </div>

      <table id="inventoryTable" class="table">
        <tr>
          <th>Item</th>
          <th>Quantity On-hand</th>
          <th>Unit Price</th>
          <th>Taxable</th>
          <th>Delete</th>
        </tr>

        <tr v-for="item in inventory" :key="item.id">
          <td>
            {{item.product.name}}
          </td>
          <td>
            {{item.quantityOnHand}}
          </td>
          <td>
            {{ euPriceFilter(item.product.price) }}
          </td>
          <td>
            <span v-if="item.product.isTaxable">
            Yes
            </span>
            <span v-else>No</span>
          </td>
          <div>
                X
            </div>
        </tr>

      </table>
      <new-product-modal v-if="isNewProductVisible"/>
      <shipment-modal 
        v-if="isShipmentVisible" 
        :inventory="inventory"
        @close="closeModals"
      />
  </div>
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import { IProductInventory } from "@/types/Product";
import { AppConfig } from 'vue';
import AdminButton from '../components/AdminButton.vue';


@Options({
        components: { AdminButton }
    })

export default class Inventory extends Vue {
  euPriceFilter(number: number){ 
    if(isNaN(number)){
          return '-';
        }
        return number.toFixed(2)+' €'
   }
   isNewProductVisible: boolean = false;
   isShipmentVisible: boolean = false;
  inventory: IProductInventory[] = [
    {
      id: 1,
      product: {
        id: 1,
        name: 'a product', 
        description: 'hello world', 
        price: 10,
        createdOn: new Date(), 
        updatedOn: new Date(),
        isTaxable: true, 
        isArchived: false
        }, 
        quantityOnHand: 100,
        idealQuantiy: 100
    },
    {
      id: 2,
      product: {
        id: 2,
        name: 'some product', 
        description: 'hello', 
        price: 13,
        createdOn: new Date(), 
        updatedOn: new Date(),
        isTaxable: true, 
        isArchived: false
        }, 
        quantityOnHand: 70,
        idealQuantiy: 100
    }
  ];

  closeModals(){
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }

  showNewProductModal(){}
  showShipmentModal(){}

}

</script>

<style scoped>

</style>