import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

export interface UserDto {
  userId: number,
  email: string,
  userTypeName: string
}

export interface UserPageData{
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

  private apiUrl: string = `${environment.apiUrl}/User`;
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
    return this.http.put<UserPageData>(`${this.apiUrl}/UpdateProfileInfoUser/${this.userId}`, userData);
  }

  getUserData(): Observable<UserPageData> {
    return this.http.get<UserPageData>(`${this.apiUrl}/GetViewUserData/${this.userId}`);
  } 
}
