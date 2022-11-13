import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { NavigationNode } from '../dto/navigation-node.dto';
import { Observable } from 'rxjs';


@Injectable()
export class NavigationNodeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll(): Observable<NavigationNode[]> {
    return this.http.get<NavigationNode[]>(this.baseUrl + 'api/NavigationNodes');
  }
  addOne(node: NavigationNode) {
    return this.http.post(this.baseUrl + 'api/NavigationNodes',node);
  }

}
