import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { GenderType } from './config.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class CreateService {

  private apiUrl: string = `${environment.apiUrl}`; 

  isCreateOpen : boolean = false;
  

  constructor(private http: HttpClient) { }

  setCreateWindowStatus() : void{
    this.isCreateOpen = !this.isCreateOpen;
  }

  getBrandTypes() {
    return this.http.get(this.apiUrl + `/BrandType/GetBrandTypes`)
  }

  getProductTypes() {
    return this.http.get(this.apiUrl + `/ProductType/GetProductTypes`)
  }

  getSizesByProductType(clothesType: string) { 
    return this.http.get(this.apiUrl + `/SizeType/GetSizeTypesByProductType?productTypeName=${clothesType}`)
  }

  getGenderTypes() : Observable<GenderType[]>{
    return this.http.get<GenderType[]>(this.apiUrl + `/GenderType/GetGenderTypes`)
  }
}
