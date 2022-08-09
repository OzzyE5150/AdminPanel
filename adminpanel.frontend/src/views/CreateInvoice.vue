<template>
  <div>
    <h1 id="invoiceTitle">Create Invoice</h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep === 1">
        <h2>Step 1: Select Customer</h2>
        <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select v-model="selectedCustomerId" class="invoiceCustomers" id="customer">
            <option disabled value="">Please select a customer</option>
            <option v-for="c in customers" :value="c.id" :key="c.id">{{ c.firstName+" "+c.lastName }}</option>
            </select>
        </div>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 2">
        <h2>Step 2: Create Order</h2>
    </div>
    
    <div class="invoice-step" v-if="invoiceStep === 3">
    
    </div>
  </div>
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import { CustomerService } from '../services/customer-service';
import { InventoryService } from '../services/inventory-service';
import InvoiceService from '../services/invoice-service';
import { ICustomer } from '../types/Customer';
import { IInvoice, ILineItem } from '../types/Invoice';
import { IProductInventory } from '../types/Product';

const customerService = new CustomerService();
const inventoryService = new InventoryService();
const invoiceService = new InvoiceService();

@Options({
    components: { }
})
export default class CreateInvoice extends Vue {
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

</style>