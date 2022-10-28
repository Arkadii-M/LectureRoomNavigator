import { Component, OnInit } from '@angular/core';
import Map from 'ol/Map';
import ImageLayer from 'ol/layer/Image';
import Static from "ol/source/ImageStatic";
import Projection from 'ol/proj/Projection';
import { getCenter } from 'ol/extent';

import View from 'ol/View';
import VectorSource from 'ol/source/Vector';
import VectorLayer from 'ol/layer/Vector';
import Feature from 'ol/Feature';
import { Circle } from 'ol/geom';
import LayerGroup from 'ol/layer/Group';

@Component({
  selector: 'university-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  private map!: Map
  public ShowRooms: boolean = false;
  public ShowAllNavigationNodes: boolean = false;


  readonly MaxFloor: number = 4;
  readonly MinFloor = 0;
  private map_size = [0, 0, 2044.16, 1478.08];

  private FloorsImagesGroup!: LayerGroup;

  private NavigationNodesLayer!: VectorLayer<VectorSource>;
  private LectureRoomsLayer!: VectorLayer<VectorSource>;

  private map_circles_source = new VectorSource();
  private last_click_coordinates!: number[];

  GetLastMouseClickCoordinates() {
    return this.last_click_coordinates;
  }

  private SetViewWithId(layer: any, id: number, floor: number) {
    if (floor == (id + 1))
      layer.setVisible(true);
    else
      layer.setVisible(false);
  }

  SetFloorView(floor: number) {
    if (this.MinFloor <= floor && floor <= this.MaxFloor) {
      this.FloorsImagesGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, id, floor); }); // show floor layer
      // clear all navigation and lecture rooms nodes
      this.LectureRoomsLayer.getSource()?.clear();
      this.NavigationNodesLayer.getSource()?.clear();
    }
    else { console.log("Invalid floor number") }
  }

  // Lecture room rendering
  AddLectureRoomsOnMap(rooms: any[]) {
    this.LectureRoomsLayer.getSource()?.clear();
    rooms.forEach((room: []) => {
      //TODO add numbers inside feature
      this.LectureRoomsLayer.getSource()?.addFeature(new Feature({
        geometry: new Circle(room, 15),
      }));
    });
  }

  // Navigation nodes rendering
  AddNavigationNodesOnMap(nav_nodes: any[]) {
    this.NavigationNodesLayer.getSource()?.clear();

    nav_nodes.forEach((node: []) => {
      this.LectureRoomsLayer.getSource()?.addFeature(new Feature({
        geometry: new Circle(node, 15),
      }));
    });
  }



  ngOnInit(): void {
    //TODO: Add all five floors here
    this.FloorsImagesGroup = new LayerGroup({
      layers: [new ImageLayer({
          source: new Static({
          url: '../../assets/images/university_floor_1.svg',
          imageExtent: this.map_size
          }),
        visible: true,
      }),
        new ImageLayer({
          source: new Static({
            url: '../../assets/images/ukraine.svg',
            imageExtent: this.map_size,
            
          }),
          visible:false,
        }),

      ],
    });

    this.LectureRoomsLayer = new VectorLayer({ source: new VectorSource({}) });
    this.NavigationNodesLayer = new VectorLayer({ source: new VectorSource({}) });

    this.map = new Map({
      layers: [
        this.FloorsImagesGroup,
        new VectorLayer({
          source: this.map_circles_source,
        }),
        this.LectureRoomsLayer,
        this.NavigationNodesLayer
      ],
      target: 'map',
      view: new View({
        projection: new Projection({
          code: 'xkcd-image',
          units: 'm',
          extent: [0, 0, 500,500],
        }),
        maxZoom: 2,
        center: getCenter(this.map_size),
        zoom: 0,
      })
    });
    this.map.on('click', this.OnMapClickAddCircle.bind(this));
  }

  OnMapClickAddCircle(event: any) {
    var new_circle = new Feature({ geometry: new Circle(event.coordinate, 15  ), });
    this.map_circles_source.addFeature(new_circle);
    this.last_click_coordinates = event.coordinate;
  }
  SetCenterAt(coordinates: [number, number]) {
    this.map.getView().setCenter(coordinates);
  }
}
