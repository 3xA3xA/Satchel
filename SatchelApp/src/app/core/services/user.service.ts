import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

export interface IUserDto {
  userId: number,
  email: string,
  userTypeName: string
}

export interface IUserPageData{
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  dateOfBirth: string;
  userPhotoSrc: string;
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

  setAuthorizedStatus(){
    this.isAuthorized = !this.isAuthorized;
  }

  get authorizedStatus(){
    return this.isAuthorized
  }

  getUserData(): Observable<IUserPageData> {
    return this.http.get<IUserPageData>(`${this.apiUrl}/User/GetViewUserData/${this.userId}`);
  } 
}
