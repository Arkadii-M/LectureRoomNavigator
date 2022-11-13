import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Floor, MapComponent } from '../../map/map.component';
import { LectureRoomService } from '../../services/lecture-room.service'
import { LectureRoom } from '../../dto/lectrure-room.dto'
import { v4 as uuidv4 } from 'uuid'

import { NavigationNodeService } from '../../services/navigation-node.service';
import { NavigationNode } from '../../dto/navigation-node.dto';
import { NavigationEdgeService } from '../../services/navigation-edge.service';
import { NavigationEdge } from '../../dto/navigation-edge.dto';

@Component({
  selector: 'navigation-edge-add',
  templateUrl: './navigation-edge-add.component.html',
  styleUrls: ['./navigation-edge-add.component.css'],
  providers: [NavigationNodeService, LectureRoomService, NavigationEdgeService]
})
export class NavigationEdgeComponent {
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

  public nav_first?: NavigationNode;
  public nav_second?: NavigationNode;
  public to_add_edge: NavigationEdge;
  public selected_pair: [NavigationNode | LectureRoom | null, NavigationNode | LectureRoom | null] = [null, null];
  public distance = 0;
  private has_first: boolean = false;

  current_floor: number = Floor.FirstFloor;
  readonly show_nodes: boolean = true;
  readonly show_rooms: boolean = true;
  readonly show_edges: boolean = true;
  click_coordinates: number[] = [];

  private UpdateNavigationEdges() {
    this.navigation_edge_service.getAll().subscribe(results => {
      this.navigation_edges = results;
    }, err => { console.error(err); });

  }

  constructor(
    private navigation_node_service: NavigationNodeService,
    private lecture_room_service: LectureRoomService,
    private navigation_edge_service: NavigationEdgeService) {
    this.to_add_edge = new NavigationEdge;
    this.navigation_node_service.getAll().subscribe(results => { this.navigation_nodes = results; }, err => { console.log(err); })
    this.lecture_room_service.getAll().subscribe(results => { this.lecture_rooms = results; }, err => { console.log(err); })
  }

  ngOnInit(): void {
    this.UpdateNavigationEdges();
  }

  setFloor(value: string) {
    this.current_floor = Number(value);
  }

  private AssignId(id: string) {
    if (this.has_first) {
      this.to_add_edge.outVertexId = id;
    }
    else {
      this.to_add_edge.inVertexId = id;
    }
    this.has_first = !this.has_first;
  }

  ClickNavigationNodeHandler(nav_id: string) {
    this.AssignId(nav_id);
    //this.has_first = !this.has_first;
    //let nav_item: NavigationNode = this.navigation_nodes.find(node => node.id == nav_id) ?? new NavigationNode;
    //this.selected_pair[0] = nav_item;
  }

  ClickLectureRoomHandler(room_id: string) {
    this.AssignId(room_id);
    //let room_item = this.lecture_rooms.find(room => room.id == room_id) ?? new LectureRoom;
    //this.selected_pair[1] = room_item;
  }

  sumbit() {
    if (this.to_add_edge.inVertexId == this.to_add_edge.outVertexId) {
      alert("Can't create edge within same node!");
      return;
    }
    this.to_add_edge.id = uuidv4();
    this.navigation_edge_service.addOne(this.to_add_edge).subscribe(finish => { this.UpdateNavigationEdges(); });
  }

}
