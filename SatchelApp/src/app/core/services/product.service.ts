import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Product  from 'src/app/shared/components/routers/catalog/catalog.component';
import { Observable} from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl: string = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  imageAlt = '';

  productRecords : Product[] = []; 

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/Product/GetProductById/${id}`);
  }

  getAllProducts(productType : string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/Product/GetAllProducts/${productType}`);
  }
}
