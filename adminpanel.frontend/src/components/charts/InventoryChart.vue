<template>
  <div>
    <apexchart 
        type="area"
        :width="'100%'"
        height="300"
        :options="options"
        :series="series"
    ></apexchart>
  </div>
</template>


<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import { IInventoryTimeline } from "@/types/InventoryGraph"

@Options({
    components: {}
})



export default class InventoryChart extends Vue {

    snapshotTimeline: IInventoryTimeline;

    get options(){
        return{
            dataLabels: { enabled: false },
            fill:{
                type: 'gradient'
            },
            stroke: {
                curve: 'smooth'
            },
            xaxis: {
                categories: this.snapshotTimeline.timeline,
                type: "datetime"
            }
        };
    }
}
</script>

<style scoped lang="scss">

</style>