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

  defaultUserPhoto: string = 'https://img.freepik.com/free-vector/illustration-businessman_53876-5856.jpg?size=626&ext=jpg&ga=GA1.1.1826414947.1705190400&semt=ais';

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
        if (this.userData.userPhotoSrc == null)
          this.userData.userPhotoSrc = this.defaultUserPhoto;
        console.log(this.userData)
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }
}
