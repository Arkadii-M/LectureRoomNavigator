import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs';




@Injectable()
export class AuthService {
  private jwtHelper: JwtHelperService = new JwtHelperService();
  private readonly admin_role: string = 'admin';
  private readonly aps_net_claims_map: { [key: string]: string } =
    {
      role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
      username: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    };
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string ) { }

  HasAdminRole(): boolean {
    let tokenPayload = this.jwtHelper.decodeToken(localStorage.getItem('token') ?? undefined);
    console.log("token decoded:", tokenPayload);
    let roles: string[] = tokenPayload[this.aps_net_claims_map['role']] as string[];
    return roles.includes(this.admin_role);;
  }

  IsLogin(): boolean {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token') ?? undefined);
  }
  Login(username: string, password: string): boolean {
    this.http.post(this.baseUrl + 'api/Login', { UserName: username, Password: password }).pipe(
      map((data: any) => { localStorage.removeItem('token'); localStorage.setItem('token', data.token);},
        catchError(err => { console.log(err); return []; }))).subscribe();
    return this.IsLogin();
  }


}
