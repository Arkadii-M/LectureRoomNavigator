import { IMapElement } from './map-element'

export class NavigationNode implements IMapElement {
  public id: string = '';
  public floor: number = 0;
  public x: number = 0;
  public y: number = 0;
}
