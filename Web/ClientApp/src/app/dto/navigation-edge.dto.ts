import { IMapElement } from './map-element'

export class NavigationEdge {
  public id: string = '';
  public inVertexId: string = '';
  public outVertexId: string = '';
  public distance: number = 0;
  public inElement?: IMapElement;
  public outElement?: IMapElement;
}
