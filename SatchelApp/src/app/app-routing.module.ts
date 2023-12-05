import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './shared/components/routers/main-page/main-page.component';
import { CartPageComponent } from './shared/components/routers/cart-page/cart-page.component';
import { FavouritesPageComponent } from './shared/components/routers/favourites-page/favourites-page.component';
import { UserPageComponent } from './shared/components/routers/user-page/user-page.component';
import { CatalogComponent } from './shared/components/routers/catalog/catalog.component';
import { ProductComponent } from './shared/components/routers/product/product.component';

const routes: Routes = [
  {path: '', component: MainPageComponent},
  {path: 'cart', component: CartPageComponent},
  {path: 'favourites', component:FavouritesPageComponent},
  {path: 'profile', component: UserPageComponent},
  {path: 'catalog', component: CatalogComponent},
  {path: 'catalog/product/:id', component: ProductComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
