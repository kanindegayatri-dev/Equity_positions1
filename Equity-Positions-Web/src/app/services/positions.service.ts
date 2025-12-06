import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Position {
  securityCode: string;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class PositionsService {

  private apiUrl = 'https://localhost:44385/api/positions';
  constructor(private http: HttpClient) {}
  
  getCurrentPositions(): Observable<Position[]> {
    return this.http.get<Position[]>(`${this.apiUrl}/current`);
  }
}
