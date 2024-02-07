import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';
import { AddProductBody, AddProductDto } from './config.service';

// не могу его вынести
export interface Filters{
  filterByMinPrice?: number, 
  filterByMaxPrice?: number, 
  filterByGender?: number, 
  filterByName?: string, 
  isFilterByDecreasePrice?: boolean, 
  isFilterByIncreasePrice?: boolean
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl: string = `${environment.apiUrl}/Product`;

  constructor(private http: HttpClient) { }

  productRecords : Product[] = []; 

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/GetProductById/${id}`);
  }

  getAllProducts(productType : string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/GetAllProducts/${productType}`);
  }

  // addNewProduct(productDto: ProductDto) {
  //   return this.http.post(`${this.apiUrl}/AddProduct`, productDto);
  // }

  addNewProduct(productBody: AddProductBody) {
    const formData = new FormData();
    Object.entries(productBody.addProductDto).forEach(([key, value]) => {
        if (Array.isArray(value)) {
            value.forEach((v, i) => formData.append(`addProductDto.${key}[${i}]`, v.toString()));
        } else {
            formData.append(`addProductDto.${key}`, value.toString());
        }
    });
    if (productBody.images instanceof FormData) {
        productBody.images.forEach((value, key) => {
            if (value instanceof Blob) {
                formData.append('images', value, key);
            }
        });
    }
    return this.http.post(`${this.apiUrl}/AddProduct`, formData);
  }


  getSellerProducts(userId: number): Observable<Product[]>{
    return this.http.get<Product[]>(`${this.apiUrl}/GetSellerProducts?userId=${userId}`)
  }
  
  
  deleteSellerProduct(productId: number) {
    return this.http.delete(`${this.apiUrl}/${productId}`)
  }

  getFilteredProducts(filters: Filters, productType: string): Observable<Product[]>{

    const filteredParams = Object.fromEntries(
      Object.entries(filters)
        .filter(([key, value]) => value != null) 
    );

    return this.http.get<Product[]>(`${this.apiUrl}/GetAllProducts/${productType}/`, {
      params: filteredParams
    });
  }
}
