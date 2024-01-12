import { Injectable } from '@angular/core';
import { Subject, Observable, of} from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IUser } from './user.service';
import { environment } from 'src/environments/environment';

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
        {name: 'userTypeId', type: 'userTypeId'}
      ]
    } 
  ]

  get step() {
    return this.steps[this.currentStep]; 
  }

  sendLoginRequestToBackend(email: string, password: string): Observable<IUser> {
    this.closeAuthWindow();
    const user = { //поменять
      email: email,
      password: password
    };
    return this.http.post<IUser>(this.apiUrl + '/LoginUser', user);   
  }

  sendRegistrationRequestToBackend(email : string, password : string, userTypeName : string) {
    //перепутаны поля password и нижнее для типа аккаунта
    console.log(email, password);

    this.closeAuthWindow();
    return this.addNewUser(email, password, userTypeName);
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

  addNewUser(email: string, password: string, userTypeName: string): Observable<IUser> {
    const user = {
      email: email,
      password: password,
      userTypeName: userTypeName
    };
    return this.http.post<IUser>(this.apiUrl + '/CreateUser', user);
  }
}
