import { Component, ViewChild } from '@angular/core';
import { LectureRoom } from '../../dto/lectrure-room.dto';
import { NavigationEdge } from '../../dto/navigation-edge.dto';
import { NavigationNode } from '../../dto/navigation-node.dto';
import { SimplePath } from '../../dto/simple-path.dto';
import { Floor, MapComponent, NodeStyle } from '../../map/map.component';
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
  public start_point: LectureRoom = new LectureRoom;
  public stop_point?: LectureRoom;
  public all_lecture_rooms: LectureRoom[] = [];


  public nav_lecture_rooms: LectureRoom[] = [];
  public nav_navigation_nodes: NavigationNode[] = [];
  public nav_navigation_edges: NavigationEdge[] = [];
  public path: SimplePath = new SimplePath;

  public current_floor: Floor = Floor.FirstFloor;
  public current_node_id: string = '';
  public current_node_index: number = 0;
  public current_coordinates: [number, number] = [0, 0];
  public current_highlight: [string, NodeStyle][] = [];


  public from_id: string = '';
  public to_id: string = '';

  public show_map: boolean = false;
  public find_path_loading: boolean = false;



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

      let curr_node = this.nav_navigation_nodes[0];

      this.current_node_id = curr_node.id;
      this.current_node_index = 0;
      this.current_highlight = [[curr_node.id, NodeStyle.dashed]];
      this.current_coordinates = [curr_node.x, curr_node.y];
      this.show_map = true;
    }
    else { alert("No path exists!"); }
    this.find_path_loading = false;
  }

  ngOnInit(): void {
    this.nav_node_service.GetEnterNode().subscribe(result => {
      this.start_point.id = result.id;
      this.start_point.name = 'Вхід';
      this.start_point.floor = result.floor;
      this.all_lecture_rooms.push(this.start_point);
    }, err => console.log(err))

    this.lect_room_service.getAll().subscribe(results => {
      let sorted_rooms: LectureRoom[] = results.sort((a, b) => {
        if (Number(a.name) < Number(b.name))
          return -1;
        if (Number(a.name) > Number(b.name))
          return 1;
        return 0;
      });
      this.all_lecture_rooms = this.all_lecture_rooms.concat(sorted_rooms);
    }, err => console.log(err));
  }

  private change_node_to(node: NavigationNode) {
    this.current_node_id = node.id;
    this.current_floor = node.floor;
    this.current_highlight = [[node.id, NodeStyle.dashed]];
    this.current_coordinates = [node.x, node.y];
  }

  show_next() {
    if (this.current_node_index+1 == this.nav_navigation_nodes.length) {
      alert("Finish");
      return;
    }

    let prev_node = this.nav_navigation_nodes[this.current_node_index];
    this.current_node_index++;
    let curr_node = this.nav_navigation_nodes[this.current_node_index];
    this.change_node_to(curr_node);

    if (curr_node.floor > prev_node.floor)
      alert("go upstairs");
    else if (curr_node.floor < prev_node.floor)
      alert("go down upstairs");
    
  }

  show_prev() {
    if (this.current_node_index == 0)
      return;
    this.current_node_index--;

    let curr_node = this.nav_navigation_nodes[this.current_node_index];
    this.change_node_to(curr_node);    
}

  find_and_show_path() {
    this.find_path_loading = true;
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
