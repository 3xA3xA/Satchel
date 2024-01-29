import { Component } from '@angular/core';
import { CreateService} from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';
import { ProductType, Brand, ConfigService } from 'src/app/core/services/config.service';



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

  constructor(private createService: CreateService,
              private configeService: ConfigService) { }

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
    //еще не написан
  }

  getBrandTypes() {
    this.createService.getBrandTypes().subscribe(
      (data: any) => {
        this.brandTypes = data;
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
