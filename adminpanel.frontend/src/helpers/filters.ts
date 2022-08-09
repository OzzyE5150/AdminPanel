import moment from "moment";

const filters ={
    usdPriceFilter(number: number){
        if(isNaN(number)){
          return '-';
        }
        return '$ '+number.toFixed(2)
      },

      euPriceFilter(number: number){
        if(isNaN(number)){
          return '-';
        }
        return number.toFixed(2)+' €'
      },
      humanizeDate(date: Date){
        return moment(date).format('MMMM Do YYYY');
    }
      
}
export default filters;