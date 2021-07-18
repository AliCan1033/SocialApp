import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_service/alertify.service';
import { UserService } from '../../_service/user.service';
import { User } from '../../_models/user';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  users: User[];
  public loading = false;

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.loading=true;
    this.userService.getUsers().subscribe(users =>{
      this.loading=false;
      this.users = users;
    },err =>{
      this.loading=false;
      this.alertify.error(err);
    })
  }
}
