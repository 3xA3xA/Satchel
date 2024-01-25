import { Component } from '@angular/core';
import { CreateService } from 'src/app/core/services/create.service';
import { ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-create-window',
  templateUrl: './create-window.component.html',
  styleUrls: ['./create-window.component.css']
})
export class CreateWindowComponent {

  constructor(private createService: CreateService) { }

  @ViewChild('fileInput') fileInput = new ElementRef(null) ;

  onFileSelected(event: any) {
    const file = event.target.files[0];
    // Обработка выбранного файла
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
