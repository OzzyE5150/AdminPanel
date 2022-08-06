const filters ={
    euPriceFilter(number: number){
        if(isNaN(number)){
          return '-';
        }
        return number.toFixed(2)+' €'
      }
}
export default filters;