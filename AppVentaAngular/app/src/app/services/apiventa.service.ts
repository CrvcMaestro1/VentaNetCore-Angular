import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Venta } from '../models/venta';
import { Response } from '../models/response';

const httpOption = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ApiventaService {

  url: string = 'https://localhost:44346/api/venta';

  constructor(private _http: HttpClient) { }

  add(venta: Venta): Observable<Response> {
    return this._http.post<Response>(this.url, venta, httpOption);
  }
}
