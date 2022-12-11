import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Floor, MapComponent } from '../../map/map.component';
import { LectureRoomService } from '../../services/lecture-room.service'
import { NavigationNodeService } from '../../services/navigation-node.service'
import { LectureRoom } from '../../dto/lectrure-room.dto'
import { NavigationNode } from '../../dto/navigation-node.dto'
import { first } from 'rxjs';
import { NavigationEdgeService } from '../../services/navigation-edge.service';
import { NavigationEdge } from '../../dto/navigation-edge.dto';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'map-view',
  templateUrl: './map-view.component.html',
  styleUrls: ['./map-view.component.css'],
  providers: [LectureRoomService, NavigationNodeService, NavigationEdgeService]
})
export class MapViewComponent implements OnInit {
  selectedOption: string = '';
  floors: any[] = [
    { value: 0, viewValue: 'Підвал' },
    { value: 1, viewValue: 'Перший' },
    { value: 2, viewValue: 'Другий' },
    { value: 3, viewValue: 'Третій' },
    { value: 4, viewValue: 'Четвертий' },
  ];
  current_floor_object: any = this.floors[1];

  public lecture_rooms: LectureRoom[] = [];
  public navigation_nodes: NavigationNode[] = [];
  public navigation_edges: NavigationEdge[] = [];
  public show_room_id: string = '';
  public show_room_coordinates: [number, number] = [1800,300];
  public data_loaded: boolean = false;

  current_floor: number = Floor.FirstFloor;
  show_rooms: boolean = true;

  constructor(
    private lectrue_room_service: LectureRoomService,
    private activateRoute: ActivatedRoute,  ) {
    activateRoute.queryParams.subscribe(
      (queryParam: any) => {
        this.show_room_id = queryParam.id;
      });
  }
  private ShowRoom(room_id: string): void {
    let show_room = this.lecture_rooms.find((room) => { return room.id == room_id; });
    if (show_room) {
      this.show_room_coordinates = [show_room.x, show_room.y];
      this.current_floor = show_room.floor;
      this.current_floor_object = this.floors.find((floor) => { return floor.value == show_room?.floor; })
    }
  }

  ngOnInit(): void {
    this.lectrue_room_service.getAll().subscribe(results => {
      this.lecture_rooms = results;
      this.ShowRoom(this.show_room_id);
      this.data_loaded = true;
    }, err => { console.error(err); });
    
  }

  setFloor(event: any) {
    this.current_floor = Number(event.value.value);
  }
}
