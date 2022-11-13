import { Injectable,Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LectureRoom } from '../dto/lectrure-room.dto';
import { Observable } from 'rxjs';


@Injectable()
export class LectureRoomService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll(): Observable<LectureRoom[]>{
    return this.http.get<LectureRoom[]>(this.baseUrl + 'api/LectureRooms');
  }

  addOne(room: LectureRoom) {
    return this.http.post(this.baseUrl + 'api/LectureRooms', room);
  }
}
