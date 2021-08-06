import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Entity, EntityVm } from './entity.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  readonly baseUrl = "https://localhost:44313/";
  entities: Entity[] = [];
  entityVm: EntityVm = new EntityVm();
  names: any = [];
  tableName: string = 'VTSolo';
  dataLoaded: Promise<boolean> = Promise.resolve(false);

  constructor(private httpClient: HttpClient) { }

  fetchAllData() {

    this.httpClient.get(this.baseUrl + "api/AzureTable/FetchData" + "?tableName=" + this.tableName)
      .subscribe((response) => {
        this.entities = response as Entity[];

        this.dataLoaded = Promise.resolve(true);
      });
  }

  fetchFilteredData() {

    var hitUrl = this.baseUrl + "api/AzureTable/FetchData" + "?tableName=" + this.tableName +"&" + this.toQueryString(this.entityVm);
    console.log(hitUrl);
    this.entities = [];

    this.httpClient.get(hitUrl)
    .subscribe((response) => {
      this.entities = response as Entity[];

      this.dataLoaded = Promise.resolve(true);
    });
        
  }

  fetchNames() {

    this.httpClient.get(this.baseUrl + "api/AzureTable/FetchNames" + "?tableName="+ this.tableName)
      .subscribe(response => this.names = response as []
        );
  }

  toQueryString(obj: { [x: string]: any; }){
    let parts = [];
    for(const prop in obj){
      const value = obj[prop];

      if(value != null && value !== undefined && value != ""){
        parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
      }

    }
    return parts.join('&');
  }
}
