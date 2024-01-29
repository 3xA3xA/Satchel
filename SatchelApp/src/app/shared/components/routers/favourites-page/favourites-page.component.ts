import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../catalog/catalog.component';
import { FavouriteService } from 'src/app/core/services/favourite.service';
import { UserService } from 'src/app/core/services/user.service';
import { ConfigService } from 'src/app/core/services/config.service';

@Component({
  selector: 'app-favourites-page',
  templateUrl: './favourites-page.component.html',
  styleUrls: ['./favourites-page.component.css']
})
export class FavouritesPageComponent {

  constructor(private router: Router, 
              private favouriteService: FavouriteService, 
              private userService: UserService, 
              private configService: ConfigService) { }

  favouriteProducts : Product[] = [];
  
  inactiveStar = this.configService.PATHS.inactiveStar;
  activeStar = this.configService.PATHS.activeStar;

  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);
    
    this.getFavourites();
  }

  isFavourite(product: Product): boolean {
    return this.favouriteProducts.some(favProduct => favProduct.productId === product.productId);
  }

  public addToFavourite(product: Product, star: HTMLImageElement) {
    if(star.src.includes('activeFavourite'))
    {
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
  
  getFavourites() : void {
    this.favouriteService.GetFavourites(this.userService.userId).subscribe(
      (productsFromQuery: Product[]) => {
        this.favouriteProducts = productsFromQuery;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  getFormattedPrice(price: number) {
    return `${this.configService.getFormattedPrice(price)}`; 
  }
}
