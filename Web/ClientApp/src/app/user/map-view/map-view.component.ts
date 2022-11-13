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


@Component({
  selector: 'map-view',
  templateUrl: './map-view.component.html',
  styleUrls: ['./map-view.component.css'],
  providers: [LectureRoomService, NavigationNodeService, NavigationEdgeService]
})
export class MapViewComponent implements OnInit {
  selectedOption: string = '';
  floors: any[] = [
    { value: 0, viewValue: 'Basement' },
    { value: 1, viewValue: 'First' },
    { value: 2, viewValue: 'Second' },
    { value: 3, viewValue: 'Third' },
    { value: 4, viewValue: 'Fourth' },
  ];

  public lecture_rooms: LectureRoom[] = [];
  public navigation_nodes: NavigationNode[] = [];
  public navigation_edges: NavigationEdge[] = [];
  public data_loaded: boolean = false;

  current_floor: number = Floor.FirstFloor;
  show_rooms: boolean = true;
  show_nav_nodes: boolean = true;
  show_nav_edges: boolean = true;
  click_coordinates: number[] = [0,0];

  constructor(
    private lectrue_room_service: LectureRoomService,
    private navigation_node_service: NavigationNodeService,
    private navigation_edge_service: NavigationEdgeService) {
  }

  ngOnInit(): void {

    this.navigation_edge_service.getAll().subscribe(result => {
      this.navigation_edges = result;
    }, err => { console.error(err); });

    this.lectrue_room_service.getAll().subscribe(results => {
      this.lecture_rooms = results;
      this.data_loaded = true;
    }, err => { console.error(err); });
    this.navigation_node_service.getAll().subscribe(result => {
      this.navigation_nodes = result;
    }, err => { console.error(err); })
    
  }

  setFloor(value: string) {
    this.current_floor = Number(value);
  }
  ShowRooms(value: string) {
    this.show_rooms = (value == "true") ? true : false;
  }
  ShowNavigationNodes(value: string) {
    this.show_nav_nodes = (value == "true") ? true : false;
    console.log(this.show_nav_nodes);
  }
  ShowNavigationEdges(value: string) {
    this.show_nav_edges = (value == "true") ? true : false;
    console.log(this.show_nav_edges);
  }
  ChildEventHandler(value: number[]) {
    this.click_coordinates = value;
  }
}
