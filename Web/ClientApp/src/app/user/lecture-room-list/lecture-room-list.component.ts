import { Component, Inject, ViewChild,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Floor, MapComponent } from '../../map/map.component';
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
    Floor.Basement.toString();
    let fl = Floor[0];
    console.log(fl);
  }

  floor_to_name(id: number) {
    return Floor[id];
  }

  ngOnInit(): void {
    this.lectrue_room_service.getAll().subscribe(results => {
      this.lecture_rooms = results;
    }, err => { console.error(err); });
  }
}
