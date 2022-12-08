import { Component, Inject, OnInit } from '@angular/core';
import { Role, User } from '../../../dto/user.dto';
import { RoleService } from '../../../services/role.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'users-crud',
  templateUrl: './users.crud.component.html',
  styleUrls: ['./users.crud.component.css'],
  providers: [UserService, RoleService]
})
export class UsersCRUDComponent {

  public users: User[] = [];
  public data_ready: boolean = false;
  public roles: Role[] = [];
  public user: User = new User;
  clonedUsers: { [s: string]: User; } = {};


  constructor(
    private users_service: UserService,
    private roles_service: RoleService) {

    this.users_service.getAll().subscribe(results => { // read all users from database
      this.users = results;
      console.log(results);
      this.data_ready = true;
    }, err => { console.error(err); });
    this.roles_service.getAll().subscribe(results => {
      this.roles = results;
      console.log(results);
    }, err => { console.error(err); })

  }

  ngOnInit(): void {
  }

  onRowEditInit(user: User) {
    this.clonedUsers[user.id] = { ...user };
  }

  onRowEditSave(user: User) {
    this.users_service.Update(user).subscribe(res => { console.log("User updated") })
    delete this.clonedUsers[user.id];
  }

  onRowEditCancel(user: User, index: number) {
    this.users[index] = this.clonedUsers[user.id];
    delete this.clonedUsers[user.id];
  }
}
