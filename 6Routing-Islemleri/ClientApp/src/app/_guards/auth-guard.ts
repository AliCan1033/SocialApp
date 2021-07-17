
import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from "../_service/auth.service";

@Injectable({
  providedIn:'root'
})
export class AuthGuard implements CanActivate{ //bu method sayesinde login olmadan bizim belirlediğimiz sayfalar dışındaki sayfalara gidemez
  constructor(private authService: AuthService,private router: Router){}
  canActivate(){
    if (this.authService.loggedIn()) {
      return true;
    }
    console.log ("auth guard");
    this.router.navigate(['/home']);
    return true;
  }
}
