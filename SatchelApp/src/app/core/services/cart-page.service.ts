import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';


@Injectable({
  providedIn: 'root'
})
export class CartPageService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = `${environment.apiUrl}`;

  GetShoppingCart(userId: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl + `/ShoppingCart/GetShoppingCart?userId=${userId}`)
  }

  GetOrders(){
    
  }

  AddProductToShoppingCart(productId: number, userId: number, selectedSize: string) {
    return this.http.post(this.apiUrl + `/ShoppingCart/AddProductToShoppingCart?productId=${productId}&userId=${userId}&sizeTypeName=${selectedSize}`, null)
  }

  DeleteProductFromShoppingCart(productId: number, userId: number, sizeTypeId: number) {
    return this.http.delete(this.apiUrl + `/ShoppingCart/DeleteProductFromShoppingCart?productId=${productId}&userId=${userId}&sizeTypeId=${sizeTypeId}`);
  }  
}
