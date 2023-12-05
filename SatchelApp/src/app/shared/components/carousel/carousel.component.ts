import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent {

  @Input() images: string[] = []
  @Input() indicators = true
  @Input() autoslide = false
  @Input() slideInterval = 3000

  selectedIndex = 0;

  ngOnInit(): void{
    if(this.autoslide){
      this.autoSlideImage()
    }
    console.log(this.images);
  }

  autoSlideImage(): void{
    setInterval(() => {
      this.onNextClick()
    }, this.slideInterval)
  }

  onNextClick(): void{
    if(this.selectedIndex === this.images.length - 1){
      this.selectedIndex = 0
    } else{
      this.selectedIndex++
    }
  }

  selectImage(index: number): void{
    this.selectedIndex = index;
  }
}
