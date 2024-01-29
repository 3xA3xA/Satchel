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
  filters: Filters = {};
  productType = '';
  selectedGender: number | null = null;
  genderType = [1, 2 ,3];
  priceRangeStep = 0;

  ngOnInit() {
    this.route.params.subscribe(params => { 
      this.productType = params['item']; 
      this.updateProducts();   
    });
    this.updateFavourites();
  }

  isFavourite(product: Product): boolean {
    return this.favouriteProducts.some(favProduct => favProduct.productId === product.productId);
  }

  togglePriceFilter() {

    this.priceRangeStep++;
  
    if (this.priceRangeStep === 1) {
      this.filters.isFilterByDecreasePrice = true;
      delete this.filters.isFilterByIncreasePrice;
    }

    if (this.priceRangeStep === 2) {
      this.filters.isFilterByIncreasePrice = true;
      delete this.filters.isFilterByDecreasePrice;
    }
  
    if(this.priceRangeStep === 3) {
      this.priceRangeStep = 0
      delete this.filters.isFilterByIncreasePrice;
      delete this.filters.isFilterByDecreasePrice;
    }
    this.updateProducts();
  }

  toggleFavourite(product: Product, star: HTMLImageElement) {
    if(!this.userService.isAuthorized){
      this.registrationService.setAuthWindowStatus();
      return;
    }     
  
    const isActive = star.src.includes('activeFavourite');
    star.src = isActive ? this.configService.PATHS.inactiveStar : this.configService.PATHS.activeStar;
    const action = isActive ? this.favouriteService.DeleteProductFromFavourites.bind(this.favouriteService) : this.favouriteService.AddFavouriteProduct.bind(this.favouriteService);
  
    action(product.productId, this.userService.userId).subscribe(() => this.updateFavourites(), console.log);
  }

  updateFavourites() {
    if(this.userService.isAuthorized) { 
      this.favouriteService.GetFavourites(this.userService.userId).subscribe(
        products => this.favouriteProducts = products,
        console.error
      );
    } 
  }

  updateProducts() {
    if (Object.keys(this.filters).length) {
      this.productService.getFilteredProducts(this.filters, this.productType).subscribe(
        products => this.products = products,
        console.error
      );
    } else {
      this.productService.getAllProducts(this.productType).subscribe(
        products => this.products = products,
        console.error
      );
    }
  }

  toggleGenderFilter(gender: number) {
    if (gender === this.selectedGender) {
      this.selectedGender = 0;
      delete this.filters.filterByGender;
    } else {
      this.selectedGender = gender;
      this.filters.filterByGender = this.selectedGender;
    }
    this.updateProducts();
  }

  clearFilters(){
    this.selectedGender = 0;
    this.priceRangeStep = 0;
    this.filters = {};
    this.updateProducts();
  }

  goToProduct(id: number) {
    this.router.navigate(['catalog/product', id]);
  }

  getFormatPrice(price: number){
    return (this.configService.getFormattedPrice(price))
  }

  getActiveStarPath(): string {
    return this.configService.PATHS.activeStar;
  }
  
  getInactiveStarPath(): string {
    return this.configService.PATHS.inactiveStar;
  }
}
