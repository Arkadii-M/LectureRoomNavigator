import { Faculty } from './faculty.dto';
import { IMapElement } from './map-element'

export class LectureRoom implements IMapElement {
  public id: string = "";
  public name: string = '';
  public faculty: Faculty = new Faculty;
  public numberOfSeats: number = 0;
  public floor: number = 0;
  public x: number = 0;
  public y: number = 0;
}
