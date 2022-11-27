import { Injectable, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs';


export class LogInResult {
  constructor(
    public loggedin: boolean,
    public is_admin: boolean) { }
}

@Injectable()
export class AuthService {
  private jwtHelper: JwtHelperService = new JwtHelperService();
  private readonly admin_role: string = 'admin';
  private readonly aps_net_claims_map: { [key: string]: string } =
    {
      role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
      username: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    };
  @Output() fireIsLoggedIn: EventEmitter<LogInResult> = new EventEmitter<LogInResult>(); 
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string ) { }

  HasAdminRole(): boolean {
    let tokenPayload = this.jwtHelper.decodeToken(localStorage.getItem('token') ?? undefined);
    if (tokenPayload == null)
      return false;
    let roles: string[] = tokenPayload[this.aps_net_claims_map['role']] as string[];
    return roles.includes(this.admin_role);;
  }

  IsLogin(): boolean {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token') ?? undefined);
  }
  Login(username: string, password: string): void {

    this.http.post(this.baseUrl + 'api/Login', { UserName: username, Password: password }).pipe(
        map((data: any) => { localStorage.removeItem('token'); localStorage.setItem('token', data.token);},
          catchError(err => { console.log(err); return []; }))).subscribe(res => {
            this.fireIsLoggedIn.emit(new LogInResult(this.IsLogin(), this.HasAdminRole()));
        });
  }

  Logout():void {
    localStorage.removeItem('token');
    this.fireIsLoggedIn.emit({ loggedin: false, is_admin: false });
  }

  getLoginEmitter() { return this.fireIsLoggedIn; }




}
