import { Component, OnInit } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { ApiService } from '../shared/api.service';
import { EntityVm } from '../shared/entity.model';
import { MapConstants, mapConstants as mapConst } from './../environments/environment';

@Component({
  selector: 'app-map-box',
  templateUrl: './map-box.component.html',
  styleUrls: ['./map-box.component.css']
})
export class MapBoxComponent implements OnInit {

  map: any;
  mapConstants: MapConstants;
  listOfNames: any = [];

  inputName: string = "";
  inputTime: Date | undefined;

  constructor(private apiService: ApiService) {
    this.mapConstants = new MapConstants();
    this.apiService.fetchAllData();
    this.apiService.fetchNames();
  }

  ngOnInit(): void {
    this.initializeMap();

    this.mapEntites();
  }

  delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

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

  config = {
    displayKey: "name", // if objects array passed which key to be displayed defaults to description
    search: true,
    limitTo: 10,
  };

  addGeoJsonLine() {

    this.map.addSource('route', this.mapConstants.geoJsonObject);
    console.log('Source Added');

    this.map.addLayer(this.mapConstants.lineObject);
    console.log('Layer Added');
  }

  async mapEntites() {

    // wait until backend datas are processed;
    if(this.apiService.entities.length < 1){
      await this.delay(2 * 1000);
      this.mapEntites();
      return;
    }
    const cordinates = [] as any;

    this.apiService.entities.forEach(entity => {
      cordinates.push([entity.lng, entity.lat]);
    });

    this.mapConstants.geoJsonObject.data.geometry.coordinates = cordinates;

    this.addGeoJsonLine();
  }

  onClick = () => {
    // this.mapEntites();
  };

  onClickRemove = () => {
    this.map.removeLayer('route');
    this.map.removeSource('route');
  };

  loadNames(){
    return this.apiService.names;
  }

  filterMapData(){
    this.apiService.entityVm.name = this.inputName;
    
    if(this.inputTime != null )
      this.apiService.entityVm.time = this.inputTime?.toISOString();

    this.apiService.fetchFilteredData();
  }
}
