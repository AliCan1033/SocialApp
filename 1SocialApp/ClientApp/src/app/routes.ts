import { Routes } from "@angular/router";
import { FriendListComponent } from "./friend-list/friend-list.component";
import { HomeComponent } from "./home/home.component";
import { MemberDetailsComponent } from "./members/member-details/member-details.component";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { NotfoundComponent } from "./notfound/notfound.component";
import { AuthGuard } from "./_guards/auth-guard";
import { MemberEditResolver } from "./_resolvers/membet-edit.resolver";

export const appRoutes:Routes=[


  {path:'',component:HomeComponent},//localhost:4200
  {path:'home',component:HomeComponent},//localhost:4200/home
  {path:'members',component:MemberListComponent, canActivate:[AuthGuard]},//localhost:4200/members ///canActivate:[AuthGuard] bilgisi eğer sayfayı kullanacak kişini token bilgisi gerekiyorsa
  {path:'member/edit',component:MemberEditComponent, resolve:{user:MemberEditResolver}, canActivate:[AuthGuard]},//localhost:4200/members/edit ///resolve:{user:MemberEditResolver}, normalde complement yüklenmeden önce data bilgisinin bize gelmesi gerektiği için bu kısmı complementte yapmak yerine _resolve servisinde yapmış olduk
  {path:'members/:id',component:MemberDetailsComponent, canActivate:[AuthGuard]},//localhost:4200/members/id
  {path:'friends',component:FriendListComponent, canActivate:[AuthGuard]},
  {path:'messages',component:MessagesComponent, canActivate:[AuthGuard]},
  {path:'**',component:NotfoundComponent}
];
