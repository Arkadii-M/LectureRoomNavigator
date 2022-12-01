import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LectureRoom } from '../dto/lectrure-room.dto';
import { Observable } from 'rxjs';
import { Faculty } from '../dto/faculty.dto';


@Injectable()
export class FacultyService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private formHeader(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + (localStorage.getItem('token') ?? '')
    });
  }
  getAll(): Observable<Faculty[]> {
    return this.http.get<Faculty[]>(this.baseUrl + 'api/Faculties');
  }
  addOne(faculty: Faculty) {
    return this.http.post(this.baseUrl + 'api/Faculties', faculty, { headers: this.formHeader() });
  }
  Update(faculty: Faculty) {
    return this.http.put(this.baseUrl + 'api/Faculties', faculty, { headers: this.formHeader() });
  }
  Delete(faculty_id: string) {
    let httpParams = new HttpParams().set('id', faculty_id);
    return this.http.delete(this.baseUrl + 'api/Faculties', { params: httpParams, headers: this.formHeader() });
  }
}
