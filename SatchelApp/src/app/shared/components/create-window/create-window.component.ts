import { Component } from '@angular/core';
import { CreateService, Brand, ProductType} from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';
import { Product } from '../routers/catalog/catalog.component';



@Component({
  selector: 'app-create-window',
  templateUrl: './create-window.component.html',
  styleUrls: ['./create-window.component.css']
})

export class CreateWindowComponent {
  imageUrls = ['https://www.fivebranches.edu/wp-content/uploads/2021/08/default-image.jpg'];
  // массивы нужны для хранения списка брендов и типов продукта.
  productTypes : ProductType[] = [];
  brandTypes : Brand[] = [];

  constructor(private createService: CreateService) { }

  @ViewChild('fileInput') fileInput = new ElementRef(null) ;

  ngOnInit() {
    this.createService.getBrandTypes().subscribe(
      (data: any) => {
        this.brandTypes = data;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );

    this.createService.getProductTypes().subscribe(
      (data: any) => {
        this.productTypes = data;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
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

  public onBgClick(event: any) {
    if (!event.target.classList.contains('create-form')) { 
      this.closeCreateWindow();
    } 
  }

  addNewProduct(){
    
  }

  closeCreateWindow(): void {
    this.createService.setCreateWindowStatus();
  }
}
