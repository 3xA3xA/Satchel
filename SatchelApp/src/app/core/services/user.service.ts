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
  dateOfBirth: Date;
  userPhotoSrc: string;
  // можно будет дописать - отзывы
}

@Injectable({
  providedIn: 'root'
})

export class UserService {
  constructor(private http: HttpClient) { }

  private apiUrl: string = `${environment.apiUrl}/User`;
  isAuthorized = false; // если true - пользователь зашел в аккаунт
  userId = 0;

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

  updateUserInfo(userData: IUserPageData): Observable<IUserPageData>  {
    console.log(userData)
    return this.http.put<IUserPageData>(`${this.apiUrl}/UpdateProfileInfoUser/${this.userId}`, userData);
  }

  getUserData(): Observable<IUserPageData> {
    return this.http.get<IUserPageData>(`${this.apiUrl}/GetViewUserData/${this.userId}`);
  } 
}
