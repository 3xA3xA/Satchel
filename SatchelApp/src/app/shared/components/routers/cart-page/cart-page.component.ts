import { Component } from '@angular/core';
import { CartPageService } from 'src/app/core/services/cart-page.service';
import { UserService } from 'src/app/core/services/user.service';
import { Product } from '../catalog/catalog.component';
import { Router} from '@angular/router';
import { ConfigService, PaymentType, ShippingType } from 'src/app/core/services/config.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent {

  constructor(private cartPageService: CartPageService, 
              private userService: UserService, 
              private router: Router,
              private configService: ConfigService) { }

  shoppingCart : Product[] = [];
  finalPrice: number = 0
  paymentTypeId: number = 0; 
  shippingTypeId: number = 0;
  paymentTypes: PaymentType[] = [];
  shippingTypes: ShippingType[] = [];
  statusMsg = '';

  ngOnInit() {
    this.checkAuthorization();
    //ngOnInit будет прерван, если chechAuth не пройдет (интересный факт)
    this.getShoppingCart(); 
    this.getPaymentTypes();
    this.getShippingTypes();
  }

  checkAuthorization() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);
  }

  getShoppingCart() {
    this.cartPageService.GetShoppingCart(this.userService.userId).subscribe(
      (productsFromQuery: Product[]) => {
        this.shoppingCart = productsFromQuery;
        this.sumPriceCart();
        console.log(this.shoppingCart)
      },
      (error) => {
        this.errorMsg();
      }
    );
  }

  sumPriceCart(){
    this.finalPrice = 0; //обнуление перед началом подсчёта
    for(let product of this.shoppingCart){
      this.finalPrice += product.price
    }
    return this.finalPrice
  }

  deleteProductFromProductCart(productId : number, sizeTypeId : number) {
    this.cartPageService.DeleteProductFromShoppingCart(productId, this.userService.userId, sizeTypeId).subscribe(
      () => {
      },
      (error) => {
        this.errorMsg();
      }
    );
  }

  addToOrder() {
    if(this.paymentTypeId == 0 || this.shippingTypeId == 0){
      this.errorMsg()
      return;
    }     

    this.cartPageService.AddToOrder(this.userService.userId, this.paymentTypeId, this.shippingTypeId).subscribe(
      () => {
        this.deleteAllProductsFromShoppingCart();
        this.ngOnInit(); // имба мув (движение)
      },
      (error) => {
        this.errorMsg();
      }
    );
  }

  deleteAllProductsFromShoppingCart(){
    this.cartPageService.DeleteAllProductsFromShoppingCart(this.userService.userId).subscribe(
      () => {
      },
      (error) => {
        this.errorMsg();
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

  getShippingTypes() {
    this.userService.getShippingTypes().subscribe(
      (data: ShippingType[]) => {
        this.shippingTypes = data;
      },
      (error) => {
        this.errorMsg()
      }
    );
  }

  getFormatPrice(price: number){
    return (this.configService.getFormattedPrice(price));
  }

  errorMsg(){
    this.statusMsg = 'Что-то пошло не так!';

    setTimeout(() => {
      this.statusMsg = ''
    }, 2000)
  }
}
