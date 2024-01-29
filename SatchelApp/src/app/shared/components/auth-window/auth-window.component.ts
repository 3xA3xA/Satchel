import { Component } from '@angular/core';
import { AuthorizationService} from 'src/app/core/services/authorization.service';
import { UserService } from 'src/app/core/services/user.service';
import { UserDto, ConfigService } from 'src/app/core/services/config.service';

@Component({
  selector: 'app-auth-window',
  templateUrl: './auth-window.component.html',
  styleUrls: ['./auth-window.component.css']
})

export class AuthWindowComponent {

  constructor(private registrationService: AuthorizationService, 
    private userService: UserService,
    private configService: ConfigService) { }

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

  updateStep() {
    this.step$ = this.registrationService.step; 
    this.isLoginVisible = false; //Кнопка входа 
    this.isSwitchVisible = true;
    console.log(this.isLoginVisible)
  }

  updateUserTypeName(isChecked: boolean) {
    if (isChecked === false)
      this.userTypeName = 'Продавец'
    else
      this.userTypeName = 'Покупатель'
  }

  handleUserLogin(): void {
    if (this.email && this.password)
      this.loginUser();
    else {
      // написать оповещение, что не все поля заполнены
    }
  }
  
  handleUserRegistration() : void {
    if (this.email && this.password && this.userTypeName && !this.isLoginVisible) //дописал проверку, что кнопка логина должна быть скрыта
      this.addNewUser();
    else
      this.registrationService.goToRegistration();
  }

  loginUser() : void {
    this.registrationService.sendLoginRequestToBackend(this.email, this.password).subscribe(
      (user: UserDto) => {
        this.userService.setAuthorizedStatus();
        this.setUserData(user.userId);
        localStorage.setItem('userId', user.userId.toString());
      },
      error => {
        console.log(error);
      }
    );
  }

  addNewUser() : void {
    this.registrationService.sendRegistrationRequestToBackend(this.email, this.password, this.userTypeName).subscribe(
      (user: UserDto) => {
        this.userService.setAuthorizedStatus();
        this.setUserData(user.userId);
        localStorage.setItem('userId', user.userId.toString());
      },
      error => {
        console.log(error);
      }
    );
  }

  setUserData(userId: number) {
    this.userService.userId = userId;
  }

  onBgClick(event: any) {
    this.configService.onBgClick(event, 'registration-form')
  }

  onEmailChange(event: any): void {
    this.email = event.target.value;
  }

  onPasswordChange(event: any): void {
    this.password = event.target.value;
  }
}
