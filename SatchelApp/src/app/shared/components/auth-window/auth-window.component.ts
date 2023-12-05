import { Component } from '@angular/core';
import { AuthorizationService } from 'src/app/core/services/authorization.service';

@Component({
  selector: 'app-auth-window',
  templateUrl: './auth-window.component.html',
  styleUrls: ['./auth-window.component.css']
})
export class AuthWindowComponent {

  constructor(private registrationService: AuthorizationService) { }

  closeAuthWindow( ): void {
      this.registrationService.closeAuthWindow();
   }
}
