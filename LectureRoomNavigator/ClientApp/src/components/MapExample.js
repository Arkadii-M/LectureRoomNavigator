import React, { Component } from "react";
import GeoJSON from 'ol/format/GeoJSON';
import Draw from 'ol/interaction/Draw';
import Map from 'ol/Map';
import View from 'ol/View';
import { OSM, Vector, Vector as VectorSource } from 'ol/source';
import VectorImageLayer from 'ol/layer/VectorImage';
import { Tile as TileLayer, Vector as VectorLayer } from 'ol/layer';
import Point from "ol/geom/Point";
import Circle from "ol/geom/Circle";
import ImageLayer from 'ol/layer/Image';
import Static from "ol/source/ImageStatic";
import Projection from 'ol/proj/Projection';
import { getCenter } from 'ol/extent';
import Feature from 'ol/Feature';
import MousePosition from 'ol/control/MousePosition';
import { createStringXY } from 'ol/coordinate';
import { defaults as defaultControls } from 'ol/control';


const image_extent = [0, 0, 612.47321, 408.0199];
export class MapExample extends Component {
    constructor(props) {
        super(props);

        this.state = { center: [0, 0], zoom: 1, selected_point: [0,0] };

        this.circles_source = new VectorSource(
            {
                //features: [new Feature({
                //    geometry: new Circle([60, 270], 5)
                //}),]
            });

        this.map = new Map({
            //controls: defaultControls().extend([this.mousePositionControl]),
            target: null,
            layers: [
                new ImageLayer(
                    {
                        source: new Static(
                            {
                                url: 'ukraine.svg',
                                imageSize: image_extent,
                                imageExtent: image_extent,
                            }),

                    }),
                new VectorLayer(
                    {
                        source: this.circles_source,
                    }),
            ],
            view: new View({
                projection: new Projection({
                    code: 'xkcd-image',
                    units: 'm',
                    extent: image_extent,
                }),
                zoom: 0,
                maxZoom: 4,
                center: getCenter(image_extent),
                zoom: this.state.zoom
            })
        });

    }

    LoadJpgImage() {

    }

    updateMap() {
        this.map.getView().setCenter(this.state.center);
        this.map.getView().setZoom(this.state.zoom);
    }

    OnClickAddCircle(event) {
        this.state.selected_point = event.coordinate;
        var new_circle = new Feature({ geometry: new Circle(event.coordinate, 5), });
        this.circles_source.clear();
        this.circles_source.addFeature(new_circle);
    }
    GetCurrentCoordinates() {
        return this.state.selected_point;
    }

    componentDidMount() {
        this.map.setTarget("map");
        this.map.on('click', this.OnClickAddCircle.bind(this));

        //// Listen to map changes
        //this.map.on("moveend", () => {
        //    let center = this.map.getView().getCenter();
        //    let zoom = this.map.getView().getZoom();
        //    this.setState({ center, zoom });
        //});
    }

    render() {
        this.updateMap(); // Update map on render?
        return (
            <div id="map" style={{ width: "100%", height: "800px", border: '1px solid black' }}>

            </div>
        );
    }
}