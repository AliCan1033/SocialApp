import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  baseUrl: string = "http://localhost:5000/api/user/";//çünkü service kısmında login olabilmesi için "http://localhost:5000/api/user/login olması gerekiyor"
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + "login", model).pipe(
      map((response: any) => {
        const result = response;
        if (result) {
          // console.log(result.token);
          localStorage.setItem("token", result.token);
          this.decodedToken=this.jwtHelper.decodeToken(result.token);
        }
      })
    )
  }
  register(model: any) {
    return this.http.post(this.baseUrl + "register", model);
  }

  loggedIn() {//aut guard yani yani kullanıcı login olmuşsa linkleri kullanabilsin olmamışsa kullanamasın
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);//token süresi biterse false döner
  }
}
