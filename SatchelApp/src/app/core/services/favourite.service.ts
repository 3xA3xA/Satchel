import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';

@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  private apiUrl: string = `${environment.apiUrl}/Favourites`;

  constructor(private http: HttpClient) { }

  AddFavouriteProduct(productId: number, userId: number) {
    return this.http.post(this.apiUrl + `/AddProductToFavourites?productId=${productId}&userId=${userId}`, null);
  }

  DeleteProductFromFavourites(productId: number, userId: number){
    return this.http.delete(this.apiUrl + `/DeleteProductFromFavourites?productId=${productId}&userId=${userId}`)
  }

  getAllProducts(productType : string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/Product/GetAllProducts/${productType}`);
  }

  GetFavourites(userId: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl + `/GetFavourites?userId=${userId}`)
  }
}

  
