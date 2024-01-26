import { Component, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/core/services/product.service';
import { FavouriteService } from 'src/app/core/services/favourite.service';
import { UserService } from 'src/app/core/services/user.service';
import { AuthorizationService } from 'src/app/core/services/authorization.service';

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

export interface Filters{
  filterByMinPrice?: number, 
  filterByMaxPrice?: number, 
  filterByGender?: number, 
  filterByName?: string, 
  isFilterByDecreasePrice?: boolean, 
  isFilterByIncreasePrice?: boolean
}

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit{

  constructor(private router: Router, 
              private productService: ProductService, 
              private favouriteService: FavouriteService, 
              private userService: UserService, 
              private route: ActivatedRoute,
              private registrationService: AuthorizationService) { }

  products: Product[] = [];
  favouriteProducts: Product[] = [];

  filters: Filters = {} ;
  productType: string = '';
  selectedGender: number | null = null;
  genderType = [1, 2 ,3]
  
  inactiveStar = '../../../../../assets/images/icons/favourites.svg'
  activeStar = '../../../../../assets/images/icons/activeFavourite.svg'

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.productType = params['item'];
      this.productService.getAllProducts(this.productType).subscribe(
        (productsFromQuery: Product[]) => {
          this.products = productsFromQuery;
        },
        (error) => {
          console.error('Error fetching products', error);
        }
      );
    });

    if(this.userService.isAuthorized){ //добавил подгрузку массива избранных, если вошли в аккаунт
      this.favouriteService.GetFavourites(this.userService.userId).subscribe(
        (productsFromQuery: Product[]) => {
          this.favouriteProducts = productsFromQuery;
        },
        (error) => {
          console.error('Error fetching products', error);
        }
      );
    } 
  }

  isFavourite(product: Product): boolean {
    return this.favouriteProducts.some(favProduct => favProduct.productId === product.productId);
  }

  public addToFavourite(product: Product, star: HTMLImageElement) {
    if(!this.userService.isAuthorized){
      this.registrationService.setAuthWindowStatus();
      return;
    }     
    
    if(star.src.includes('activeFavourite')){
      star.src = this.inactiveStar;
      this.favouriteService.DeleteProductFromFavourites(product.productId, this.userService.userId);
    }
    else {
      star.src = this.activeStar;
      this.favouriteService.AddFavouriteProduct(product.productId, this.userService.userId);
    }
  }

  onGenderClick(gender: number) {
    if (gender === this.selectedGender) {
      this.selectedGender = 0;
      delete this.filters.filterByGender

    } else {
      this.selectedGender = gender;
      this.filters.filterByGender = this.selectedGender
    }
  }

  clearFilters(){
    this.selectedGender = 0
    this.filters = {}
    this.getFilteredProducts()
  }

  getFilteredProducts(){

    this.productService.getFilteredProducts(this.filters, this.productType).subscribe(
      (productsFromQuery: Product[]) => {
        this.products = productsFromQuery;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  goToProduct(id: number) {
    this.router.navigate(['catalog/product', id]);
  }

  getFormatPrice(price: number){
    return (this.productService.getFormattedPrice(price))
  }

}
