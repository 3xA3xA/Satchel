import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Product from 'src/app/shared/components/routers/catalog/catalog.component';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7082/api/Product'; //URL API

  constructor(private http: HttpClient) { }

  imageAlt = '';

  productRecords : Product[] = []; 

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  getAllClothes(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }
}
