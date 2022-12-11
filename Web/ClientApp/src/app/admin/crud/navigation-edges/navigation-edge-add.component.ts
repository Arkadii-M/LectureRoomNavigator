import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Floor, MapComponent, NodeStyle } from '../../../map/map.component';
import { LectureRoomService } from '../../../services/lecture-room.service'
import { LectureRoom } from '../../../dto/lectrure-room.dto'
import { v4 as uuidv4 } from 'uuid'

import { NavigationNodeService } from '../../../services/navigation-node.service';
import { NavigationNode } from '../../../dto/navigation-node.dto';
import { NavigationEdgeService } from '../../../services/navigation-edge.service';
import { NavigationEdge } from '../../../dto/navigation-edge.dto';
import { IMapElement } from '../../../dto/map-element';

@Component({
  selector: 'navigation-edge-add',
  templateUrl: './navigation-edge-add.component.html',
  styleUrls: ['./navigation-edge-add.component.css'],
  providers: [NavigationNodeService, LectureRoomService, NavigationEdgeService]
})
export class NavigationEdgeComponent {
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

  public nav_first?: NavigationNode;
  public nav_second?: NavigationNode;
  public to_add_edge: NavigationEdge = new NavigationEdge;
  public selected_pair: [NavigationNode | LectureRoom | null, NavigationNode | LectureRoom | null] = [null, null];
  public current_highlight: [string, NodeStyle][] = [];
  public show_map: boolean = false;
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
/*    this.to_add_edge = new NavigationEdge;*/
  }

  ngOnInit(): void {
    this.navigation_node_service.getAll().subscribe(results => { this.navigation_nodes = results; }, err => { console.log(err); })
    this.lecture_room_service.getAll().subscribe(results => { this.lecture_rooms = results; }, err => { console.log(err); })
    this.navigation_edge_service.getAll().subscribe(results => { this.navigation_edges = results; this.show_map = true;}, err => { console.error(err); });
/*    this.UpdateNavigationEdges();*/
  }


  setFloor(event: any) {
    this.current_floor = Number(event.value.value);
  }

  private BothLectureRoom(first_id: string, second_id: string): boolean {
    return this.lecture_rooms.some((room) => { return room.id == first_id; })
      && this.lecture_rooms.some((room) => { return room.id == second_id; });
  }

  private distance_(first: IMapElement, second: IMapElement): number {
    return Math.sqrt(Math.pow(first.x - second.x, 2) + Math.pow(first.y - second.y, 2))/26; // TODO caclucate map size
  }

  private get_map_element(id: string): IMapElement | undefined {
    let res: undefined | IMapElement = this.lecture_rooms.find((lr) => { return lr.id == id; });
    if (!res)
      res = this.navigation_nodes.find((nav) => { return nav.id == id; });

    return res;
  }

  private AssignId(id: string):void {
    if (this.has_first) {
      this.to_add_edge.outVertexId = id;
    }
    else {
      this.to_add_edge.inVertexId = id;
    }

    this.has_first = !this.has_first;
    this.current_highlight = [[this.to_add_edge.inVertexId, NodeStyle.selected], [this.to_add_edge.outVertexId, NodeStyle.selected]];

    if (this.to_add_edge.inVertexId != '' && this.to_add_edge.outVertexId != '') {
      let from_element = this.get_map_element(this.to_add_edge.inVertexId);
      let to_element = this.get_map_element(this.to_add_edge.outVertexId);
      
      if (from_element != undefined && to_element != undefined)
        this.to_add_edge.distance = this.distance_(from_element, to_element);
      else
        this.to_add_edge.distance = 0;
    }


  }

  ClickNavigationNodeHandler(nav_id: string) {
    this.AssignId(nav_id);
  }

  ClickLectureRoomHandler(room_id: string) {
    this.AssignId(room_id);
  }

  sumbit() {
    if (this.to_add_edge.inVertexId == this.to_add_edge.outVertexId) {
      alert("Can't create edge within same node!");
      return;
    }
    else if (this.BothLectureRoom(this.to_add_edge.inVertexId, this.to_add_edge.outVertexId)) {
      alert("Can't create edge between lecture rooms!")
      return;
    }
    this.navigation_edge_service.addOne(this.to_add_edge).subscribe(finish => { this.UpdateNavigationEdges(); });
  }

}
