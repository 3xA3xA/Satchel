import { Component, Input, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/core/services/product.service';

export interface Product{
  id: number,
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

  constructor(private router: Router, private productService: ProductService, private route: ActivatedRoute) { }

  products : Product[] = [];
  
  inactiveStar = '../../../../../assets/images/icons/favourites.svg'
  activeStar = '../../../../../assets/images/icons/activeFavourite.svg'
  starStatus = this.inactiveStar
  isActive = false;

  ngOnInit() {
    this.route.params.subscribe(params => {
      let productType : string = params['item'];
      this.productService.getAllProducts(productType).subscribe(
        (productsFromQuery: Product[]) => {
          this.products = productsFromQuery;
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
