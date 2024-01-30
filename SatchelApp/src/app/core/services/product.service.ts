import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';
import { ProductDto } from './config.service';

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

  //https://localhost:7082/api/Product/AddProduct
  addNewProduct(productDto: ProductDto, productImages: string[]) {
    // const object = {
    //   productDto: productDto,
    //   addProductImagesDto: productImages
    // };
  
    // return this.http.post(`${this.apiUrl}/AddProduct`, object);
    console.log(productDto)
    console.log(productImages)
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
}
