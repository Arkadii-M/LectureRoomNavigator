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

@Component({
  selector: 'map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  public map!: Map
  private map_size = [0, 0, 612.47321, 408.0199];
  private map_circles_source = new VectorSource();

  ngOnInit(): void {
    this.map = new Map({
      layers: [
        new ImageLayer({
          source: new Static({
            url: '../../assets/images/ukraine.svg',
            imageExtent: this.map_size,
          })
        }),
        new VectorLayer({
          source: this.map_circles_source,
        }),
      ],
      target: 'map',
      view: new View({
        projection: new Projection({
          code: 'xkcd-image',
          units: 'm',
          extent: [0,0,200,200],
        }),
        maxZoom: 5,
        center: getCenter(this.map_size),
        zoom: 0,
      })
    });
    this.map.on('click', this.OnMapClickAddCircle.bind(this));
  }

  OnMapClickAddCircle(event: any) {
    var new_circle = new Feature({ geometry: new Circle(event.coordinate, 5), });
    this.map_circles_source.addFeature(new_circle);
  }
}
