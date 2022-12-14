<template>
  <div>
    <h1 id="invoiceTitle">Create Invoice</h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep === 1">
        <h2>Step 1: Select Customer</h2>
        <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer: </label>
        <select v-model="selectedCustomerId" class="invoice-customers" id="customer">
            <option disabled value="">Please select a customer</option>
            <option v-for="c in customers" :value="c.id" :key="c.id">{{ c.firstName+" "+c.lastName }}</option>
            </select>
          
        </div>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 2">
        <h2>Step 2: Create Order</h2>
            <div v-if="inventory.length" class="invoice-step-detail">
            <label for="product">Product: </label>
            <select v-model="newItem.product" class="invoiceLineItem" id="product">
                <option disabled value="">Please select a product</option>
                <option v-for="i in inventory" :value="i.product" :key="i.product.id">
                {{ i.product.name }}
                </option>
            </select>
            <label for="quantity"> Quantity: </label>
            <input v-model="newItem.quantity" type="number" min="0" id="quantity"/>
            </div>
            
        <div class="invoice-item-actions">
            <admin-button :disabled="!newItem.product || !newItem.quantity" @button:click="addLineItem">
            Add Line Item</admin-button>
            <admin-button :disabled="!lineItems.length" @button:click="finalizeOrder">
            Finalize Order</admin-button>
        </div>
        <div class="invoice-order-list" v-if="lineItems.length">
            <div class="runningTotal">
                <h3>Running Total: </h3>
                {{price(runningTotal)}}
            </div>
             <hr />
        <table class="table">
          <thead>
            <tr>
              <th>Product</th>
              <th>Description</th>
              <th>Qty.</th>
              <th>Price</th>
              <th>Subtotal</th>
            </tr>
          </thead>
            <tr
            v-for="lineItem in lineItems"
            :key="`index_${lineItem.product.id}`"
            >
             <td>{{ lineItem.product.name }}</td>
             <td>{{ lineItem.product.description }}</td>
             <td>{{ lineItem.quantity }}</td>
             <td>{{ lineItem.product.price }}</td>
             <td> {{ price(lineItem.product.price * lineItem.quantity) }} </td>
             </tr>
            </table>
        </div>
    </div>
    
    <div class="invoice-step" v-if="invoiceStep === 3">
     <h2>Step 3: Review and Submit</h2>
      <admin-button @button:click="submitInvoice"> Submit Invoice </admin-button>
     <hr />

    <div class="invoice-step-detail" id="invoice" ref="invoice">
        <div class="invoice-logo">
            <img id="imgLogo" alt="Solar Coffe logo" src="../assets/images/logo.png">
            <h3>1712 Code Lane</h3>
            <h3>San Softwarino, CA 9000</h3>
            <h3>USA</h3> 

          <div class="invoice-order-list" v-if="lineItems.length">
            <div class="invoice-header">
              <h3>Invoice: {{ date(new Date()) }}</h3>
              <h3>
                Customer:
                {{
                  this.selectedCustomer.firstName +
                    " " +
                    this.selectedCustomer.lastName
                }}
              </h3>
              <h3>
                Address: {{ this.selectedCustomer.primaryAddress.addressLine1 }}
              </h3>
              <h3 v-if="this.selectedCustomer.primaryAddress.addressLine2">
                {{ this.selectedCustomer.primaryAddress.addressLine2 }}
              </h3>
              <h3>
                {{ this.selectedCustomer.primaryAddress.city }},
                {{ this.selectedCustomer.primaryAddress.state }},
                {{ this.selectedCustomer.primaryAddress.postalCode }}
              </h3>
              <h3>
                {{ this.selectedCustomer.primaryAddress.country }}
              </h3>
            </div>
            <table class="table">
              <thead>
                <tr>
                  <th>Product</th>
                  <th>Description</th>
                  <th>Qty.</th>
                  <th>Price</th>
                  <th>Subtotal</th>
                </tr>
              </thead>
              <tr
                v-for="lineItem in lineItems"
                :key="`index_${lineItem.product.id}`"
              >
                <td>{{ lineItem.product.name }}</td>
                <td>{{ lineItem.product.description }}</td>
                <td>{{ lineItem.quantity }}</td>
                <td>{{ lineItem.product.price }}</td>
                <td>
                  {{ price(lineItem.product.price * lineItem.quantity) }}
                </td>
              </tr>
              <tr>
                <th colspan="4"></th>
                <th>Grand Total</th>
              </tr>
              <tfoot>
                <tr>
                  <td colspan="4" class="due">Balance due upon receipt:</td>
                  <td class="price-final">{{ price(runningTotal) }}</td>
                </tr>
              </tfoot>
            </table>
          </div>
        </div>
      </div>
    </div>
    <hr />
    <div class="invoice-steps-actions">
        <admin-button @button:click="prev" :disabled="!canGoPrev">Previous</admin-button>
        <admin-button @button:click="next" :disabled="!canGoNext">Next</admin-button>
        <admin-button @button:click="startOver">Start Over</admin-button>
    </div>
  </div>
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import AdminButton from '../components/AdminButton.vue';
import { CustomerService } from '../services/customer-service';
import { InventoryService } from '../services/inventory-service';
import InvoiceService from '../services/invoice-service';
import { ICustomer } from '../types/Customer';
import { IInvoice, ILineItem } from '../types/Invoice';
import { IProductInventory } from '../types/Product';
import filters from '../helpers/filters';
import jsPDF from "jspdf";
import html2canvas from "html2canvas"

const customerService = new CustomerService();
const inventoryService = new InventoryService();
const invoiceService = new InvoiceService();

@Options({
    components: { AdminButton }
})
export default class CreateInvoice extends Vue {
  declare $refs:{
  invoice:HTMLElement
}

    invoiceStep = 1;
    invoice: IInvoice = {
        createdOn: new Date(),
        updatedOn: new Date(),
        lineItems: [],
        customerId: 0
    };
    customers: ICustomer[] = [];
    selectedCustomerId: number = 0;
    inventory: IProductInventory[] =[];
    lineItems: ILineItem[] =[];
    newItem: ILineItem = {product: undefined, quantity: 0};

     price = filters.usdPriceFilter
     date = filters.humanizeDate

    addLineItem(){
        let newItem: ILineItem={
            product: this.newItem.product,
            quantity: parseInt(this.newItem.quantity.toString())
        };

        let existingItems = this.lineItems.map(item => item.product.id);

        if(existingItems.includes(newItem.product.id)){
            let lineItem = this.lineItems.find(
                item => item.product.id === newItem.product.id
            );
            
            let currentQuantity = Number(lineItem?.quantity);
            let updatedQuantity = currentQuantity += newItem.quantity;
            if(lineItem != undefined){
            lineItem.quantity = updatedQuantity;}
        }
        else {
            this.lineItems.push(this.newItem);
        }

        this.newItem = { product: undefined, quantity: 0};
    }

    startOver():void {
        this.invoice = { customerId: 0, lineItems: [], createdOn: new Date, updatedOn: new Date()};
        this.invoiceStep =1;
    }

    finalizeOrder(){
        this.invoiceStep = 3;
    }

    get canGoPrev(){
        return this.invoiceStep !== 1;
    }

    get canGoNext(){
        if(this.invoiceStep === 1){
            return this.selectedCustomerId !== 0;
        }
        if (this.invoiceStep === 2){
            return true;
        }
        if (this.invoiceStep === 3){ return false}

        return false;
    }
    get runningTotal(){
        return this.lineItems
                    .reduce((a, b) => a + (b['product']['price'] * b['quantity']), 0)
    }
    async submitInvoice(): Promise<void> {
        this.invoice ={
            customerId: this.selectedCustomerId,
            lineItems: this.lineItems,
            createdOn: new Date(),
            updatedOn: new Date()
        };

        await invoiceService.makeNewInvoice(this.invoice);

        this.downloadPdf();
        await this.$router.push("/orders");
    }

    downloadPdf(){
        let pdf = new jsPDF("p", "pt", "a4", true);
        let invoice = document.getElementById('invoice');
        let width = this.$refs.invoice.clientWidth;
        let height = this.$refs.invoice.clientHeight;

        html2canvas(invoice).then(canvas => {
            let image = canvas.toDataURL('image/png');
            pdf.addImage(image, 'PNG', 0, 0, width * 0.55, height * 0.55);
            pdf.save('invoice');
        });
    }

    prev():void{
        if (this.invoiceStep === 1){
            return;
        }
        this.invoiceStep -=1;
    }
    next():void{
        if(this.invoiceStep ===3){
            return;
        }
        this.invoiceStep +=1;
    }

      get selectedCustomer() {
    return this.customers.find(c => c.id == this.selectedCustomerId);
  }

    async initialize() : Promise<void> {
        this.customers = await customerService.getCustomers();
        this.inventory = await inventoryService.getInventory();
    }
    async created(){
       await this.initialize();
    }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.invoice-steps-actions{
    display: flex;
    width: 100%;
}
.price-pre-tax{
    font-weight: bold;
}
.price-final{
    font-weight: bold;
    color: $admin-green;
}
.due {
    font-weight: bold;
}
.invoice-header{
    width: 100%;
    margin-bottom: 1.2rem;
}
.invoice-logo{
    padding-top: 1.4rem;
    text-align: center;

    img{
        width: 280px;
    }
}
    .invoice-step{
    }
    .invoice-step-detail{
        margin: 1.2rem;
    }

    invoice-order-list{
        margin-top: 1.2rem;
        padding: 0.8rem;

        .line-item{
            display: flex;
            border-bottom: 1px dashed #ccc;
            padding: 0.8rem;
        }
        .item-col{
            flex-grow: 1;
        }
    }
</style>