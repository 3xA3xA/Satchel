import { Component } from '@angular/core';
import { AuthorizationService, Step } from 'src/app/core/services/authorization.service';
import { switchMap, Subject } from 'rxjs';

@Component({
  selector: 'app-auth-window',
  templateUrl: './auth-window.component.html',
  styleUrls: ['./auth-window.component.css']
})
export class AuthWindowComponent {

  title = 'Что-то не так'

  step$ = this.registrationService.step;

  ngOnInit() {
    this.registrationService.stepChange.subscribe(() => {
      this.updateStep(); 
    });

  }  

  constructor(private registrationService: AuthorizationService) { }

  updateStep() {
    this.step$ = this.registrationService.step; 
  }

  nextStep(): void {
    this.registrationService.next()
  }

  prevStep(): void {
    this.registrationService.prev()
  }

  closeAuthWindow( ): void {
      this.registrationService.closeAuthWindow();
   }
}
