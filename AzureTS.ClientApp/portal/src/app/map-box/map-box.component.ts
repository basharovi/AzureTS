import { Component, OnInit } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { NgxSpinnerService } from 'ngx-spinner';
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
  selectedDates: any;

  constructor(private apiService: ApiService,
    private spinner: NgxSpinnerService) {
    this.mapConstants = new MapConstants();
    this.apiService.fetchNames();
    this.apiService.fetchAllData();
  }

  ngOnInit(): void {
    this.initializeMap(mapConst.long, mapConst.lat);

    this.mapEntites();

    this.ShowSpinner(true);
  }

  isInvalidDate(date: { weekday: () => number; }) {
    return date.weekday() === 0;
  }

  delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

  initializeMap(lng: number, lat: number) {

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

  ShowSpinner(input: boolean){
    if(input)
      this.spinner.show();
    else
      this.spinner.hide();
  }

  addGeoJsonLine() {
    
    this.map.addSource('route', this.mapConstants.geoJsonObject);
    console.log('Source Added');

    this.map.addLayer(this.mapConstants.lineObject);
    console.log('Layer Added');
  }

  async mapEntites() {
    
    while (await this.apiService.dataLoaded !== true){
        await this.delay(3 * 1000);
    }

    this.apiService.dataLoaded = Promise.resolve(false);

    const cordinates = [] as any;

    let lngSum = 0;
    let latSum = 0;
    this.apiService.entities.forEach(entity => {
      lngSum += entity.lng;
      latSum += entity.lat;
      cordinates.push([entity.lng, entity.lat]);
    });

    this.mapConstants.geoJsonObject.data.geometry.coordinates = cordinates;

    if(cordinates.length < 1){
      this.flyToTheLocation(mapConst.long, mapConst.lat);
    }
    else{
      let lngAvg = lngSum / cordinates.length;
      let latAvg = latSum / cordinates.length;
     
      this.flyToTheLocation(lngAvg, latAvg);
    }

    
    this.addGeoJsonLine();

    this.ShowSpinner(false);
  }

  onClickRemove = () => {
    this.map.removeLayer('route');
    this.map.removeSource('route');
  };

  loadNames(){
    return this.apiService.names;
  }

  filterMapData(){
    this.ShowSpinner(true);

    this.apiService.entityVm.name = this.inputName;

    this.apiService.entityVm.fromDate = this.selectedDates.startDate?.toJSON();
    this.apiService.entityVm.toDate = this.selectedDates.endDate?.toJSON();

    this.apiService.fetchFilteredData();
   
    this.onClickRemove();

    this.mapEntites();
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
