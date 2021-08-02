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
    this.apiService.fetchNames();
    this.apiService.fetchAllData();
  }

  ngOnInit(): void {
    this.initializeMap(mapConst.long, mapConst.lat);

    this.mapEntites(false);

    
  }

  delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

  initializeMap(lng: number, lat: number) {

    // this.mapConstants = new MapConstants();

    this.map = new mapboxgl.Map({
      accessToken: mapConst.accessToken,
      container: "map",
      style: mapConst.style,
      center: [lng, lat],
      zoom: mapConst.mapZoom,
      attributionControl: false
    });

    // Add zoom and rotation controls to the map.
    this.map.addControl(new mapboxgl.NavigationControl());

  }

  addGeoJsonLine(isFilter: boolean) {
    if(isFilter)
      this.onClickRemove();

    this.map.addSource('route', this.mapConstants.geoJsonObject);
    console.log('Source Added');

    this.map.addLayer(this.mapConstants.lineObject);
    console.log('Layer Added');
  }

  async mapEntites(isFilter: boolean) {

    // wait until backend datas are processed;
    if(this.apiService.entities.length < 1){
      await this.delay(2 * 1000);
      this.mapEntites(isFilter);
      return;
    }
    const cordinates = [] as any;

    let lngSum = 0;
    let latSum = 0;
    this.apiService.entities.forEach(entity => {
      lngSum += entity.lng;
      latSum += entity.lat;
      cordinates.push([entity.lng, entity.lat]);
    });

    this.mapConstants.geoJsonObject.data.geometry.coordinates = cordinates;

    let lngAvg = lngSum / cordinates.length;
    let latAvg = latSum / cordinates.length;
   
    // this.initializeMap(lngAvg, latAvg);
    this.flyToTheLocation(lngAvg, latAvg);
    this.addGeoJsonLine(isFilter);
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

    this.mapEntites(true);
  }

  flyToTheLocation(lng: number, lat: number){
    this.map.flyTo({
      center: [
      lng, lat
      ],
      essential: true
      });
  }
}
