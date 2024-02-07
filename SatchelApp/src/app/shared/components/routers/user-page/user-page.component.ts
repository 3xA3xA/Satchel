import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService, UserPageData } from 'src/app/core/services/user.service';
import { Router } from '@angular/router';
import { CreateService } from 'src/app/core/services/create.service';
import { ConfigService, PaymentType, Order } from 'src/app/core/services/config.service';
import { Product } from '../catalog/catalog.component';
import { ProductService } from 'src/app/core/services/product.service';
import { CartPageService } from 'src/app/core/services/cart-page.service';

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
              private configService: ConfigService,
              private cartPageService: CartPageService) { }

  defaultUserPhoto: string = this.configService.PATHS.defaultUserPhoto;

  userData: UserPageData = this.initializeUserData();
  userInfoForm = this.initializeUserInfoForm(this.userData);
  sellerProducts: Product[] = [];
  orders: Order[] = [];
  paymentTypes: PaymentType[] = [];
  statusMsg = '';

  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);

    this.getUserData();
    this.getSellerProducts();
    this.getPaymentTypes();
    this.getOrders();
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

  getSellerProducts(simplifiedData: boolean = true){
    this.productService.getSellerProducts(this.userService.userId).subscribe(
      (data: Product[]) => {
        simplifiedData && data.length > 3 ? this.sellerProducts = data.slice(0, 3) :  this.sellerProducts = data;
      },
      (error) => {
        console.log(error);   
      }
    )
  }

  updateUserInfo() {
    this.userService.updateUserInfo(this.userData).subscribe(
      (data: UserPageData) => {
        this.statusMsg = 'Данные обновлены';
      },
      (error) => {
        this.statusMsg = 'Что-то пошло не так!';
      }
    );
    
    setTimeout(() => {
      this.statusMsg = ''
    }, 2000)
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
        this.userData.dateOfBirth = data.dateOfBirth.toString().slice(0, 10);
        this.updateUserInfoForm();
        if (this.userData.userPhotoSrc == null)
          this.userData.userPhotoSrc = this.defaultUserPhoto;
      },
      (error) => {
        this.errorMsg()
      }
    );
  }

  getOrders() {
    this.cartPageService.GetOrders(this.userService.userId).subscribe(
      (data: Order[]) => {
        this.orders = data;
        console.log(this.orders)
      },
      (error) => {
        this.errorMsg()
      }
    );
  }

  getPaymentTypes() {
    this.userService.getPaymentTypes().subscribe(
      (data: PaymentType[]) => {
        this.paymentTypes = data;
      },
      (error) => {
        this.errorMsg()
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

  showMoreProducts() {
    this.getSellerProducts(false)
  }

  deleteSellerProduct(productId: number) {
    this.productService.deleteSellerProduct(productId).subscribe(
      (data: any) => {
        this.getSellerProducts(false);
      },
      (error) => {
        this.errorMsg()
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
      dateOfBirth: '',
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

  errorMsg(){
    this.statusMsg = 'Что-то пошло не так!';

    setTimeout(() => {
      this.statusMsg = ''
    }, 2000)
  }
}
