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

  constructor(private httpClient: HttpClient) { }

  fetchAllData() {

    this.httpClient.get(this.baseUrl + "api/AzureTable/FetchData" + "?tableName=VTMagtec")
      .subscribe(response => this.entities = response as Entity[]
        );
  }

  toQueryString(obj: { [x: string]: any; }){
    let parts = [];
    for(const prop in obj){
      const value = obj[prop];

      if(value != null && value !== undefined){
        parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
      }

    }
    return parts.join('&');
  }
}
