import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model: any = {};

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
  }

  login(){
    // console.log(this.model);//htmldeki form kısmında inputlardaki değerler console yazdırılır
    this.authService.login(this.model).subscribe(next =>{
      console.log("log başarılı");
    },error =>{
      console.log("login hatalı");
    })
  }

  loggedIn(){
    const token = localStorage.getItem("token");
    return token?true:false;
  }
  logout(){
    localStorage.removeItem("token");
    console.log("logout");
  }
}
