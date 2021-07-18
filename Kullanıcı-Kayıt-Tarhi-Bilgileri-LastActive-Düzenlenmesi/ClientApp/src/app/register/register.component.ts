import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_service/alertify.service';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model:any ={};

  constructor(private authService:AuthService,
              private alertify:AlertifyService,
              private route: Router) { }

  ngOnInit(): void {
  }

  register(){
    // console.log(this.model);
    this.authService.register(this.model).subscribe(()=>{
      this.alertify.success("kullanıcı oluşturuldu");
    },error=>{
      this.alertify.error(error);
    },()=>{
      this.authService.login(this.model).subscribe(()=>{
        this.route.navigate(['/members'])
      })
    });
  }

}
