import { Component, Inject, OnInit } from '@angular/core';
import { Role, User } from '../../../dto/user.dto';
import { RoleService } from '../../../services/role.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'users-crud',
  templateUrl: './users.crud.component.html',
  styleUrls: ['./users.crud.component.css'],
  providers: [UserService]
})
export class UsersCRUDComponent {

  public users: User[] = [];

  constructor(
    private users_service: UserService) {

    this.users_service.getAll().subscribe(results => { // read all users from database
      this.users = results;
      console.log(results);
    }, err => { console.error(err); });
  }

  ngOnInit(): void {
  }
}
