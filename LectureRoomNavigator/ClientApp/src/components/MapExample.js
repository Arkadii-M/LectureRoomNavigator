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
export class MapExample extends Component {
    constructor(props) {
        super(props);

        this.state = { center: [0, 0], zoom: 1 };

        this.mousePositionControl = new MousePosition({
            coordinateFormat: createStringXY(4),
            projection: 'xkcd-image',
            // comment the following two lines to have the mouse position
            // be placed within the map.
            className: 'custom-mouse-position',
            target: document.getElementById('mouse-position'),
        });


        this.map = new Map({
            controls: defaultControls().extend([this.mousePositionControl]),
            target: null,
            layers: [
                new ImageLayer(
                    {
                        source: new Static(
                            {
                                /*                                imageLoadFunction: this.load_image,*/
                                url: 'ukraine.svg',
                                imageExtent: [0, 0, 612.47321, 408.0199],
                            }),

                    }),
                new VectorLayer(
                    {
                        source: new VectorSource(
                            {
                                features: [new Feature({
                                    geometry: new Circle([60, 270], 5)
                                }),]
                            }
                        )
                    }),
            ],
            view: new View({
                projection: new Projection({
                    code: 'xkcd-image',
                    units: 'm',
                    extent: [0, 0, 612.47321, 408.0199],
                }),
                zoom: 2,
                maxZoom: 8,
                center: getCenter([0, 0, 612.47321, 408.0199]),
                zoom: this.state.zoom
            })
        });

    }

    updateMap() {
        this.map.getView().setCenter(this.state.center);
        this.map.getView().setZoom(this.state.zoom);
    }

    componentDidMount() {
        this.map.setTarget("map");

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
            <div id="map" style={{ width: "100%", height: "500px" }}>

            </div>
        );
    }
}
