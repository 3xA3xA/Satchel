import { Component } from '@angular/core';
import { AuthorizationService } from 'src/app/core/services/authorization.service';
import { ChangeDetectorRef } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(private changeDetector: ChangeDetectorRef ,private registrationService: AuthorizationService, private userService: UserService, private router: Router) { }

  isAuthorize = this.userService.isAuthorized

  openAuthWindow() {
    console.log(this.isAuthorize)
    if(this.userService.authorizedStatus){
      this.router.navigate(['/profile']);
    } else{
      this.registrationService.setAuthWindowStatus();
    }
  }

  routeToFavourites() {
    if(this.userService.authorizedStatus){
      this.router.navigate(['/favourites']);
    }
  }

  routeToShoppingCart() {
    if(this.userService.authorizedStatus){
      this.router.navigate(['/cart']);
    }
  }
}
