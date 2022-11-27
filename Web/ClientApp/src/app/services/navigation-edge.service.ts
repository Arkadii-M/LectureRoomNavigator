import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NavigationEdge } from '../dto/navigation-edge.dto';
import { Observable } from 'rxjs';


@Injectable()
export class NavigationEdgeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private formHeader(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + (localStorage.getItem('token') ?? '')
    });
  }
  getAll(): Observable<NavigationEdge[]> {
    return this.http.get<NavigationEdge[]>(this.baseUrl + 'api/NavigationEdges');
  }

  addOne(edge: NavigationEdge) {
    return this.http.post(this.baseUrl + 'api/NavigationEdges', edge, { headers: this.formHeader() });
  }
}
