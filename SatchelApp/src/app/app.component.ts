import { Component } from '@angular/core';
import { AuthorizationService } from './core/services/authorization.service';
import { UserService } from './core/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DatabaseProject';

  constructor(private authService: AuthorizationService, private userService: UserService) { }

  ngOnInit(){
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.userService.setAuthorizedStatus();
      this.userService.userId = Number(userId);
    }
  }

  //тут есть пиздец, что 4-5 раз отрабатывает, хелп кто может. жижа
  get isRegistrationOpen(): boolean {
    return this.authService.isRegistrationOpen;
  }
}