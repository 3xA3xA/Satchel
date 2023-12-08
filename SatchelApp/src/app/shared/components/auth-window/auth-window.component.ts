import { Component } from '@angular/core';
import { AuthorizationService, IUser } from 'src/app/core/services/authorization.service';

@Component({
  selector: 'app-auth-window',
  templateUrl: './auth-window.component.html',
  styleUrls: ['./auth-window.component.css']
})
export class AuthWindowComponent {

  step$ = this.registrationService.step;

  email: string = '';
  password: string = '';

  ngOnInit() {
    this.registrationService.stepChange.subscribe(() => {
      this.updateStep(); 
    });

  }  

  constructor(private registrationService: AuthorizationService) { }

  updateStep() {
    this.step$ = this.registrationService.step; 
  }

  login(): void {
    if (this.email && this.password) {
      this.registrationService.login(this.email, this.password).subscribe(
        (user: IUser) => {
          console.log(user); // поменять обработку тут и в error
        },
        error => {
          console.log(error);
          this.email = this.password = '';
          this.registrationService.resetSteps();
        }
      );
    }
    else {
      if (this.email) // полная уйня, если есть логин но нет пароля - редирект на регистрацию - вообще отказаться от ++
      {
        this.registrationService.currentStep++;
        this.updateStep();
      }     
    }
  }
  
  registration() : void{
    if (this.email || this.password)
    {
      this.registrationService.registation(this.email, this.password, 0).subscribe(
        (user: IUser) => {
          console.log(user); // поменять обработку тут и в error
        },
        error => {
          console.log(error);
        }
      );
    }
    else{
      this.registrationService.goToRegistration();
    }  
  }

  closeAuthWindow( ): void {
      this.registrationService.closeAuthWindow();
   }

   onEmailChange(event: any): void {
    this.email = event.target.value;
  }

  onPasswordChange(event: any): void {
    this.password = event.target.value;
  }
}
