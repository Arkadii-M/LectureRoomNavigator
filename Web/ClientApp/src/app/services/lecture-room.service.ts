import { Injectable,Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LectureRoom } from '../dto/lectrure-room.dto';
import { Observable } from 'rxjs';


@Injectable()
export class LectureRoomService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private formHeader(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + (localStorage.getItem('token') ?? '')
    });
  }

  getAll(): Observable<LectureRoom[]>{
    return this.http.get<LectureRoom[]>(this.baseUrl + 'api/LectureRooms');
  }

  addOne(room: LectureRoom) {
    return this.http.post(this.baseUrl + 'api/LectureRooms', room, { headers: this.formHeader() });
  }
  Update(room: LectureRoom) {
    return this.http.put(this.baseUrl + 'api/LectureRooms', room, { headers: this.formHeader() });
  }
  Delete(room_id: string) {
    let httpParams = new HttpParams().set('id', room_id);
    return this.http.delete(this.baseUrl + 'api/LectureRooms', { params: httpParams, headers: this.formHeader() });
  }
}
