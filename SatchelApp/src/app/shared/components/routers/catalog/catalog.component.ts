import { Component, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService, Filters } from 'src/app/core/services/product.service';
import { FavouriteService } from 'src/app/core/services/favourite.service';
import { UserService } from 'src/app/core/services/user.service';
import { AuthorizationService } from 'src/app/core/services/authorization.service';
import { ConfigService } from 'src/app/core/services/config.service';

// не получилось вынести его в service
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
  genderTypeId: number,
  sizeName?: string
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
              private registrationService: AuthorizationService,
              private configService: ConfigService) { }

  products: Product[] = [];
  favouriteProducts: Product[] = [];

  filters: Filters = {} ;
  productType: string = '';
  selectedGender: number | null = null;
  genderType = [1, 2 ,3]
  
  inactiveStar = this.configService.PATHS.inactiveStar;
  activeStar = this.configService.PATHS.activeStar;

  ngOnInit() {
    // получение типа продуктов из роута
    this.route.params.subscribe(params => { 
      this.productType = params['item']; 
      this.getAllProducts();   
    });

    this.getFavourites();
  }

  isFavourite(product: Product): boolean {
    return this.favouriteProducts.some(favProduct => favProduct.productId === product.productId);
  }

  addToFavourite(product: Product, star: HTMLImageElement) {
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

  getFavourites() : void {
    if(this.userService.isAuthorized) { 
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

  getAllProducts() : void {
    this.productService.getAllProducts(this.productType).subscribe(
      (productsFromQuery: Product[]) => {
        this.products = productsFromQuery;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
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
    return (this.configService.getFormattedPrice(price))
  }

}
