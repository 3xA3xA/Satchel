import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from 'src/app/core/services/user.service';
import { IUserPageData } from 'src/app/core/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent {
  constructor(private userService: UserService, private router: Router) { }

  defaultUserPhoto: string = 'https://img.freepik.com/free-vector/illustration-businessman_53876-5856.jpg?size=626&ext=jpg&ga=GA1.1.1826414947.1705190400&semt=ais';

  userData: IUserPageData = {
    firstName: '',
    middleName: '',
    lastName: '',
    email: '',
    dateOfBirth: '',
    userPhotoSrc: '',
  }

  userInfoForm = new FormGroup({
    firstName: new FormControl(''),
    middleName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('')
  });

  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);
    this.userService.getUserData().subscribe(
      (data: IUserPageData) => {
        this.userData = data;
        this.getUserInfo()
        if (this.userData.userPhotoSrc == null)
          this.userData.userPhotoSrc = this.defaultUserPhoto;
        console.log(this.userData)
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  getUserInfo(){
    const form = this.userInfoForm;
    const firstName = form.get('firstName');
    const middleName = form.get('middleName');
    const lastName = form.get('lastName');
    const email = form.get('email')


    console.log(this.userData.email, email)
    // firstName?.setValue(this.userData.firstName)
    // middleName?.setValue(this.userData.middleName)
    // lastName?.setValue(this.userData.lastName)
    email?.setValue(this.userData.email)
  }
}
