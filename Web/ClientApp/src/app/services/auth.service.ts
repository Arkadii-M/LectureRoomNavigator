import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class AuthService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  IsLogin(): boolean {
    /*    return confirm('There will be an authentication guard');*/
    return true;
  }
  Login(username: string, password: string): boolean {
    return true;
  }
}
