import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';

@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  private apiUrl: string = `${environment.apiUrl}/Favourites`;

  constructor(private http: HttpClient) { }

  addFavouriteProduct(productId: number, userId: number){
    const params = new HttpParams()
      .set('productId', productId.toString())
      .set('userId', userId.toString());

    this.http.post(this.apiUrl + '/AddProductToFavourites', params);
  }
}
