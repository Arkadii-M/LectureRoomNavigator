import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  user_logeed_in = false;
  user_is_admin = false;

  constructor(private auth_service: AuthService) {
    this.auth_service.fireIsLoggedIn.subscribe(res => {
      console.log(res);
      this.user_logeed_in = res.loggedin; this.user_is_admin = res.is_admin;
    })
  }

  ngOnInit(): void {
    this.user_logeed_in = this.auth_service.IsLogin();
    this.user_is_admin = this.auth_service.HasAdminRole();
    //this.user_logeed_in = this.auth_service.IsLogin();
    //this.user_is_admin = this.auth_service.HasAdminRole();
    //this.auth_service.fireIsLoggedIn.subscribe(res => { this.user_logeed_in = res.loggedin; this.user_is_admin=res.is_admin; })
  }

  logout() {
    this.auth_service.Logout();
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
