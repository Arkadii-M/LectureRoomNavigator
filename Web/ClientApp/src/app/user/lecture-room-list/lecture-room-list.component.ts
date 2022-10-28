import { Component, Inject, ViewChild,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MapComponent } from '../../map/map.component';
import { LectureRoomService } from '../../services/lecture-room.service';
import { LectureRoom } from '../../dto/lectrure-room.dto';

@Component({
  selector: 'lecture-room-list',
  templateUrl: './lecture-room-list.component.html',
  styleUrls: ['./lecture-room-list.component.css'],
  providers: [LectureRoomService]
})
export class LectureRoomListComponent implements OnInit {
  public lecture_rooms: LectureRoom[] = [];
  @ViewChild(MapComponent, { static: false })
  private child: MapComponent | undefined;

  constructor(private lectrue_room_service: LectureRoomService) {
  }

  ngOnInit(): void {
    this.lectrue_room_service.getAllLecturesRooms().subscribe(results => {
      this.lecture_rooms = results;
    }, err => { console.error(err); });
  }
}
