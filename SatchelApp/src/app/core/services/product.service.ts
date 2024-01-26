import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';
import { Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
import { Filters } from 'src/app/shared/components/routers/catalog/catalog.component';

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

  getFilteredProducts(filters: Filters, productType: string): Observable<Product[]>{

    const filteredParams = Object.fromEntries(
      Object.entries(filters)
        .filter(([key, value]) => value != null) 
    );

    return this.http.get<Product[]>(`${this.apiUrl}/Product/GetAllProducts/${productType}/`, {
      params: filteredParams
    });
  }

  formatNumber(num: number) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
  }

  getFormattedPrice(price: number) {
    return `${this.formatNumber(price)} ₽`; 
  }
}
