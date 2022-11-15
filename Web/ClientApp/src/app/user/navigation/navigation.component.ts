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
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'room-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
  providers: [PathService, NavigationNodeService, LectureRoomService, NavigationEdgeService, DropdownModule],
})
export class NavigationComponent{


  public start_point?: LectureRoom;
  public stop_point?: LectureRoom;
  public all_lecture_rooms: LectureRoom[] = [];


  public nav_lecture_rooms: LectureRoom[] = [];
  public nav_navigation_nodes: NavigationNode[] = [];
  public nav_navigation_edges: NavigationEdge[] = [];
  public path: SimplePath = new SimplePath;

  public current_floor: Floor = Floor.FirstFloor;
  public current_node_id: string = '';
  public current_node_index: number = 0;
  public current_highlight: [number, number][] = [];


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

  private ExtractCoordinates(node: NavigationNode) {
    return [node.x, node.y];
  }



  private GetDataForPath() {
    if (this.path.isAnyPathExists) {
      this.nav_lecture_rooms = this.path?.lectureRoomArray;
      this.nav_navigation_nodes = this.path?.navigationNodesArray;
      this.nav_navigation_edges = this.path?.navigationEdgesArray;
      this.current_node_id = this.nav_navigation_nodes[0].id;
      this.current_node_index = 0;
      this.show_map = true;

      let curr_node = this.nav_navigation_nodes[0];

      this.current_highlight = [[curr_node.x, curr_node.y]];
    }
    else { alert("No path exists!"); }
  }

  ngOnInit(): void {
    this.lect_room_service.getAll().subscribe(results => {
      this.all_lecture_rooms = results.sort((a, b) => {
        if (Number(a.name) < Number(b.name))
          return -1;
        if (Number(a.name) > Number(b.name))
          return 1;
        return 0;
      })

    }, err => console.log(err));
  }

  show_next() {
    this.current_node_index += 1;

    if (this.current_node_index >= this.nav_navigation_nodes.length) {
      this.current_node_id = this.to_id;
    }
    else {
      let curr_node =  this.nav_navigation_nodes[this.current_node_index];
      this.current_node_id = curr_node.id;
      this.current_floor = curr_node.floor;
      this.current_highlight = [[curr_node.x, curr_node.y]];

    }

  }

  find_and_show_path() {
    if (this.start_point && this.stop_point) {
      this.current_floor = this.start_point.floor;
      this.from_id = this.start_point.id;
      this.to_id = this.stop_point.id;
      this.path_service.getOptimalPath(this.from_id, this.to_id).subscribe(
        results => {
          this.path = results;
          this.GetDataForPath();
        }, err => console.log(err));
    }
    else { console.log("start or stop is null") }
  }
}
