<template>
  <div>
    <h1 id="ordersTitle">
        Sales Orders
    </h1>
    <hr />
    <table id="sales-orders" class="table" v-if="orders.length">
        <thead>
            <tr>
                <th>CustomerId</th>
                <th>OrderId</th>
                <th>Order Total</th>
                <th>Order Status</th>
                <th>Mark Complete</th>
            </tr>
             <tr v-for="lineItem in orders" :key="lineItem.id">
                <td>
                 {{ lineItem.customer.id }}
                </td>
                <td>
                 {{ lineItem.id }}
                </td>
                <td>
                 {{ price(getTotal(lineItem)) }}
                </td>
                <td :class="{green : lineItem.isPaid}">
                    {{ getStatus(lineItem.isPaid )}}
                </td>
                <td>
                    <div
                        v-if="!lineItem.isPaid"
                        class="lni lni-checkmark-circle order-complete green"
                        @click="markComplete(lineItem.id)"
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

    getTotal(lineItem: ISalesOrder){
        return lineItem.salesOrderItems.reduce((a, b) => a + (b['product']['price'] * b['quantity']), 0);
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
@import "src/scss/global.scss";
.green {
  font-weight: bold;
  color: $admin-green;
}

.order-complete {
  cursor: pointer;
  text-align: center;
}
</style>