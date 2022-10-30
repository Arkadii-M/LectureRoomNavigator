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
import { Circle, Geometry, Point } from 'ol/geom';
import LayerGroup from 'ol/layer/Group';
import { LectureRoom } from '../dto/lectrure-room.dto';
import BaseLayer from 'ol/layer/Base';
import { NavigationNode } from '../dto/navigation-node.dto';
import Style from 'ol/style/Style';
import Fill from 'ol/style/Fill';
import Stroke from 'ol/style/Stroke';
import Text from 'ol/style/Text';

export enum Floor {
  Basement = 0,
  FirstFloor,
  SecondFloor,
  ThirdFloor,
  FourhFloor
}

@Component({
  selector: 'university-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {


  private map!: Map

  readonly NumberOfFloors: number = 5;

  public ShowRooms: boolean = false;
  public ShowAllNavigationNodes: boolean = false;


  private map_size = [0, 0, 2044.16, 1478.08];

  private FloorsImagesGroup!: LayerGroup;
  private NavigationNodesGroup!: LayerGroup;
  private LectureRoomGroup!: LayerGroup;

  private map_circles_source = new VectorSource();
  private last_click_coordinates!: number[];

  GetLastMouseClickCoordinates() {
    return this.last_click_coordinates;
  }

  private SetViewWithId(layer: any, id: number, floor: Floor) {
    if (floor == id)
      layer.setVisible(true);
    else
      layer.setVisible(false);
  }

  SetFloorView(floor: Floor) {
    this.FloorsImagesGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, id, floor); }); // show floor layer
    this.LectureRoomGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, id, floor); });
    this.NavigationNodesGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, id, floor) });
  }

  // Lecture room rendering
  AddLectureRoomsOnMap(rooms: LectureRoom[]) {

    let lec_layers = this.LectureRoomGroup.getLayers().getArray();
    lec_layers.forEach((l, id) => {
      let vec_layer = l as VectorLayer<VectorSource>;
      let vec_source = vec_layer.getSource();
      vec_source?.clear();

      rooms.filter((value) => { return value.floor == id; }).forEach((room) => {// get lecture rooms on this floor
        let curr_style = new Style({
          fill: new Fill({ color: 'rgba(185,255,200,0.9)' }),
          text: new Text({ text: room.name,scale:1.3 })
        });

        let curr_feature = new Feature({ geometry: new Circle([room.x, room.y], 15) });
        curr_feature.setStyle(curr_style);

        vec_source?.addFeature(curr_feature);
      });
      vec_layer.changed();
    });
  }

  ShowLectureRooms(show: boolean) {
    this.LectureRoomGroup.setVisible(show);
  }




  AddNavigationNodesOnMap(nodes: NavigationNode[]) {

    let lec_layers = this.NavigationNodesGroup.getLayers().getArray();
    lec_layers.forEach((l, id) => {
      let vec_layer = l as VectorLayer<VectorSource>;
      let vec_source = vec_layer.getSource();
      vec_source?.clear();

      let features: Feature<Geometry>[] = [];
      nodes.filter((value) => { return value.floor == id; }).forEach((node) => {// get navigation nodes on this floor
        vec_source?.addFeature(new Feature({
          geometry: new Circle([node.x, node.y], 15),
        }));
      });
      vec_source?.changed();
    });
  }


  ngOnInit(): void {
    //TODO: Add all five floors here

    //let floors_layers: ImageLayer<Static>[] = [];
    this.FloorsImagesGroup = new LayerGroup({
      layers: [new ImageLayer({
          source: new Static({
            url: '../../assets/images/ukraine.svg',
          imageExtent: this.map_size
          }),
        visible: false,
      }),
        new ImageLayer({
          source: new Static({
            url: '../../assets/images/university_floor_1.svg',
            imageExtent: this.map_size,
            
          }),
          visible: true,
        }),

      ],
    });

    let nav_empty_layers: VectorLayer<VectorSource>[] = [];
    let lec_empy_layers: VectorLayer<VectorSource>[] = [];

    for (let i = 0; i < this.NumberOfFloors; ++i) {
      nav_empty_layers.push(new VectorLayer({ source: new VectorSource({ features: [] }), visible: false }));
      lec_empy_layers.push(new VectorLayer({ source: new VectorSource({ features: [] }), visible: false }));
    }

    this.NavigationNodesGroup = new LayerGroup({ layers: nav_empty_layers });
    this.LectureRoomGroup = new LayerGroup({ layers: lec_empy_layers });

    this.map = new Map({
      layers: [
        this.FloorsImagesGroup,
        new VectorLayer({
          source: this.map_circles_source,
        }),
        this.NavigationNodesGroup,
        this.LectureRoomGroup,
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
    //this.map.on('click', this.OnMapClickAddCircle.bind(this));
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
