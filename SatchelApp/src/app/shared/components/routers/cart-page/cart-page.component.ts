import { Component } from '@angular/core';
import { CartPageService } from 'src/app/core/services/cart-page.service';
import { UserService } from 'src/app/core/services/user.service';
import { Product } from '../catalog/catalog.component';
import { ProductService } from 'src/app/core/services/product.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent {

  constructor(private cartPageService: CartPageService, private userService: UserService, private productService: ProductService) { }

  shoppingCart : Product[] = [];
  finalPrice: number = 0

  ngOnInit() {
    this.cartPageService.GetShoppingCart(this.userService.userId).subscribe(
      (productsFromQuery: Product[]) => {
        this.shoppingCart = productsFromQuery;
        console.log(productsFromQuery)
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }


  sumPriceCart(){
    for(let product of this.shoppingCart){
      this.finalPrice += product.price
    }
  }

  getFormatPrice(price: number){
    return (this.productService.getFormattedPrice(price))
  }
}
