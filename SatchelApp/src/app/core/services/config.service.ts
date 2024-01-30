import { Injectable } from '@angular/core';
import { AuthorizationService } from './authorization.service';

export interface Brand {
  brandTypeId: number;
  name: string;
}

export interface ProductType {
  productTypeId: number;
  name: string;
}

export interface UserDto {
  userId: number,
  email: string,
  userTypeName: string
}

export interface ProductDto {
  name: string,
  description: string,
  productTypeId: number,
  price: number,
  brandTypeId: number,
  genderTypeId: number
  sizeIds: number[]
}

export interface UserPageData{
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  dateOfBirth: Date;
  userPhotoSrc: string;
  userType: string;
  // можно будет дописать - отзывы
}

export interface Step {
  title: string;
  fields: {
    name: string;
    type: string;
  }[] 
}

@Injectable({
  providedIn: 'root'
})

export class ConfigService {

  constructor(private authorizationService: AuthorizationService) { }

  readonly PATHS = {
    inactiveStar: '../../../../../assets/images/icons/favourites.svg',
    activeStar: '../../../../../assets/images/icons/activeFavourite.svg',
    defaultUserPhoto: '../../../assets/images/defaultUserImage.avif',
    defaultProductImage: '../../../assets/images/defaultProductImage.jpg' //лучше скачать, долго грузится иногда
  };

  readonly mainCarouselImages = [
    '../../../assets/images/mainCarouselImage1.webp',
    '../../../assets/images/mainCarouselImage2.jpg',
    '../../../assets/images/mainCarouselImage3.jpg'
  ]

  getFormattedPrice(num: number) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ") + ' ₽';
  }
}



// 1)Пути к ресурсам: Пути к изображениям, такие как inactiveStar и activeStar, выглядят очень длинными и сложными. 
// Если возможно, рассмотрите возможность перемещения этих ресурсов в более доступное место, чтобы упростить пути.

// 2)URL изображений: URL-ы изображений, такие как defaultProductImage и mainCarouselImages, указывают на внешние ресурсы. 
// Это может привести к проблемам с производительностью или доступностью, если эти ресурсы станут недоступными или изменятся. 
// Если возможно, рассмотрите возможность хранения этих изображений локально в вашем приложении.

// 3)Интерфейсы: Интерфейсы Brand и ProductType объявлены в этом файле. 
// Если они используются в других частях вашего приложения, 
// вы можете рассмотреть возможность перемещения их в отдельный файл или файлы для лучшей организации и повторного использования.

// 4)Метод getFormattedPrice: Этот метод добавляет знак рубля (₽) к отформатированной строке. 
// Если ваше приложение будет использоваться в разных странах с разной валютой, вы можете рассмотреть возможность сделать этот метод более гибким, 
// позволяя передавать символ валюты в качестве параметра.
