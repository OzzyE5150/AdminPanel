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
      <new-product-modal v-if="isNewProductVisible"
      @save:product="saveNewProduct"
      @close="closeModals"/>
      <shipment-modal 
        v-if="isShipmentVisible" 
        :inventory="inventory"
        @save:shipment="saveNewShipment"
        @close="closeModals"
      />
  </div>
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import { IProduct, IProductInventory } from "@/types/Product";
import { AppConfig } from 'vue';
import AdminButton from '../components/AdminButton.vue';
import { InventoryService } from '../services/inventory-service';
import {ProductService} from '../services/product-service'
import { IShipment } from "@/types/Shipment";
// import { ProductService } from "@/services/product-service";
import NewProductModal from "@/components/modals/NewProductModal.vue";
import ShipmentModal from "@/components/modals/ShipmentModal.vue";
import InventoryChart from "@/components/charts/InventoryChart.vue";

const inventoryService = new InventoryService();
const productService = new ProductService();

@Options({
        components: { AdminButton, ShipmentModal, NewProductModal }
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

  inventory: IProductInventory[] = [];

  closeModals(){
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }

  showNewProductModal(){
    this.isNewProductVisible = true;
  }
  showShipmentModal(){
    this.isShipmentVisible = true;
  }
  async saveNewProduct(newProduct: IProduct){
    await productService.save(newProduct);
    this.isNewProductVisible = false;
    await this.initialize();
  }
  async saveNewShipment(shipment: IShipment){
     await inventoryService.updateInventoryQuantity(shipment);
    this.isShipmentVisible = false;
    await this.initialize();
  }
  
  async initialize(){
    this.inventory = await inventoryService.getInventory();
  }
  async created(){
    await this.initialize();
  }

}

</script>

<style scoped>

</style>