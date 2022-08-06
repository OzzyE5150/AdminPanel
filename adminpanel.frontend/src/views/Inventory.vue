<template>
  <div class="inventory-container">
      <h1 id="inventoryTitle">
      Inventory Dashboard
      </h1>
      <hr/>

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
  </div>
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import { IProductInventory } from "@/types/Product";
import { AppConfig } from 'vue';


@Options({
        components: {}
    })

export default class Inventory extends Vue {
  euPriceFilter(number: number){ 
    if(isNaN(number)){
          return '-';
        }
        return number.toFixed(2)+' €'
   }
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
}

</script>

<style scoped>

</style>