import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CreateService {

  isCreateOpen : boolean = false;

  constructor() { }

  setCreateWindowStatus() : void{
    this.isCreateOpen = !this.isCreateOpen;
    console.log(this.isCreateOpen)
  }
}
