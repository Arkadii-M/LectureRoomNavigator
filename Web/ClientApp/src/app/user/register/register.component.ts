import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Floor, MapComponent } from '../../map/map.component';
import { AuthService, LogInResult } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../dto/user.dto';
import { v4 as uuidv4 } from 'uuid'

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [UserService]
})
export class RegisterComponent {

  username: string = '';
  password: string = '';


  constructor(private user_service: UserService,
    private route: ActivatedRoute,
    private router: Router,) {
  }

  Register() {
    let new_user = new User();
    new_user.id = uuidv4();
    new_user.userName = this.username;
    new_user.password = this.password;
    this.user_service.addOne(new_user).subscribe((r) => { this.router.navigateByUrl('login'); });
  }
}
