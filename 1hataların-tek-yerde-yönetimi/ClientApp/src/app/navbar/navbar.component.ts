import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_service/auth.service';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model: any = {};

  constructor(public authService:AuthService,private router: Router) { }

  ngOnInit(): void {
  }

  login(){
    // console.log(this.model);//htmldeki form kısmında inputlardaki değerler console yazdırılır
    this.authService.login(this.model).subscribe(next =>{
      console.log("log başarılı");
      this.router.navigate(['/members']);//kullanıdı başarı ile giriş yapmışta angi sayfaya yönlencek burada navigate ile yapılır
    },error =>{
      console.log(error);
    })
  }

  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem("token");
    console.log("logout");
    this.router.navigate(['/home']);
  }
}
