import { Component } from '@angular/core';
import { AuthorizationService } from 'src/app/core/services/authorization.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(private registrationService: AuthorizationService) { }

  openAuthWindow() {
    this.registrationService.openAuthWindow();
  }
}
