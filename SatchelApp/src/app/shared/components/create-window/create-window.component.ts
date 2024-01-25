import { Component } from '@angular/core';
import { CreateService } from 'src/app/core/services/create.service';

@Component({
  selector: 'app-create-window',
  templateUrl: './create-window.component.html',
  styleUrls: ['./create-window.component.css']
})
export class CreateWindowComponent {

  constructor(private createService: CreateService) { }

  public onBgClick(event: any) {
  
    if (!event.target.classList.contains('create-form')) { 
      this.closeCreateWindow();
    } 
  }

  closeCreateWindow(): void {
    this.createService.setCreateWindowStatus();
  }
}
