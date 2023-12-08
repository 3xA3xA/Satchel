import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Product  from 'src/app/shared/components/routers/catalog/catalog.component';
import { Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7082/api'; //URL API научиться правильно писать роуты для сервиса

  constructor(private http: HttpClient) { }

  imageAlt = '';

  productRecords : Product[] = []; 

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/Product/${id}`); //вставил /Product/ хз, мб уйня
  }

  getAllClothes(productType : string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/${productType}`);
  }
}
