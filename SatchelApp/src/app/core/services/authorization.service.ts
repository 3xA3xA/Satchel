import { Injectable } from '@angular/core';
import { Subject, Observable, of} from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

export interface Step {
  title: string;
  fields: {
    name: string;
    type: string;
  }[] 
}

export interface IUser {
  email : string;
  password: string;
  userType: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(private http: HttpClient) {}

  private apiUrl = 'https://localhost:7082/api';

  stepChange = new Subject();
  currentStep = 0;
  isRegistrationOpen : boolean = false;

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
        {name: 'userTypeId', type: 'userTypeId'}
      ]
    } 
  ]

  get step() {
    return this.steps[this.currentStep]; 
  }

  login(email?: string, password?: string): Observable<IUser> {
    this.currentStep = 1; 
    this.stepChange.next(this.currentStep); 
  
    if (email && password)
    {
      this.resetSteps();
      this.closeAuthWindow();
      const user = {
        email: email,
        password: password
      };
      return this.http.post<IUser>(this.apiUrl + '/LoginUser', user);
    }
    return of({email: '', password: '', userType: 0}); //пустой
  }

  registation(email : string, password : string, userTypeId : number){
    this.currentStep = 2;
    this.stepChange.next(this.currentStep); 

    //перепутаны поля password и нижнее для типа аккаунта
    console.log(email, password)

    if (email && password)
    {
      this.resetSteps();
      this.closeAuthWindow();
      return this.addNewUser(email, password, 1);
    }
    return of({email: '', password: '', userType: userTypeId}); //пустой
  }
  
  resetSteps() {
    this.currentStep = 0; 
    this.stepChange.next(this.currentStep); 
  }  

  goToRegistration(){
    this.currentStep = 2; 
    this.stepChange.next(this.currentStep); 
  }

  openAuthWindow() : void {
    this.isRegistrationOpen = true;
  }

  closeAuthWindow() : void{
    this.isRegistrationOpen = false;
  }

  addNewUser(email: string, password: string, userTypeId: number): Observable<IUser> {
    const user = {
      email: email,
      password: password,
      userTypeId: userTypeId
    };
    return this.http.post<IUser>(this.apiUrl + '/CreateUser', user);
  }
}
