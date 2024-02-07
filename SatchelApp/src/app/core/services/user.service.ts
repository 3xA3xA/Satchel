import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/app/shared/components/routers/catalog/catalog.component';
import { PaymentType, ShippingType } from './config.service';

// не могу вынести
export interface UserPageData{
  [key: string]: any;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  dateOfBirth: Date;
  userPhotoSrc: string;
  userType: string;
  // можно будет дописать - отзывы
}

@Injectable({
  providedIn: 'root'
})

export class UserService {
  constructor(private http: HttpClient) { }

  private apiUrl: string = `${environment.apiUrl}`;
  isAuthorized = false; // если true - пользователь зашел в аккаунт
  userId = 0;
  userTypeName = 'Покупатель';

  setAuthorizedStatus(){
    this.isAuthorized = !this.isAuthorized;
  }

  get authorizedStatus(){
    return this.isAuthorized
  }

  exitFromAccount(){
    this.setAuthorizedStatus();
    this.userId = 0;
  }

  updateUserInfo(userData: UserPageData): Observable<UserPageData>  {
    return this.http.put<UserPageData>(`${this.apiUrl}/User/UpdateProfileInfoUser/${this.userId}`, userData);
  }

  getUserData(): Observable<UserPageData> {
    return this.http.get<UserPageData>(`${this.apiUrl}/User/GetViewUserData/${this.userId}`);
  } 

  getPaymentTypes() : Observable<PaymentType[]> {
    return this.http.get<PaymentType[]>(`${this.apiUrl}/PaymentType/GetPaymentTypes`)
  }

  getShippingTypes() : Observable<ShippingType[]> {
    return this.http.get<ShippingType[]>(`${this.apiUrl}/ShippingType/GetShippingTypes`)
  }
}
