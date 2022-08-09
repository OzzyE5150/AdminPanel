<template>
  <div>
    <h1 id="ordersTitle">
        Sales Orders
    </h1>
    <table id="sales-orders" class="table" v-if="orders.length">
        <thead>
            <tr>
                <th>CustomerId</th>
                <th>OrderId</th>
                <th>Order Total</th>
                <th>Order Status</th>
                <th>Mark Complete</th>
            </tr>
             <tr v-for="order in orders" :key="order.id">
                <td>
                 {{ order.customer.id }}
                </td>
                <td>
                 {{ order.id }}
                </td>
                <td>
                 {{ price(getTotal(order)) }}
                </td>
                <td :class="{green : order.isPaid}">
                    {{ getStatus(order.isPaid )}}
                </td>
                <td>
                    <div
                        i-if="!order.isPaid"
                        class="lni lni-checkmark-circle order-complete"
                        @click="markComplete(order.id)"
                    >
                    </div>
                </td>
             </tr>
        </thead>
    </table>
  </div>
</template>

<script lang="ts">
import {Options, Vue} from 'vue-class-component';
import { OrderService } from '../services/order-service';
import { ISalesOrder } from '../types/Order';
import filters from '../helpers/filters';

const orderService = new OrderService();


@Options({
    components: { }
})
export default class Orders extends Vue {
    price = filters.usdPriceFilter

    orders: ISalesOrder[] =[];

    getTotal(order: ISalesOrder){
        return order.salesOrderItems.reduce((a, b) => a + (b['product']['price'] * b['quantity']), 0);
    }
    getStatus(isPaid: boolean){
        return isPaid ? "Paid in Full" : "Unpaid";
    }
    async markComplete(orderId: number){
        await orderService.markOrderComplete(orderId);
        this.initialize();
    }

    async initialize(){
        this.orders = await orderService.getOrders();
    }
    async created(){
        await this.initialize();
    }
    
}
</script>

<style scoped lang="scss">

</style>