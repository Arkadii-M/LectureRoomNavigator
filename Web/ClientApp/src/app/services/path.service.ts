import { Injectable, Inject, SimpleChange } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SimplePath } from '../dto/simple-path.dto';


@Injectable()
export class PathService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getOptimalPath(from_id: string, to_id: string): Observable<SimplePath> {
    let params = new HttpParams();
    params = params.append('from_id', from_id);
    params = params.append('to_id', to_id);
    return this.http.get<SimplePath>(this.baseUrl + "api/Path", { params: params });
  }
}
