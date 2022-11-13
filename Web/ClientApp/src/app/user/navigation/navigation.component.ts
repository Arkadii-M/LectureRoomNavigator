import { Component, ViewChild } from '@angular/core';
import { LectureRoom } from '../../dto/lectrure-room.dto';
import { NavigationEdge } from '../../dto/navigation-edge.dto';
import { NavigationNode } from '../../dto/navigation-node.dto';
import { SimplePath } from '../../dto/simple-path.dto';
import { Floor, MapComponent } from '../../map/map.component';
import { LectureRoomService } from '../../services/lecture-room.service';
import { NavigationEdgeService } from '../../services/navigation-edge.service';
import { NavigationNodeService } from '../../services/navigation-node.service';
import { PathService } from '../../services/path.service';

@Component({
  selector: 'room-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
  providers: [PathService, NavigationNodeService, LectureRoomService, NavigationEdgeService]
})
export class NavigationComponent{


  public lecture_rooms: LectureRoom[] = [];
  public navigation_nodes: NavigationNode[] = [];
  public navigation_edges: NavigationEdge[] = [];
  public path?: SimplePath;

  public current_floor: Floor = Floor.FirstFloor;
  public current_node_id: string = '';
  public current_node_index: number = 0;


  public from_id: string = '578d85cf-3c95-498d-8547-b0a445efa9f9';
  /*  public to_id: string = 'f7546872-eba4-4207-a10a-a1d8a3fd4015';*/
  public to_id: string = '827597a1-95af-4607-9857-877d1adc1a18';

  public show_map: boolean = false;



  public readonly show_rooms: boolean = true;
  public readonly show_nav_nodes: boolean = true;
  public readonly show_nav_edges: boolean = true;

  constructor(private path_service: PathService,
    private nav_node_service: NavigationNodeService,
    private nav_edge_service: NavigationEdgeService,
    private lect_room_service: LectureRoomService) {

  }

  private GetDataForPath() {

    this.lecture_rooms = this.path?.lectureRoomArray ?? [];
    this.navigation_nodes = this.path?.navigationNodesArray ?? [];
    this.navigation_edges = this.path?.navigationEdgesArray ?? [];
    this.current_node_id = this.navigation_nodes[0].id;
    this.current_node_index = 0;
    this.show_map = true;
  }

  ngOnInit(): void {
  /*  this.path_service.getOptimalPath('578d85cf-3c95-498d-8547-b0a445efa9f9', 'f7546872-eba4-4207-a10a-a1d8a3fd4015').subscribe(results => { this.path = results; this.GetDataForPath(); }, err => console.log(err));*/
    
  }

  show_next() {
    this.current_node_index += 1;
    if (this.current_node_index >= this.navigation_nodes.length) {
      this.current_node_id = this.to_id;
    }
    else {
      this.current_node_id = this.navigation_nodes[this.current_node_index].id;
      this.current_floor = this.navigation_nodes[this.current_node_index].floor;

    }

  }

  find_and_show_path() {
    this.path_service.getOptimalPath(this.from_id, this.to_id).subscribe(results => { this.path = results; this.GetDataForPath(); }, err => console.log(err));
  }
}
