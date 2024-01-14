import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/core/services/product.service';
import { Product } from '../catalog/catalog.component';
import { CartPageService } from 'src/app/core/services/cart-page.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  id = 0;

  selectedSize: string | null = null;

  onSizeClick(size: string) {
    if (size === this.selectedSize) {
      this.selectedSize = null;  
    } else {
      this.selectedSize = size;
    }
  }

  addProductToShoppingCart(){
    if (this.selectedSize != null && this.userService.userId != 0){
      this.cartPageService.AddProductToShoppingCart(this.id, this.userService.userId);
    }
  }

  constructor( private route: ActivatedRoute, private productService: ProductService, private cartPageService: CartPageService, private userService: UserService) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.id = id ? +id : 0;

    this.productService.getProductById(this.id).subscribe(
      (product) => {
        this.product = product;
        console.log(product.sizes)
      },
      (error) => {
        console.error('Error fetching product by ID:', error);
      }
    );
  }

  @Input() product: Product = {
    productId: 0,   
    name: '',
    description: '',
    producrCategoryId: 0,
    productTypeId: 0,
    price: 0,
    images: [''],
    sizes: [''],
    brandTypeId: 0,
    genderTypeId: 0
  }
}
