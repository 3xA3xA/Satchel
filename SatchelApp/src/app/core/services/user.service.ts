import { Injectable } from '@angular/core';

export interface IUser {
  email: string;
  password: string;
  userType: string;
}

@Injectable({
  providedIn: 'root'
})

export class UserService {

  isAuthorized = true

  setAuthorizedStatus(){
    this.isAuthorized = !this.isAuthorized;
  }

  get authorizedStatus(){
    return this.isAuthorized
  }

  constructor() { }
}
