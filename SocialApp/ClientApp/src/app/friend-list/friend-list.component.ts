import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { AlertifyService } from '../_service/alertify.service';
import { UserService } from '../_service/user.service';
@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {

  followParams:string='followings';
  users: User[];

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.userService.getUsers(this.followParams).subscribe(users =>{
      this.users = users;
    },err =>{
      this.alertify.error(err);
    })
  }
}
