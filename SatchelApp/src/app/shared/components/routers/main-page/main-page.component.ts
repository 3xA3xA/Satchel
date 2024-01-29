import { Component } from '@angular/core';
import { ConfigService } from 'src/app/core/services/config.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})

export class MainPageComponent {
  images = this.configService.mainCarouselImages;
  
  constructor(private configService: ConfigService) { }
}
