<template>
  <div v-if="timelineBuilt">
    <apexchart
      type="area"
      :width="`100%`"
      height="300"
      :options="options"
      :series="series"
    >
    </apexchart>
  </div>
</template>


<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import { IInventoryTimeline } from "@/types/InventoryGraph"
import { Get, Sync } from 'vuex-pathify';
import VueApexCharts from 'vue3-apexcharts';
import moment from 'moment';
import apexchart from '@/App.vue'
import { Store } from 'vuex';



@Options({
    components: {  },
    
})



export default class InventoryChart extends Vue {
    components:{}
    

// declare $store:{
//   dispatch
// }


    @Sync("snapshotTimeline")
    snapshotTimeline?: IInventoryTimeline;
    @Get("isTimelineBuilt")
    timelineBuilt?: boolean;

  get options() {
    return {
      dataLabels: { enabled: false },
      fill: {
        type: "gradient"
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        // categories: this.snapshotTimeline.timeline,
        categories: this.snapshotTimeline.timeline.map(t =>
          moment(t).format("dd HHMMss")
        ),
        type: "datetime"
      }
    };
  }
    get series(){
        return this.snapshotTimeline.productInventorySnapshots
            .map(snapshot => (
                { name: snapshot.productId,
                data: snapshot.quantityOnHand
                }
            ));
    }
    async created(){
      
        await this.$store.dispatch("assignSnapshots");
    }
}
</script>

<style scoped lang="scss">

</style>