import { Component, OnInit } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { ApiService } from '../shared/api.service';
import { MapConstants, mapConstants as mapConst } from './../environments/environment';

@Component({
  selector: 'app-map-box',
  templateUrl: './map-box.component.html',
  styleUrls: ['./map-box.component.css']
})
export class MapBoxComponent implements OnInit {

  map: any;
  cordinates: [number[]] = [[]];
  mapConstants: MapConstants;

  constructor(private apiService: ApiService) {
    this.mapConstants = new MapConstants();
  }

  ngOnInit(): void {
    this.initializeMap();

    this.apiService.fetchAllData();

    // this.mapEntites();

    // this.addGeoJsonLine();
  }

  initializeMap() {

    this.map = new mapboxgl.Map({
      accessToken: mapConst.accessToken,
      container: "map",
      style: mapConst.style,
      center: [mapConst.long, mapConst.lat],
      zoom: mapConst.mapZoom,
      attributionControl: false
    });

  }

  addGeoJsonLine() {

    this.map.addSource('route', this.mapConstants.geoJsonObject);
    this.map.addLayer(this.mapConstants.lineObject);

    this.map.on('load', () => {
    });
  }

  mapEntites() {
    // let mapped = this.apiService.entities.map(({ lat, lng }) => ({ lng, lat }));
    // this.mapConstants.geoJsonObject.data.geometry.coordinates = mapped;

    const cordinates = [] as any;
    let entities = this.apiService.entities;

    for (let i = 0; i < 10; i++) {
      cordinates.push([entities[i].lng, entities[i].lat]);
    }

    this.mapConstants.geoJsonObject.data.geometry.coordinates = cordinates;
  }

  onClick() {
    this.mapEntites();

    this.addGeoJsonLine();
  }

}
