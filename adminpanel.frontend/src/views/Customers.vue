<template>
<div>
    <h1 id="customersTitle">
        Manage Customers
    </h1>
    <hr />
    <div class="customer-actions">
        <admin-button @button:click="showNewCustomerModal">
            Add Customer
        </admin-button>
    </div>
    <table id="customers" class="table">
        <tr>
            <th>Customer</th>
            <th>Address</th>
            <th>State</th>
            <th>Since</th>
            <th>Delete</th>
        </tr>
        <tr v-for="customer in customers" :key="customer.id">
            <td>
                {{ customer.firstName + " " + customer.lastName }}
            </td>
            <td>
                {{ customer.primaryAddress.addressLine1+ " "+ customer.primaryAddress.addressLine2}}
            </td>
            <td>
                {{ customer.primaryAddress.state }}
            </td>
            <td>
                {{ humanizeDate(customer.createdOn) }}
            </td>
            <td>
          <div class="lni lni-cross-circle customer-delete" >
          </div>
          </td>
        </tr>
    </table>
    <new-customer-modal
        @close="closeModal"
        @save:customer="saveNewCustomer"
        v-if="isNewCustomerModalVisible"
    />
</div>
  
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import AdminButton from '../components/AdminButton.vue';
import {ICustomer, ICustomerAddress} from "@/types/Customer"
import {CustomerService} from '@/services/customer-service';
import moment from 'moment';
import NewCustomerModal from '@/components/modals/NewCustomerModal.vue'

const customerService = new CustomerService();

@Options({
    components: { AdminButton, NewCustomerModal }
})

export default class Customers extends Vue {

    humanizeDate(date: Date){
        return moment(date).format('MMMM Do YYYY');
    }

    customers: ICustomer[] = [];
    isNewCustomerModalVisible: boolean = false;

    async saveNewCustomer(customer: ICustomer){
     await customerService.addCustomer(customer);
    this.isNewCustomerModalVisible = false;
    await this.initialize();
  }
    async deleteCustomer(id: number){
        await customerService.deleteCustomer(id);
        await this.initialize();
    }

    async showNewCustomerModal(){
        this.isNewCustomerModalVisible = true;
    }
    closeModal(){
        this.isNewCustomerModalVisible = false;
    }
    async initialize(){
        this.customers = await customerService.getCustomers();
    }
    async created(){
       await this.initialize();
    }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.customer-actions{
    display: flex;
    margin-bottom: 0.8rem;

    div {
        margin-right: 0.8rem;
    }
}
.customer-delete {
    cursor: pointer;
    font-weight: bold;
    font-size: 1.2rem;
    color: $admin-red;
}

</style>