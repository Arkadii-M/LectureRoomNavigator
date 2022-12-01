import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LectureRoom } from '../dto/lectrure-room.dto';
import { Observable } from 'rxjs';
import { Role, User } from '../dto/user.dto';


@Injectable()
export class RoleService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private formHeader(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + (localStorage.getItem('token') ?? '')
    });
  }

  getAll(): Observable<Role[]> {
    return this.http.get<Role[]>(this.baseUrl + 'api/Roles', { headers: this.formHeader() });
  }

  addOne(role: Role) {
    return this.http.post(this.baseUrl + 'api/Roles', role);
  }
  Update(role: Role) {
    return this.http.put(this.baseUrl + 'api/Roles', role, { headers: this.formHeader() });
  }
  Delete(role_id: string) {
    let httpParams = new HttpParams().set('id', role_id);
    return this.http.delete(this.baseUrl + 'api/Roles', { params: httpParams, headers: this.formHeader() });
  }
}
