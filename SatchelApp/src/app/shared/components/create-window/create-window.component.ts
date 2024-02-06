import { Component, Input } from '@angular/core';
import { CreateService} from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';
import { ProductType, Brand, ConfigService, ProductDto, SizeType } from 'src/app/core/services/config.service';
import { ProductService } from 'src/app/core/services/product.service';
import { UserService } from 'src/app/core/services/user.service';



@Component({
  selector: 'app-create-window',
  templateUrl: './create-window.component.html',
  styleUrls: ['./create-window.component.css']
})

export class CreateWindowComponent {

  // массивы нужны для хранения списка фотографий продукта, брендов и типов продукта.
  productTypes : ProductType[] = [];
  brandTypes : Brand[] = [];
  sizeByProductTypes : SizeType[] = []; // Временно :))))

  @Input() newProduct: ProductDto = {  
    name: '',
    description: '',
    productTypeId: 0,
    price: 0,
    brandTypeId: 0,
    genderTypeId: 0,
    userId: this.userService.userId,
    sizeTypeIds: [],
    images: [this.configeService.PATHS.defaultProductImage]
  }

  constructor(private createService: CreateService,
              private configeService: ConfigService,
              private productService: ProductService,
              private userService: UserService) { }

  @ViewChild('fileInput') fileInput = new ElementRef(null) ;

  ngOnInit() {
    this.getBrandTypes();
    this.getProductTypes();   
    this.GetSizes();
  }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files.length) {
      this.newProduct.images = [] //обнулил предыдущие загруженные фотографии
      for (let i = 0; i < event.target.files.length; i++) {
        const file = event.target.files[i];
        let reader = new FileReader();
    
        reader.onload = (event: any) => {
          this.newProduct.images.push(event.target.result);
        }
        console.log(this.newProduct.images)
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
    this.productService.addNewProduct(this.newProduct).subscribe
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

  GetSizes() {
    this.createService.getSizesByProductType('clothes').subscribe( //fix later
      (data: any) => {
        this.sizeByProductTypes = data;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

}
