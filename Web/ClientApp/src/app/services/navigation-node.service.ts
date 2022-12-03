import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { NavigationNode } from '../dto/navigation-node.dto';
import { Observable } from 'rxjs';


@Injectable()
export class NavigationNodeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private formHeader(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + (localStorage.getItem('token') ?? '')
    }); }
  getAll(): Observable<NavigationNode[]> {
    return this.http.get<NavigationNode[]>(this.baseUrl + 'api/NavigationNodes');
  }
  addOne(node: NavigationNode) {
    return this.http.post(this.baseUrl + 'api/NavigationNodes', node, { headers: this.formHeader() });
  }
  Update(node: NavigationNode) {
    return this.http.put(this.baseUrl + 'api/NavigationNodes', node, { headers: this.formHeader() });
  }
  Delete(node_id: string) {
    let httpParams = new HttpParams().set('id', node_id);
    return this.http.delete(this.baseUrl + 'api/NavigationNodes', { params: httpParams, headers: this.formHeader() });
  }
  GetEnterNode() {
    return this.http.get<NavigationNode>(this.baseUrl + 'api/NavigationNodes/university_enter');
  }

}
