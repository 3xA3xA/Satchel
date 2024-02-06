import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService, UserPageData } from 'src/app/core/services/user.service';
import { Router } from '@angular/router';
import { CreateService } from 'src/app/core/services/create.service';
import { ConfigService } from 'src/app/core/services/config.service';
import { Product } from '../catalog/catalog.component';
import { ProductService } from 'src/app/core/services/product.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent {
  
  constructor(private userService: UserService, 
              private productService: ProductService,
              private createService: CreateService, 
              private router: Router,
              private configService: ConfigService) { }

  defaultUserPhoto: string = this.configService.PATHS.defaultUserPhoto;

  userData: UserPageData = this.initializeUserData();
  userInfoForm = this.initializeUserInfoForm(this.userData);
  sellerProducts: Product[] = [];

  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);

    this.getUserData();

    this.getSellerProducts();
  }

  get isCreateProductWindowOpen(): boolean {
    return this.createService.isCreateOpen;
  }

  getFormattedPrice(price: number) {
    return `${this.configService.getFormattedPrice(price)}`; 
  }

  goToProduct(id: number) {
    this.router.navigate(['catalog/product', id]);
  }

  getSellerProducts(){
    this.productService.getSellerProducts(this.userService.userId).subscribe(
      (data: Product[]) => {
        this.sellerProducts = data;
        console.log(data)
      },
      (error) => {

      }
    )
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

  deleteSellerProduct(productId: number) {
    this.productService.deleteSellerProduct(productId).subscribe(
      (data: any) => {
        this.sellerProducts = data;
        console.log(data) //verim верим
      },
      (error) => {
        this.handleError('Error fetching user data', error);
      }
    );
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
