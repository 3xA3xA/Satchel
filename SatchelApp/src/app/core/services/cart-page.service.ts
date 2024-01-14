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

  private apiUrl: string = `${environment.apiUrl}/ShoppingCart`;

  GetShoppingCart(userId: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl + `/GetShoppingCart?userId=${userId}`)
  }

  AddProductToShoppingCart(productId: number, userId: number) {
    return this.http.post(this.apiUrl + `/AddProductToShoppingCart?productId=${productId}&userId=${userId}`, null)
    .subscribe(data => {
      console.log(data + 'сохранилось');
    }, error => {
      console.error(error);
    });
  }
}
