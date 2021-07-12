import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  readonly baseUrl = "https://localhost:44313/";

  constructor(private httpClient: HttpClient) { }

  getAllData(){
    this.httpClient.get(this.baseUrl + "api/Home/GetAll")
  }
}
