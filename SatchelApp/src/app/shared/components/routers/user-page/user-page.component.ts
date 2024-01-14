import { Component } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { IUserPageData } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent {
  constructor(private userService: UserService) { }

  userData: IUserPageData = {
    firstName: '',
    middleName: '',
    lastName: '',
    email: '',
    dateOfBirth: '',
    userPhotoSrc: '',
  }

  ngOnInit() {
    this.userService.getUserData().subscribe(
      (data: IUserPageData) => {
        this.userData = data;
        console.log(this.userData);
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }
}
