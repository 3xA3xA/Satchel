import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

export interface Brand {
  brantTypeId: number;
  name: string;
}

//возможно, уже существует :(
export interface ProductType {
  productTypeId: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})

export class CreateService {

  //сомнительный сервис, не похож на апишные. поэтому обращаться будет к разным методами с разными апи.
  private apiUrl: string = `${environment.apiUrl}`; 

  isCreateOpen : boolean = false;
  

  constructor(private http: HttpClient) { }

  setCreateWindowStatus() : void{
    this.isCreateOpen = !this.isCreateOpen;
    console.log(this.isCreateOpen)
  }

  getBrandTypes() {
    return this.http.get(this.apiUrl + `/BrandType/GetBrandTypes`)
  }

  getProductTypes() {
    return this.http.get(this.apiUrl + `/ProductType/GetProductTypes`)
  }
}
