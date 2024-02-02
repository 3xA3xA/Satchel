import { Component, Input } from '@angular/core';
import { CreateService} from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';
import { ProductType, Brand, ConfigService, ProductDto } from 'src/app/core/services/config.service';
import { ProductService } from 'src/app/core/services/product.service';



@Component({
  selector: 'app-create-window',
  templateUrl: './create-window.component.html',
  styleUrls: ['./create-window.component.css']
})

export class CreateWindowComponent {

  // массивы нужны для хранения списка фотографий продукта, брендов и типов продукта.
  imageUrls = [this.configeService.PATHS.defaultProductImage];
  productTypes : ProductType[] = [];
  brandTypes : Brand[] = [];
  sizeByProductTypes : any[] = []; // Временно :))))

  @Input() newProduct: ProductDto = {  
    name: '',
    description: '',
    productTypeId: 0,
    price: 0,
    brandTypeId: 0,
    genderTypeId: 0,
    sizeIds: []
  }

  constructor(private createService: CreateService,
              private configeService: ConfigService,
              private productService: ProductService) { }

  @ViewChild('fileInput') fileInput = new ElementRef(null) ;

  ngOnInit() {
    this.getBrandTypes();
    this.getProductTypes();   
  }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files.length) {
      this.imageUrls = [] //обнулил предыдущие загруженные фотографии
      for (let i = 0; i < event.target.files.length; i++) {
        const file = event.target.files[i];
        let reader = new FileReader();
    
        reader.onload = (event: any) => {
          this.imageUrls.push(event.target.result);
        }
        console.log(this.imageUrls)
        reader.readAsDataURL(file);
      }
    }
  }

  onBgClick(event: any) {
    if (!event.target.classList.contains('create-form')) { 
      this.createService.setCreateWindowStatus()
    } 
  }

  addNewProduct() : void {
    this.productService.addNewProduct(this.newProduct, this.imageUrls).subscribe
      (data => {
        console.log(data);
      }, error => {
        console.error(error);
      });
    }

  getBrandTypes() {
    this.createService.getBrandTypes().subscribe(
      (data: any) => {
        this.brandTypes = data;
        console.log(data)
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  getProductTypes() {
    this.createService.getProductTypes().subscribe(
      (data: any) => {
        this.productTypes = data;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

}
