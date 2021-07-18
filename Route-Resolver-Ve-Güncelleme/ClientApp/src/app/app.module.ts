import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { from } from 'rxjs';
import { JwtModule } from "@auth0/angular-jwt";


import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductsComponent } from './products/products.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { AuthGuard } from './_guards/auth-guard';
import { ErrorInterceptor } from './_service/error.intercaptor';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { PhotoGalleryComponent } from './photo-gallery/photo-gallery.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/membet-edit.resolver';

export function tokenGetter() {
  return localStorage.getItem("token");
}



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProductsComponent,
    ProductFormComponent,
    ProductDetailsComponent,
    RegisterComponent,
    MemberListComponent,
    FriendListComponent,
    HomeComponent,
    MessagesComponent,
    NotfoundComponent,
    MemberDetailsComponent,
    PhotoGalleryComponent,
    MemberEditComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,//wep api için bağlantı için
    FormsModule,//html kısımlarında ng submit vs form da kullandığımız bilgiler için
    JwtModule.forRoot({//normalde user.sevice.ts ve yokenle kullanacağımız her yerde headers Authorization ve bearer bilgilerini belirtmemiz lazımdı
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5000"],//token göndermeye gerek var
        disallowedRoutes: ["localhost:5000/api/auth"],//token göndermeye gerek yok
      },
    }),
    RouterModule.forRoot(appRoutes)//localhost:4200/members vs için routes.ts
  ],
  providers: [AuthGuard,
    {
    provide:HTTP_INTERCEPTORS,
    useClass:ErrorInterceptor,
    multi:true
    },
    MemberEditResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
