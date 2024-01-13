import { Component, Input, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/core/services/product.service';
import { Product } from '../catalog/catalog.component';

@Component({
  selector: 'app-favourites-page',
  templateUrl: './favourites-page.component.html',
  styleUrls: ['./favourites-page.component.css']
})
export class FavouritesPageComponent {

  constructor(private router: Router, private productService: ProductService, private route: ActivatedRoute) { }

  favouriteProducts : Product[] = [];
  
  inactiveStar = '../../../../../assets/images/icons/favourites.svg'
  activeStar = '../../../../../assets/images/icons/activeFavourite.svg'
  starStatus = this.inactiveStar
  isActive = false;

  ngOnInit() {
    this.route.params.subscribe(params => {
      let productType : string = params['item'];
      this.productService.getAllProducts(productType).subscribe(
        (productsFromQuery: Product[]) => {
          this.favouriteProducts = productsFromQuery;
        },
        (error) => {
          console.error('Error fetching products', error);
        }
      );
    });
  }

  addToFavourite(product: Product, star: HTMLImageElement){
    if(star.src.includes('activeFavourite')){
      star.src = this.inactiveStar;
    }
    else star.src = this.activeStar
    
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
