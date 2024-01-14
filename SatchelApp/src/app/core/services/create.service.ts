import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CreateService {

  isCreateOpen : boolean = false;

  constructor() { }

  closeCreateWindow() : void{
    this.isCreateOpen = false;
  }
}
