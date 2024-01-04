import { Component } from '@angular/core';
import { AuthorizationService } from 'src/app/core/services/authorization.service';
import { UserService } from 'src/app/core/services/user.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(private registrationService: AuthorizationService, private userService: UserService, private router: Router) { }

  openAuthWindow() {

    console.log(this.userService.isAuthorized)

    if(this.userService.authorizedStatus){
      this.router.navigate(['/profile']);
    } else{
      this.registrationService.setAuthWindowStatus();
    }
  }
}
