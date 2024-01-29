import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService, UserPageData } from 'src/app/core/services/user.service';
import { Router } from '@angular/router';
import { CreateService } from 'src/app/core/services/create.service';
import { ConfigService } from 'src/app/core/services/config.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent {
  
  constructor(private userService: UserService, 
              private createService: CreateService, 
              private router: Router,
              private configService: ConfigService) { }

  defaultUserPhoto: string = this.configService.PATHS.defaultUserPhoto;

  userData: UserPageData = this.initializeUserData();
  userInfoForm = this.initializeUserInfoForm(this.userData);

  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);

    this.getUserData();
  }

  get isCreateProductWindowOpen(): boolean {
    return this.createService.isCreateOpen;
  }

  updateUserInfo() {
    this.userService.updateUserInfo(this.userData).subscribe(
      (data: UserPageData) => {
        //получить ответ
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  openCreateWindow(){
    this.createService.setCreateWindowStatus();
  }

  exitFromAccount(){
    this.userService.exitFromAccount();
    localStorage.clear();
    this.router.navigate(['/']);
  }

  getUserData() : void {
    this.userService.getUserData().subscribe(
      (data: UserPageData) => {
        this.userData = data;
        this.updateUserInfoForm();
        if (this.userData.userPhotoSrc == null)
          this.userData.userPhotoSrc = this.defaultUserPhoto;
      },
      (error) => {
        this.handleError('Error fetching user data', error);
      }
    );
  }

  onFileSelected(event : any) {
    const file:File = event.target.files[0];

    if (file) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
            this.userData.userPhotoSrc = e.target.result;
        }
        reader.readAsDataURL(file);
    }
  }

  updateUserInfoForm() {
    this.userInfoForm.patchValue({
      firstName: this.userData.firstName,
      middleName: this.userData.middleName,
      lastName: this.userData.lastName,
      email: this.userData.email,
      birth: this.userData.dateOfBirth,
      userPhoto: this.userData.userPhotoSrc
    });
  }

  initializeUserData(): UserPageData {
    return {
      firstName: '',
      middleName: '',
      lastName: '',
      email: '',
      dateOfBirth: new Date(1990, 0, 1),
      userPhotoSrc: '',
      userType: 'Покупатель'
    };
  }
  
  initializeUserInfoForm(data: UserPageData): FormGroup {
    return new FormGroup({
      firstName: new FormControl(data.firstName),
      middleName: new FormControl(data.middleName),
      lastName: new FormControl(data.lastName),
      email: new FormControl(data.email),
      birth: new FormControl(data.dateOfBirth),
      userPhoto: new FormControl(data.userPhotoSrc)
    });
  }

  handleError(message: string, error: any) {
    console.error(message, error);
  }

}
