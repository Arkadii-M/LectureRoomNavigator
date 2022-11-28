import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LectureRoom } from '../dto/lectrure-room.dto';
import { Observable } from 'rxjs';
import { User } from '../dto/user.dto';


@Injectable()
export class UserService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private formHeader(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + (localStorage.getItem('token') ?? '')
    });
  }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'api/Users', { headers: this.formHeader() });
  }

  addOne(user: User) {
    return this.http.post(this.baseUrl + 'api/Users',user);
  }
  Update(user: User) {
    return this.http.put(this.baseUrl + 'api/Users', user, { headers: this.formHeader() });
  }
  Delete(user_id: string) {
    let httpParams = new HttpParams().set('id', user_id);
    return this.http.delete(this.baseUrl + 'api/Users', { params: httpParams, headers: this.formHeader() });
  }
}
