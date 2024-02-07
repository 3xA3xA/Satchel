import { Component } from '@angular/core';
import { CartPageService } from 'src/app/core/services/cart-page.service';
import { UserService } from 'src/app/core/services/user.service';
import { Product } from '../catalog/catalog.component';
import { Router} from '@angular/router';
import { ConfigService } from 'src/app/core/services/config.service';

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

  ngOnInit() {
    this.checkAuthorization();
    //ngOnInit будет прерван, если chechAuth не пройдет (интересный факт)
    this.getShoppingCart(); 
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
        this.handleError('Error fetching products', error);
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
        this.getShoppingCart(); //получение нового списка товаров
      },
      (error) => {
        this.handleError('Error deleting product', error);
      }
    );
  }

  addToOrder() {
    this.cartPageService.AddToOrder(this.userService.userId, this.paymentTypeId, 1).subscribe(
      () => {
        this.deleteAllProductsFromShoppingCart();
        this.getShoppingCart(); //получение нового списка товаров
      },
      (error) => {
        this.handleError('Error deleting product', error);
      }
    );
  }

  deleteAllProductsFromShoppingCart(){
    this.cartPageService.DeleteAllProductsFromShoppingCart(this.userService.userId).subscribe(
      () => {
      },
      (error) => {
        this.handleError('Error deleting product', error);
      }
    );
  }

  getFormatPrice(price: number){
    return (this.configService.getFormattedPrice(price));
  }

  handleError(message: string, error: any) {
    console.error(message, error); // тут можно расписать полную логику при ошибке
  }
}
