import { Component } from '@angular/core';
import { AuthorizationService} from 'src/app/core/services/authorization.service';
import { UserService } from 'src/app/core/services/user.service';
import { IUserDto } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-auth-window',
  templateUrl: './auth-window.component.html',
  styleUrls: ['./auth-window.component.css']
})
export class AuthWindowComponent {

  step$ = this.registrationService.step;

  email: string = '';
  password: string = '';
  userTypeName: string = 'Покупатель'; // лучше поменять на id 
  isLoginVisible = true;
  isSwitchVisible = false;
  isChecked: boolean = false;

  ngOnInit() {
    this.registrationService.stepChange.subscribe(() => {
      this.updateStep(); 
    });
  }  

  constructor(private registrationService: AuthorizationService, private userService: UserService) { }

  updateStep() {
    this.step$ = this.registrationService.step; 
    this.isLoginVisible = false;
    this.isSwitchVisible = true;
  }

  updateUserTypeName(isChecked: boolean) {
    if (isChecked === false)
      this.userTypeName = 'Продавец'
    else
      this.userTypeName = 'Покупатель'
  }

  handleUserLogin(): void {
    if (this.email && this.password) {
      this.registrationService.sendLoginRequestToBackend(this.email, this.password).subscribe(
        (user: IUserDto) => {
          console.log(user);
          this.userService.setAuthorizedStatus();
          this.setUserData(user.userId);
        },
        error => {
          console.log(error);
        }
      );
    }
  }
  
  handleUserRegistration() : void {
    this.registrationService.goToRegistration();

    if (this.email && this.password && this.userTypeName)
    {
      this.registrationService.sendRegistrationRequestToBackend(this.email, this.password, this.userTypeName).subscribe(
        (user: IUserDto) => {
          console.log(user);
          this.userService.setAuthorizedStatus();
          this.setUserData(user.userId);
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  setUserData(userId: number) {
    this.userService.userId = userId;
  }

  public onBgClick(event: any) {
    if (!event.target.classList.contains('registration-form')) { 
      this.closeAuthWindow();
    } 
  }

  closeAuthWindow(): void {
      this.registrationService.closeAuthWindow();
   }

  onEmailChange(event: any): void {
    this.email = event.target.value;
  }

  onPasswordChange(event: any): void {
    this.password = event.target.value;
  }
}
