import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  isRegistrationOpen : boolean = false;

  constructor() { }

  openAuthWindow() : void {
    this.isRegistrationOpen = true;
  }

  closeAuthWindow() : void{
    this.isRegistrationOpen = false;
  }
}
