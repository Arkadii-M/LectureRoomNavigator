import { Component, Inject, OnInit } from '@angular/core';
import { Role } from '../../../dto/user.dto';
import { RoleService } from '../../../services/role.service';
import { v4 as uuidv4 } from 'uuid'

@Component({
  selector: 'roles-crud',
  templateUrl: './roles.crud.component.html',
  styleUrls: ['./roles.crud.component.css'],
  providers: [RoleService]
})
export class RolesCRUDComponent {

  public roles: Role[] = [];
  public role: Role = new Role;
  public submitted: boolean = false;
  public addDialog: boolean = false;

  constructor(
    private role_service: RoleService) {
  }

  UpdateRoles() {
    this.role_service.getAll().subscribe(results => { // read all roles from database
      this.roles = results;
      console.log(results);
    }, err => { console.error(err); });
  }
  ngOnInit(): void {
    this.UpdateRoles();
  }

  delete(del_role: Role) {
    this.role_service.Delete(del_role.id).subscribe(res => { this.UpdateRoles() });
  }

  openNew() {
    this.role = new Role;
    this.submitted = false;
    this.addDialog = true;
  }
  hideDialog() {
    this.addDialog = false;
    this.submitted = false;
  }

  saveRole() {
    this.submitted = true;
    this.addDialog = false;

    if (this.role.name.length < 3) {
      console.error("Invalid name length!")
      return;
    }
    this.role.id = uuidv4();
    this.role_service.addOne(this.role).subscribe(res => this.UpdateRoles());
  }
}
