import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_service/alertify.service';
import { AuthService } from '../_service/auth.service';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model: any = {};

  constructor(public authService:AuthService,private router: Router,private alertify: AlertifyService) { }

  ngOnInit(): void {
  }

  login(){
    // console.log(this.model);//htmldeki form kısmında inputlardaki değerler console yazdırılır
    this.authService.login(this.model).subscribe(next =>{
      this.alertify.success("log başarılı");
      this.router.navigate(['/members']);//kullanıdı başarı ile giriş yapmışta angi sayfaya yönlencek burada navigate ile yapılır
    },error =>{
      this.alertify.error(error);
    })
  }

  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem("token");
    this.alertify.warning("logout");
    this.router.navigate(['/home']);
  }
}
