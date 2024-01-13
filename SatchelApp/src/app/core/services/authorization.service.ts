import { Injectable } from '@angular/core';
import { Subject, Observable, of} from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IUserDto } from './user.service';

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

  constructor(private http: HttpClient) {}

  private apiUrl: string = `${environment.apiUrl}/User`;

  stepChange = new Subject();
  currentStep = 0;
  isRegistrationOpen : boolean = false;

  // полная залупа
  steps: Step[] = [
    {
      title: 'Вход в аккаунт',
      fields: [
        {name: 'email', type: 'email'},
        {name: 'password', type: 'password'} 
      ]
    },
    {
      title: 'Регистрация',
      fields: [
        {name: 'email', type: 'email'},
        {name: 'password', type: 'password'},
      ]
    } 
  ]

  get step() {
    return this.steps[this.currentStep]; 
  }

  sendLoginRequestToBackend(email: string, password: string): Observable<IUserDto> {
    this.closeAuthWindow();
    const user = { //поменять
      email: email,
      password: password
    };
    return this.http.post<IUserDto>(this.apiUrl + '/LoginUser', user);   
  }

  sendRegistrationRequestToBackend(email : string, password : string, userTypeId : boolean) : Observable<IUserDto> {
    //перепутаны поля password и нижнее для типа аккаунта
    this.closeAuthWindow();
    const user = {
      email: email,
      password: password,
      userTypeId: userTypeId
    };
    return this.http.post<IUserDto>(this.apiUrl + '/CreateUser', user);
  }
  
  resetSteps() {
    this.currentStep = 0; 
    this.stepChange.next(this.currentStep); 
  }  

  goToRegistration(){
    this.currentStep = 1; 
    this.stepChange.next(this.currentStep); 
  }

  setAuthWindowStatus() : void {
    this.isRegistrationOpen = true;
  }

  closeAuthWindow() : void{
    this.resetSteps();
    this.isRegistrationOpen = false;
  }
}