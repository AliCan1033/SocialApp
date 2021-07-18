import { Routes } from "@angular/router";
import { FriendListComponent } from "./friend-list/friend-list.component";
import { HomeComponent } from "./home/home.component";
import { MemberListComponent } from "./member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { NotfoundComponent } from "./notfound/notfound.component";
import { AuthGuard } from "./_guards/auth-guard";

export const appRoutes:Routes=[


  {path:'',component:HomeComponent},//localhost:4200
  {path:'home',component:HomeComponent},//localhost:4200/home
  {path:'members',component:MemberListComponent, canActivate:[AuthGuard]},//localhost:4200/members
  {path:'friends',component:FriendListComponent, canActivate:[AuthGuard]},
  {path:'messages',component:MessagesComponent, canActivate:[AuthGuard]},
  {path:'**',component:NotfoundComponent}
];
