import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model:any ={};

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
  }

  register(){
    // console.log(this.model);
    this.authService.register(this.model).subscribe(()=>{
      console.log("kullanıcı oluşturuldu");
    },error=>{
      console.log(error);
    });
  }

}
