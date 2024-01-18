import { Component, Input, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/core/services/product.service';
import { FavouriteService } from 'src/app/core/services/favourite.service';
import { UserService } from 'src/app/core/services/user.service';

export interface Product{
  productId: number,
  name: string,
  description: string,
  producrCategoryId: number,
  productTypeId: number,
  price: number,
  images: string[],
  sizes: string[]
  brandTypeId: number,
  genderTypeId: number
}

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit{

  constructor(private router: Router, private productService: ProductService, private favouriteService: FavouriteService, private userService: UserService, private route: ActivatedRoute) { }

  products : Product[] = [];
  
  inactiveStar = '../../../../../assets/images/icons/favourites.svg'
  activeStar = '../../../../../assets/images/icons/activeFavourite.svg'
  starStatus = this.inactiveStar;

  ngOnInit() {
    this.route.params.subscribe(params => {
      let productType : string = params['item'];
      this.productService.getAllProducts(productType).subscribe(
        (productsFromQuery: Product[]) => {
          console.log(productsFromQuery)
          this.products = productsFromQuery;
        },
        (error) => {
          console.error('Error fetching products', error);
        }
      );
    });
  }

  public addToFavourite(product: Product, star: HTMLImageElement) {
    if (!this.userService.isAuthorized)
      return;
    
    if(star.src.includes('activeFavourite')){
      star.src = this.inactiveStar;
      this.favouriteService.DeleteProductFromFavourites(product.productId, this.userService.userId);
    }
    else {
      star.src = this.activeStar;
      this.favouriteService.AddFavouriteProduct(product.productId, this.userService.userId);
    }
    
  }

  goToProduct(id: number) {
    this.router.navigate(['catalog/product', id]);
  }

  getFormatPrice(price: number){
    return (this.productService.getFormattedPrice(price))
  }

}
