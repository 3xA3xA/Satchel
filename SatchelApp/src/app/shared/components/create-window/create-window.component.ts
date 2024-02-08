import { Component, Input } from '@angular/core';
import { CreateService} from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';
import { ProductType, Brand, ConfigService, AddProductBody, SizeType, GenderType } from 'src/app/core/services/config.service';
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
  sizeByProductTypes : SizeType[] = [];
  genderTypes: GenderType[] = [];
  statusMsg = ''
  newProductImages: string[] = [this.configeService.PATHS.defaultProductImage]

  @Input() newProduct: AddProductBody = {  
    addProductDto: {
      name: '',
      description: '',
      productTypeId: 0,
      price: 0,
      brandTypeId: 0,
      genderTypeId: 0,
      userId: this.userService.userId,
      sizeTypeIds: [],
    },
    images: []
  }

  constructor(private createService: CreateService,
              private configeService: ConfigService,
              private productService: ProductService,
              private userService: UserService) { }

  @ViewChild('fileInput') fileInput = new ElementRef(null) ;

  ngOnInit() {
    this.getBrandTypes();
    this.getProductTypes();   
    this.getGenderTypes();
  } 

  onFileSelected(event : any) {
    if (event.target.files && event.target.files.length) {
      let imageFilesArr: File[] = []
      this.newProductImages = [] //обнулил предыдущие загруженные фотографии
      for (let i = 0; i < event.target.files.length; i++) {
        imageFilesArr.push(event.target.files[i]);
        const file = event.target.files[i];
        let reader = new FileReader();

        reader.onload = (event: any) => {
          this.newProductImages.push(event.target.result);
        }
        reader.readAsDataURL(file);
      }

      this.newProduct.images = imageFilesArr;
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
        this.statusMsg = 'Товар поступил в продажу';

        setTimeout(() => {
          this.statusMsg = ''
          this.createService.setCreateWindowStatus();
        }, 2000)

        
      }, error => {
        this.errorMsg()
        console.log(error)
      });
    }

  getBrandTypes() {
    this.createService.getBrandTypes().subscribe(
      (data: any) => {
        this.brandTypes = data;
      },
      (error) => {
        this.errorMsg()
      }
    );
  }

  getProductTypes() {
    this.createService.getProductTypes().subscribe(
      (data: any) => {
        this.productTypes = data;
      },
      (error) => {
        this.errorMsg()
      }
    );
  }

  getSizes(productTypeName: string) {
    this.createService.getSizesByProductType(productTypeName).subscribe(
      (data: any) => {
        this.sizeByProductTypes = data;
      },
      (error) => {
        this.errorMsg()
      }
    );
  }

  getGenderTypes(){
    this.createService.getGenderTypes().subscribe(
      (data: GenderType[]) => {
        this.genderTypes = data;
      },
      (error) => {
        this.errorMsg()
      }
    );
  }


  getProductNameById(id: number): string {
    const product = this.productTypes.find(pt => pt.productTypeId === Number(id));
    return product ? product.name : '';
  }  

  errorMsg(){
    this.statusMsg = 'Что-то пошло не так!';
    setTimeout(() => {
      this.statusMsg = ''
    }, 2000)
  }
  
}
