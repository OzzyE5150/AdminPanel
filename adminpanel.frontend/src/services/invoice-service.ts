import axios, { AxiosError } from "axios";
import { IInvoice } from '@/types/Invoice';

export default class InvoiceService {
 API_URL = process.env.VUE_APP_API_URL;

 public async makeNewInvoice(invoice: IInvoice): Promise<boolean>{
    let now = new Date();
    invoice.createdOn = now;
    invoice.updatedOn = now;
    let result: any = await axios.post(`${this.API_URL}/invoice/`, invoice);
   try{
      return result.data;
   }catch(err){ console.log(`Error: ${(err as AxiosError)?.response?.data}`); }
    
 }
}