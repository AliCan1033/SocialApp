import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl: string = "http://localhost:5000/api/user/";//çünkü service kısmında login olabilmesi için "http://localhost:5000/api/user/login olması gerekiyor"
  constructor(private http: HttpClient) { }

  login(model:any){
    return this.http.post(this.baseUrl+"login",model).pipe(
      map((response:any)=>{
        const result =response;
        if (result) {
          // console.log(result.token);
          localStorage.setItem("token",result.token);
        }
      })
    )
  }
  register(model:any){
    return this.http.post(this.baseUrl+"register",model);
  }
}
