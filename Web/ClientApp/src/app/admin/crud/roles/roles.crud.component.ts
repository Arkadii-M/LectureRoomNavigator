import { Component, Inject, OnInit } from '@angular/core';
import { Role } from '../../../dto/user.dto';
import { RoleService } from '../../../services/role.service';

@Component({
  selector: 'roles-crud',
  templateUrl: './roles.crud.component.html',
  styleUrls: ['./roles.crud.component.css'],
  providers: [RoleService]
})
export class RolesCRUDComponent {

  public roles: Role[] = [];

  constructor(
    private role_service: RoleService) {

    this.role_service.getAll().subscribe(results => { // read all roles from database
      this.roles = results;
      console.log(results);
    }, err => { console.error(err); });
  }

  ngOnInit(): void {
  }
}
