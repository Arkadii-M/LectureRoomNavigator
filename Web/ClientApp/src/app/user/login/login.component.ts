import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Floor, MapComponent } from '../../map/map.component';
import { AuthService, LogInResult } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';
  returnUrl: string = '/';
  login_failed: boolean = false;

  private LoggedIn(): void { // route to return url
    console.log("User is logged in successful");
    this.router.navigateByUrl(this.returnUrl);

  }
  

  constructor(private auth_service: AuthService,
    private route: ActivatedRoute,
    private router: Router,) {
  }
  ngOnInit(): void {
    this.auth_service.getLoginEmitter().subscribe((res: LogInResult) => {
      console.log(res);
      if (res.loggedin)
        this.LoggedIn();
      else
        this.login_failed = true;

    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

  }

  Login() {
    this.auth_service.Login(this.username, this.password);
  }
}
