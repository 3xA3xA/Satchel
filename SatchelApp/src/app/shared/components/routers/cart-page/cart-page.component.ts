import { Component } from '@angular/core';
import { CartPageService } from 'src/app/core/services/cart-page.service';
import { UserService } from 'src/app/core/services/user.service';
import { Product } from '../catalog/catalog.component';
import { ProductService } from 'src/app/core/services/product.service';
import { Router} from '@angular/router';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent {

  constructor(private cartPageService: CartPageService, private userService: UserService, private productService: ProductService, private router: Router) { }

  shoppingCart : Product[] = [];
  finalPrice: number = 0

  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);
    this.cartPageService.GetShoppingCart(this.userService.userId).subscribe(
      (productsFromQuery: Product[]) => {
        this.shoppingCart = productsFromQuery;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }


  // ты его нигде не использовал :)
  sumPriceCart(){
    for(let product of this.shoppingCart){
      this.finalPrice += product.price
    }
  }

  getFormatPrice(price: number){
    return (this.productService.getFormattedPrice(price))
  }
}
