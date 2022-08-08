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
          <td v-bind:class="`${applyColor(item.quantityOnHand, item.idealQuantity)}`">
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
          <td>
          <div class="lni lni-cross-circle product-archive" @click="archiveProduct(item.product.id)">
          </div>
          </td>
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

  async archiveProduct(productId: number){
    await productService.archive(productId)
    await this.initialize
  }

  applyColor(current: number, target: number){
    if(current <= 0){
      return "red";
    } if (Math.abs(target-current) > 8){
      return "yellow";
    }
    return "green";
  }

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

<style scoped lang="scss">
  @import "@/scss/global.scss";

  .green {
    font-weight: bold;
    color: $admin-green;
  }
.yellow {
    font-weight: bold;
    color: $admin-yellow;
  }
  .red {
    font-weight: bold;
    color: $admin-red;
  }
  .inventory-actions{
    display: flex;
    margin-bottom: 0.8rem;
  }
  .product-archive{
    cursor: pointer;
    font-weight: bold;
    font-size: 1.2rem;
    color: $admin-red;
    
  }
</style>