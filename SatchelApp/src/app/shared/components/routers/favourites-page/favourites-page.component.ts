import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../catalog/catalog.component';
import { FavouriteService } from 'src/app/core/services/favourite.service';
import { UserService } from 'src/app/core/services/user.service';
import { ConfigService } from 'src/app/core/services/config.service';
import { AuthorizationService } from 'src/app/core/services/authorization.service';

@Component({
  selector: 'app-favourites-page',
  templateUrl: './favourites-page.component.html',
  styleUrls: ['./favourites-page.component.css']
})
export class FavouritesPageComponent {

  constructor(private router: Router, 
              private favouriteService: FavouriteService, 
              private userService: UserService, 
              private configService: ConfigService,
              private authorizationService: AuthorizationService) { }

  favouriteProducts : Product[] = [];
  
  ngOnInit() {
    if (!this.userService.isAuthorized)
      this.router.navigate(['/']);
    
    this.getFavourites();
  }

  isFavourite(product: Product): boolean {
    return this.favouriteProducts.some(favProduct => favProduct.productId === product.productId);
  }

  goToProduct(id: number) {
    this.router.navigate(['catalog/product', id]);
  }
  
  getFavourites() : void {
    this.favouriteService.GetFavourites(this.userService.userId).subscribe(
      products => this.favouriteProducts = products,
      console.error
    );
  }

  toggleFavourite(product: Product, star: HTMLImageElement) {  
    const isActive = star.src.includes('activeFavourite');
    star.src = isActive ? this.configService.PATHS.inactiveStar : this.configService.PATHS.activeStar;
    const action = isActive ? this.favouriteService.DeleteProductFromFavourites.bind(this.favouriteService) : this.favouriteService.AddFavouriteProduct.bind(this.favouriteService);
  
    action(product.productId, this.userService.userId).subscribe(() => this.getFavourites(), console.log);
  }

  getFormattedPrice(price: number) {
    return `${this.configService.getFormattedPrice(price)}`; 
  }

  getActiveStarPath(): string {
    return this.configService.PATHS.activeStar;
  }
  
  getInactiveStarPath(): string {
    return this.configService.PATHS.inactiveStar;
  }
}
