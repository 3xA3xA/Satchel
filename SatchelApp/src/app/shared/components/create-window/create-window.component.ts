import { Component } from '@angular/core';
import { CreateService } from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-create-window',
  templateUrl: './create-window.component.html',
  styleUrls: ['./create-window.component.css']
})
export class CreateWindowComponent {
  imageUrls = ['https://www.fivebranches.edu/wp-content/uploads/2021/08/default-image.jpg'];
  // массивы нужны для хранения списка брендов и типов продукта.
  productTypes = {};
  brandTypes = {};

  constructor(private createService: CreateService) { }

  @ViewChild('fileInput') fileInput = new ElementRef(null) ;

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
    console.log(this.imageUrls)
  }

  public onBgClick(event: any) {
  
    if (!event.target.classList.contains('create-form')) { 
      this.closeCreateWindow();
    } 
  }

  closeCreateWindow(): void {
    this.createService.setCreateWindowStatus();
  }
}
