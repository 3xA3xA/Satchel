import { Injectable } from '@angular/core';

export interface IUserDto {
  userId: number,
  email: string,
  userTypeName: string
}

@Injectable({
  providedIn: 'root'
})

export class UserService {

  isAuthorized = false; // если true - пользователь зашел в аккаунт
  userId = 0;

  setAuthorizedStatus(){
    this.isAuthorized = !this.isAuthorized;
  }

  get authorizedStatus(){
    return this.isAuthorized
  }

  constructor() { }
}
