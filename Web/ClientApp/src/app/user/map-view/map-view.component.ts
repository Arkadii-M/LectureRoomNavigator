import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MapComponent } from '../../map/map.component';
import { LectureRoomService } from '../../services/lecture-room.service'
import { LectureRoom } from '../../dto/lectrure-room.dto'

@Component({
  selector: 'map-view',
  templateUrl: './map-view.component.html',
  styleUrls: ['./map-view.component.css'],
  providers: [LectureRoomService]
})
export class MapViewComponent implements OnInit {

  public lecture_rooms: LectureRoom[] = [];

  @ViewChild(MapComponent, { static: false })
  private child: MapComponent | undefined;

  current_floor: number = 0;
  show_rooms: boolean = true;
  show_nav_nodes: boolean = false;

  constructor(private lectrue_room_service: LectureRoomService) {
  }

  ngOnInit(): void {
    this.lectrue_room_service.getAllLecturesRooms().subscribe(results => {
      this.lecture_rooms = results;
    }, err => { console.error(err); });
  }

  setFloor(value: string) {
    this.current_floor = Number(value);
  }
  ShowRoom(value: string) {
    this.show_rooms = (value == "true") ? true : false;
  }
  ShowNavigation(value: string) {
    this.show_nav_nodes = (value == "true") ? true : false;
  }

  render_map() {
    console.log("render..", this.show_rooms);
    this.child?.SetFloorView(this.current_floor);

    if (this.show_rooms) {
      var rooms_xy: any[] = [];
      this.lecture_rooms.forEach((r) => {
        rooms_xy.push([r.x, r.y]);
      });
      console.log(rooms_xy);
      this.child?.AddLectureRoomsOnMap(rooms_xy);
    }

    if (this.show_nav_nodes) {

    }
  }
}
