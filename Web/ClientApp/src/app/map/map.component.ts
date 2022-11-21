import { Component, OnInit, OnChanges, SimpleChanges ,Output,EventEmitter, Input, SimpleChange } from '@angular/core';
import Map from 'ol/Map';
import ImageLayer from 'ol/layer/Image';
import Static from "ol/source/ImageStatic";
import Projection from 'ol/proj/Projection';
import { boundingExtent, containsCoordinate, getCenter } from 'ol/extent';

import View from 'ol/View';
import VectorSource from 'ol/source/Vector';
import VectorLayer from 'ol/layer/Vector';
import Feature from 'ol/Feature';
import { Circle, Geometry, LineString, Point } from 'ol/geom';
import LayerGroup from 'ol/layer/Group';
import Style, { StyleLike } from 'ol/style/Style';
import Fill from 'ol/style/Fill';
import Text from 'ol/style/Text';

import { LectureRoom } from '../dto/lectrure-room.dto';
import { NavigationNode } from '../dto/navigation-node.dto';
import { Collection } from 'ol';
import { NavigationEdge } from '../dto/navigation-edge.dto';
import Stroke from 'ol/style/Stroke';
import BaseLayer from 'ol/layer/Base';
import { Coordinate } from 'ol/coordinate';

export enum Floor {
  Basement = 0,
  FirstFloor,
  SecondFloor,
  ThirdFloor,
  FourhFloor
}

export enum NodeStyle {
  classic = 'classic',
  dashed = 'dashed',
  selected = 'selected'

}

@Component({
  selector: 'university-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, OnChanges {

  private map!: Map
  public readonly navigation_layer_lable: string = "navigation_layer";
  public readonly lecture_room_layer_lable: string = "lecture_room_layer";

  readonly NumberOfFloors: number = 5;
  private _CurrentFloor: Floor = Floor.FirstFloor;
  private map_size = [0, 0, 2876, 2208];
  private current_map_center: Coordinate = getCenter(this.map_size);

  private FloorsImagesGroup!: LayerGroup;
  private NavigationNodesGroup: LayerGroup = new LayerGroup({});
  private LectureRoomGroup: LayerGroup = new LayerGroup({});
  private NavigationEdgeGroup: LayerGroup = new LayerGroup({});


  private CreateLectureRoomFeature(room: LectureRoom) {
    let curr_style = new Style({
      fill: new Fill({ color: 'rgba(185,255,200,0.9)' }),
      text: new Text({ text: room.name, scale: 1.3 })
    });

    let curr_feature = new Feature({ geometry: new Circle([room.x, room.y], 15) });

    curr_feature.setId(room.id); // attach id
    curr_feature.set('name', room.name);
    curr_feature.set('style', NodeStyle.classic);
    curr_feature.setStyle((feature, resolution) => {
      let base_style = new Style({
        fill: new Fill({ color: 'rgba(185,255,200,0.9)' }),
        text: new Text({ text: feature.get('name'), scale: 1.3 })
      });
      switch (feature.get('style')) {
        case NodeStyle.selected:
          base_style.setStroke(new Stroke({ color: 'rgba(18,237,245,1.0)', width: 3 }))
          break;
      }
      return base_style;
    });
    return curr_feature;
  }

  private CreateNavigationNodeFeature(node: NavigationNode) {
    let curr_feature = new Feature({ geometry: new Circle([node.x, node.y], 10)});

    curr_feature.set('style', NodeStyle.classic);
    curr_feature.setId(node.id); // attach id
    curr_feature.setStyle((feature, resolution) => {
      let base_style = new Style({ fill: new Fill({ color: 'rgba(255,35,44,0.9)' }) });
      switch (feature.get('style')) {
        case NodeStyle.dashed:
          base_style.setStroke(new Stroke({ color: 'rgba(0,0,255,1.0)', width: 4, lineDash: [1, 6] }));
          break;
        case NodeStyle.selected:
          base_style.setStroke(new Stroke({ color: 'rgba(18,237,245,1.0)', width: 3 }))
          break;
      }
      return base_style;
    });


    return curr_feature;
  }

  private CreateEdgeFeature(edge: NavigationEdge) {
    let curr_style = new Style({
      stroke: new Stroke({ color: 'rgba(0,0,0,1.0)', width: 3, lineDash: [4, 8] }),
    });

    if (edge.inElement && edge.outElement) {
      var points = [[edge.inElement?.x, edge.inElement?.y], [edge.outElement?.x, edge.outElement?.y]];
    }
    else {
      points = []
    }
    let curr_feature = new Feature({ geometry: new LineString(points) });
    curr_feature.setStyle(curr_style);
    curr_feature.setId(edge.id); // attach id
    return curr_feature;

  }


  @Input()
  set LectureRoomsLayersData(rooms: LectureRoom[]) {
    if (rooms.length != 0) {
      let layers: Collection<VectorLayer<VectorSource>> = new Collection<VectorLayer<VectorSource>>();
      for (let i = 0; i < this.NumberOfFloors; ++i) {
        let curr_source = new VectorSource({ features: [] });

        rooms.filter((value, id, arr) => { return value.floor == i }).forEach((room, id, arr) => {
          curr_source.addFeature(this.CreateLectureRoomFeature(room));
        });

        layers.push(new VectorLayer({
          source: curr_source,
          visible: false,
          properties: { floor: i, type: this.lecture_room_layer_lable }
        }));
        curr_source.changed();
      }
      this.LectureRoomGroup.setLayers(layers);
      this.LectureRoomGroup.changed();
    }
  }
  @Input()
  set NavigationNodesLayersData(nodes: NavigationNode[]) {
    if (nodes.length != 0) {
      let layers: Collection<VectorLayer<VectorSource>> = new Collection<VectorLayer<VectorSource>>();
      for (let i = 0; i < this.NumberOfFloors; ++i) {
        let curr_source = new VectorSource({ features: [] });

        nodes.filter((value, id, arr) => { return value.floor == i }).forEach((node, id, arr) => {
          curr_source.addFeature(this.CreateNavigationNodeFeature(node));
        });

        layers.push(new VectorLayer({
          source: curr_source,
          visible: false,
          properties: { floor: i, type: this.navigation_layer_lable },
        }));
        curr_source.changed();
      }
      this.NavigationNodesGroup.setLayers(layers);
      this.NavigationNodesGroup.changed();
    }
  }

  @Input()
  set NavigationEdgesLayersData(edges: NavigationEdge[]) {
    if (edges == undefined || edges.length != 0) {
      let layers: Collection<VectorLayer<VectorSource>> = new Collection<VectorLayer<VectorSource>>();
      for (let i = 0; i < this.NumberOfFloors; ++i) {
        let curr_source = new VectorSource({ features: [] });


        edges.filter((value, id, arr) => {
          return (value.inElement?.floor == i && value.outElement?.floor == i)
        }).forEach((edge, id, arr) => {
          curr_source.addFeature(this.CreateEdgeFeature(edge));
        });

        layers.push(new VectorLayer({
          source: curr_source,
          visible: false,
          properties: { floor: i, type: "nav_layer" }
        }));
        curr_source.changed();
      }
      this.NavigationEdgeGroup.setLayers(layers);
      this.NavigationEdgeGroup.changed();
    }
  }
  @Input()
  set CurrentFloor(floor: Floor) {
    this._CurrentFloor = floor;
    if (this.map)
      this.SetFloorView(this._CurrentFloor);
  }
  @Input()
  set HighlightNodes(id_style: [string, NodeStyle][]) {

    if (id_style) {
      id_style.forEach((val) => { this.HighlightNode(val); });
    }
  }

  @Input()
  set ShowLectureRooms(value: boolean) {
    this.LectureRoomGroup.setVisible(value);
  }

  @Input()
  set ShowNavigationNodes(value: boolean) {
    this.NavigationNodesGroup.setVisible(value);
  }

  @Input()
  set ShowNavigationEdges(value: boolean) {
    this.NavigationEdgeGroup.setVisible(value);
  }

  @Input()
  set SetCenterIfNotInView(value: [number, number]) {
    this.current_map_center = value;
    if (this.map) {
      //if (!containsCoordinate(this.map.getView().calculateExtent(this.map.getSize()), this.current_map_center))
      //  this.map.getView().setCenter(this.current_map_center);
      this.map.getView().animate({
        center: this.current_map_center,
        duration: 500
      })
    }
  }

  @Output() click_coordinates: EventEmitter<number[]> = new EventEmitter<number[]>();
  @Output() on_navigation_click: EventEmitter<string> = new EventEmitter<string>();
  @Output() on_room_click: EventEmitter<string> = new EventEmitter<string>();

  ngOnChanges(changes: SimpleChanges) {
    for (let propName in changes) {
      if (propName == "LectureRoomsLayersData")
        if (this.map)
          this.SetFloorView(this._CurrentFloor);

      if (propName == "NavigationNodesLayersData")
        if (this.map)
          this.SetFloorView(this._CurrentFloor);

      if (propName == "NavigationEdgesLayersData")
        if (this.map)
          this.SetFloorView(this._CurrentFloor);

      if (propName == "HighlightNodes") {
        let prop: SimpleChange = changes[propName];
        if (this.map) {
          if (!prop.firstChange) {
            let prev_h = prop.previousValue as [string, NodeStyle][];
            let curr_ids: string[] = (prop.currentValue as [string, NodeStyle][]).map((val) => { return val[0] });
            prev_h.filter((val) => { return !curr_ids.some(curr_id => curr_id === val[0]) }).forEach((val) => { this.UnHighlightNode(val); })
          }
        }
      }
    }
  }

  private map_circles_source = new VectorSource();
  private last_click_coordinates!: number[];

  GetLastMouseClickCoordinates() {
    return this.last_click_coordinates;
  }

  private SetViewWithId(layer: any, floor: Floor) {
    layer.setVisible(layer.get("floor") == floor);
  }

  SetFloorView(floor: Floor) {
    this.FloorsImagesGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, floor); }); // show floor layer
    this.LectureRoomGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, floor); });
    this.NavigationNodesGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, floor); });
    this.NavigationEdgeGroup.getLayers().forEach((l, id) => { this.SetViewWithId(l, floor); })
  }

  SetCenter(point: number[]) {
    this.map.getView().setCenter(point);
  }

  setFeatureStyle(layers: Collection<BaseLayer>, id_style: [string, NodeStyle]) {
    layers.forEach((l) => {
      let curr_layer = l as VectorLayer<VectorSource>;
      let curr_feature = curr_layer.getSource()?.getFeatureById(id_style[0]);

      if (curr_feature) {
        curr_feature.set('style', id_style[1])
        curr_feature.changed();
      }
    })
  }

  UnHighlightNode(id_style: [string, NodeStyle]) {
    id_style[1] = NodeStyle.classic;
    this.setFeatureStyle(this.NavigationNodesGroup.getLayers(), id_style);
    this.setFeatureStyle(this.LectureRoomGroup.getLayers(), id_style);
  }

  HighlightNode(id_style: [string, NodeStyle]) {
    this.setFeatureStyle(this.NavigationNodesGroup.getLayers(), id_style);
    this.setFeatureStyle(this.LectureRoomGroup.getLayers(), id_style);
  }
 

  ngOnInit(): void {
    let floor_layers: Collection<ImageLayer<Static>> = new Collection<ImageLayer<Static>>;
    for (let i = 0; i < this.NumberOfFloors; ++i) {
      floor_layers.push(new ImageLayer({
        source: new Static({
          url: '../../assets/images/university_floor_' + i.toString() + '.svg',
          imageExtent: this.map_size
        }),
        visible: false,
        properties: { floor: i }
      }))
    }
    this.FloorsImagesGroup = new LayerGroup();
    this.FloorsImagesGroup.setLayers(floor_layers);

    this.map = new Map({
      layers: [
        this.FloorsImagesGroup,
        new VectorLayer({
          source: this.map_circles_source,
        }),
        this.NavigationEdgeGroup,
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
        center: this.current_map_center,
        zoom: 1,
      })
    });
    this.map.on('click', this.OnMapClickGetCoordinates.bind(this));
    this.SetFloorView(this._CurrentFloor);
  }

  private OnMapClickGetCoordinates(event: any) {

    this.map_circles_source.clear();
    var new_circle = new Feature({ geometry: new Point(event.coordinate) });
    this.map_circles_source.addFeature(new_circle);

    this.map.forEachFeatureAtPixel(event.pixel, (feature, layer) => {
      let feature_id = feature.getId();
      feature_id = (feature_id != null) ? feature_id.toString() : "";
      if (layer.get("type") == this.navigation_layer_lable) {
        this.on_navigation_click.emit(feature_id);
      }
      else if (layer.get("type") == this.lecture_room_layer_lable) {
        this.on_room_click.emit(feature_id);
      }
    });

    this.click_coordinates.emit(event.coordinate);

  }

  SetCenterAt(coordinates: [number, number]) {
    this.map.getView().setCenter(coordinates);
  }

  
}
