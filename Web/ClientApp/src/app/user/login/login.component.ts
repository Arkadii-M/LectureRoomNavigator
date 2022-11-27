import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Floor, MapComponent } from '../../map/map.component';
import { AuthService, LogInResult } from '../../services/auth.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';

  private LoggedIn(): void { // route to return url
    console.log("User is logged in successful");
  }

  constructor(private auth_service: AuthService) {
    this.auth_service.getLoginEmitter().subscribe((res: LogInResult) => {
      console.log(res);
      if (res.loggedin)
        this.LoggedIn();
    });
  }
  ngOnInit(): void {

  }

  Login() {
    this.auth_service.Login(this.username, this.password);
  }
}
