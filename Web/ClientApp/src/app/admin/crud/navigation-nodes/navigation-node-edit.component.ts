import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Floor, MapComponent } from '../../../map/map.component';
import { LectureRoomService } from '../../../services/lecture-room.service'
import { LectureRoom } from '../../../dto/lectrure-room.dto'
import { v4 as uuidv4 } from 'uuid'
import { Faculty } from '../../../dto/faculty.dto';
import { FacultyService } from '../../../services/faculty.service';
import { NavigationNode } from '../../../dto/navigation-node.dto';
import { NavigationNodeService } from '../../../services/navigation-node.service';

@Component({
  selector: 'navigation-node-edit',
  templateUrl: './navigation-node-edit.component.html',
  styleUrls: ['./navigation-node-edit.component.css'],
  providers: [NavigationNodeService]
})
export class NavigationNodeEditComponent {
  selectedOption: string = '';
  floors: any[] = [
    { value: 0, viewValue: 'Підвал' },
    { value: 1, viewValue: 'Перший' },
    { value: 2, viewValue: 'Другий' },
    { value: 3, viewValue: 'Третій' },
    { value: 4, viewValue: 'Четвертий' },
  ];

  public navigation_nodes: NavigationNode[] = [];

  public to_update_node: NavigationNode = new NavigationNode;

  current_floor: number = Floor.FirstFloor;
  readonly show_nodes: boolean = true;
  data_ready: boolean = false;
  click_coordinates: number[] = [];

  private GetAllNavigationNodes() {
    this.nav_node_service.getAll().subscribe(results => {
      this.navigation_nodes = results;
      this.data_ready = true;
    }, err => { console.error(err); });

  }

  constructor(
    private nav_node_service: NavigationNodeService) {
  }

  ngOnInit(): void {
    this.GetAllNavigationNodes();
  }

  setFloor(event: any) {
    this.current_floor = Number(event.value.value);
    this.to_update_node.id = '';
  }
  ClickNavigationNodeHandle(nav_id: string) {
    let selected = this.navigation_nodes.find((nav) => { return nav.id == nav_id; });
    if (!selected)
      throw new Error("Selected node is not found in list");

    this.to_update_node = selected;
  }
  update() {
    if (this.to_update_node.id != '')
      this.nav_node_service.Update(this.to_update_node).subscribe(finish => { this.GetAllNavigationNodes(); }, err => { console.log(err); });
  }

  delete() {
    if (this.to_update_node.id != '')
      this.nav_node_service.Delete(this.to_update_node.id).subscribe(finish => { this.GetAllNavigationNodes(); }, err => { console.log(err); });
  }

}
