import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Floor, MapComponent } from '../../map/map.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthService]
})
export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';
  constructor(private auth_service: AuthService) {
  }
  ngOnInit(): void {

  }

  Login() {
    console.log(this.auth_service.Login(this.username, this.password));
  }
}
