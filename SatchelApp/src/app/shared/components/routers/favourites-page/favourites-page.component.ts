import { Component, Input, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Product } from '../catalog/catalog.component';
import { FavouriteService } from 'src/app/core/services/favourite.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-favourites-page',
  templateUrl: './favourites-page.component.html',
  styleUrls: ['./favourites-page.component.css']
})
export class FavouritesPageComponent {

  constructor(private router: Router, private favouriteService: FavouriteService, private userService: UserService, private route: ActivatedRoute) { }

  favouriteProducts : Product[] = [];
  
  inactiveStar = '../../../../../assets/images/icons/favourites.svg'
  activeStar = '../../../../../assets/images/icons/activeFavourite.svg'
  //starStatus = this.activeStar

  ngOnInit() {
    if (!this.userService.isAuthorized) //проверка, что пользователь авторизирован.
      this.router.navigate(['/']);
    this.favouriteService.GetFavourites(this.userService.userId).subscribe(
      (productsFromQuery: Product[]) => {
        this.favouriteProducts = productsFromQuery;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  isFavourite(product: Product): boolean {
    return this.favouriteProducts.some(favProduct => favProduct.productId === product.productId);
  }

  public addToFavourite(product: Product, star: HTMLImageElement) {
      star.src = this.inactiveStar;
      this.favouriteService.DeleteProductFromFavourites(product.productId, this.userService.userId);
  }

  goToProduct(id: number) {
    this.router.navigate(['catalog/product', id]);
  }

  formatNumber(num: number) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
  }
  
  getFormattedPrice(price: number) {
    return `${this.formatNumber(price)} ₽`; 
  }
}
