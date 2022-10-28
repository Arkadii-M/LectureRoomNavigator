import { Component, ViewChild } from '@angular/core';
import { MapComponent } from '../../map/map.component';

@Component({
  selector: 'room-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent{

  @ViewChild(MapComponent, { static: false })
  private child: MapComponent | undefined;
  public x = 0;
  public y = 0;

  getCoordinates() {
    var curr = this.child?.GetLastMouseClickCoordinates();
    [this.x, this.y] = curr == undefined ? [0, 0] : curr;
  }
  setMapCenterOnCoordinates() {
    this.child?.SetCenterAt([this.x, this.y]);
    this.child?.SetFloorView(2);
  }

}
