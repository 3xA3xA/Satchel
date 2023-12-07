import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface Step {
  title: string;
  fields: {
    name: string;
    type: string;
  }[] 
}

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  stepChange = new Subject();

  steps: Step[] = [
    {
      title: 'Введите email',
      fields: [
        {name: 'email', type: 'email'}  
      ]
    },
    {
      title: 'Введите пароль',
      fields: [
        {name: 'password', type: 'password'} 
      ]
    },
    {
      title: 'Регистрация',
      fields: [
        {name: 'email', type: 'email'},
        {name: 'password', type: 'password'},
        {name: 'name', type: 'name'}
      ]
    } 
  ]

  currentStep = 0;

  get step() {
    return this.steps[this.currentStep]; 
  }

  login() {
    this.currentStep = 1; 
    this.stepChange.next(this.currentStep); 

    console.log(`Отработал сервис со стэпом ${this.currentStep}`)
  }

  prev() {
    this.currentStep--;
  }

  isRegistrationOpen : boolean = false;

  constructor() { }

  openAuthWindow() : void {
    this.isRegistrationOpen = true;
  }

  closeAuthWindow() : void{
    this.isRegistrationOpen = false;
  }
}
