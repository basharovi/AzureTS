import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Entity } from './entity.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  readonly baseUrl = "https://localhost:44313/";
  entities: Entity[] = [];

  constructor(private httpClient: HttpClient) { }

  fetchAllData() {

    // this.entities = this.httpClient.get(this.baseUrl + "api/Home/GetAll");
    
    this.httpClient.get(this.baseUrl + "api/Home/GetAll")
      .subscribe(response => this.entities = response as Entity[]
        );
    console.log('requested to api!');
  }
}
