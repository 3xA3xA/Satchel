import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { MainPageComponent } from './shared/components/routers/main-page/main-page.component';
import { FavouritesPageComponent } from './shared/components/routers/favourites-page/favourites-page.component';
import { CartPageComponent } from './shared/components/routers/cart-page/cart-page.component';
import { UserPageComponent } from './shared/components/routers/user-page/user-page.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { CarouselComponent } from './shared/components/carousel/carousel.component';
import { CatalogComponent } from './shared/components/routers/catalog/catalog.component';
import { ProductComponent } from './shared/components/routers/product/product.component';
import { ProductService } from './core/services/product.service';
import { HttpClientModule } from '@angular/common/http';
import { AuthWindowComponent } from './shared/components/auth-window/auth-window.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainPageComponent,
    FavouritesPageComponent,
    CartPageComponent,
    UserPageComponent,
    FooterComponent,
    CarouselComponent,
    CatalogComponent,
    ProductComponent,
    AuthWindowComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
