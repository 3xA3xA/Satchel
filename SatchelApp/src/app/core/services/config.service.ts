import { Injectable } from '@angular/core';
import { AuthorizationService } from './authorization.service';

export interface Brand {
  brantTypeId: number;
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
    defaultUserPhoto: 'https://img.freepik.com/free-vector/illustration-businessman_53876-5856.jpg?size=626&ext=jpg&ga=GA1.1.1826414947.1705190400&semt=ais',
    defaultProductImage: 'https://www.fivebranches.edu/wp-content/uploads/2021/08/default-image.jpg' //лучше скачать, долго грузится иногда
  };

  readonly mainCarouselImages = [
    'https://gornovosti.ru/media/cache/e8/88/e88864d23b58865f4d7336a4aa9bd4a0.webp',
    'https://cdn0.divan.ru/img/v1/rsSv4OLvQbYMSNyHv8HIfJUhSR2K639SAYjgxDqAzuU/rs:fit:1920:1440:0:0/g:ce:0:0/bg:ffffff/q:85/czM6Ly9kaXZhbi93aWtpLWFydGljbGUvMzc2OTc2OC5qcGc.jpg',
    'https://avatars.dzeninfra.ru/get-zen_doc/4120782/pub_636e28839308536fe022cde4_636e28bd5e02fd50d4c70e8b/scale_1200'
  ]

  getFormattedPrice(num: number) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ") + ' ₽';
  }


  //formObject - объект - который не должен находится в месте клика, чтобы закрылась форма
  public onBgClick(event: any, formObject: string) {
    if (!event.target.classList.contains(formObject)) { 
      this.authorizationService.closeAuthWindow();
    } 
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
