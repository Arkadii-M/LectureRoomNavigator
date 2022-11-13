import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LectureRoom } from '../dto/lectrure-room.dto';
import { Observable } from 'rxjs';
import { Faculty } from '../dto/faculty.dto';


@Injectable()
export class FacultyService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll(): Observable<Faculty[]> {
    return this.http.get<Faculty[]>(this.baseUrl + 'api/Faculties');
  }
}
