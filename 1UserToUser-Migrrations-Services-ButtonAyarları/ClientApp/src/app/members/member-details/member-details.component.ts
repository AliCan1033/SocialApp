import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_service/alertify.service';
import { AuthService } from 'src/app/_service/auth.service';
import { UserService } from 'src/app/_service/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})

export class MemberDetailsComponent implements OnInit {

  user: User;
  followText:string='Follow';
  constructor(private userService:UserService,private alertfy:AlertifyService,private route:ActivatedRoute,private authService:AuthService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.user=data.user;
    })
  }

  followUser(userId:number){
    this.userService.followUser(this.authService.decodedToken.nameid,userId)
                    .subscribe(result =>{
                      this.alertfy.success(this.user.name+'kullanıcı takibe alındı');
                      this.followText='Already Follow';
                    },err=>{
                      this.alertfy.error(err);
                    })
  }


}
