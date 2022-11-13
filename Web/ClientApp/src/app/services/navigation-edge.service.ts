import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NavigationEdge } from '../dto/navigation-edge.dto';
import { Observable } from 'rxjs';


@Injectable()
export class NavigationEdgeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll(): Observable<NavigationEdge[]> {
    return this.http.get<NavigationEdge[]>(this.baseUrl + 'api/NavigationEdges');
  }

  addOne(edge: NavigationEdge) {
    return this.http.post(this.baseUrl + 'api/NavigationEdges', edge);
  }
}
