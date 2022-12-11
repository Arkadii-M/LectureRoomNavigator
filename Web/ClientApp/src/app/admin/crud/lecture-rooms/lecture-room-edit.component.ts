import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Floor, MapComponent } from '../../../map/map.component';
import { LectureRoomService } from '../../../services/lecture-room.service'
import { LectureRoom } from '../../../dto/lectrure-room.dto'
import { Faculty } from '../../../dto/faculty.dto';
import { FacultyService } from '../../../services/faculty.service';

@Component({
  selector: 'lecture-room-edit',
  templateUrl: './lecture-room-edit.component.html',
  styleUrls: ['./lecture-room-edit.component.css'],
  providers: [LectureRoomService, FacultyService]
})
export class LectureRoomEditComponent {
  selectedOption: string = '';
  floors: any[] = [
    { value: 0, viewValue: 'Підвал' },
    { value: 1, viewValue: 'Перший' },
    { value: 2, viewValue: 'Другий' },
    { value: 3, viewValue: 'Третій' },
    { value: 4, viewValue: 'Четвертий' },
  ];
  current_floor_object: any = this.floors[1];

  public lecture_rooms: LectureRoom[] = [];
  public faculties: Faculty[] = [];

  public to_update_room: LectureRoom = new LectureRoom;

  current_floor: number = Floor.FirstFloor;
  readonly show_rooms: boolean = true;
  data_ready: boolean = false;
  click_coordinates: number[] = [];

  private GetAllLectureRooms() {
    this.lectrue_room_service.getAll().subscribe(results => {
      this.lecture_rooms = results;
      this.data_ready = true;
    }, err => { console.error(err); });

  }

  constructor(
    private lectrue_room_service: LectureRoomService,
    private faculty_service: FacultyService) {

    this.faculty_service.getAll().subscribe(results => {
      this.faculties = results;
      this.setFaculty(this.faculties[0]);
    }, err => { console.error(err); });
  }

  ngOnInit(): void {
    this.GetAllLectureRooms();
  }
  setFloor(event: any) {
    this.current_floor = Number(event.value.value);
    this.to_update_room.id = '';
  }
  setFaculty(event: any) {
    if (this.to_update_room)
      this.to_update_room.faculty = event.value;
  }
  ClickOnRoomNodeHandle(room_id: string) {
    let selected = this.lecture_rooms.find((lr) => { return lr.id == room_id; });
    if (!selected)
      throw new Error("Selected node is not found in list");

    this.to_update_room = selected;
  }
  update() {
    if (this.to_update_room.id != '')
      this.lectrue_room_service.Update(this.to_update_room).subscribe(finish => { this.GetAllLectureRooms(); }, err => { console.log(err); });
  }

  delete() {
    if (this.to_update_room.id != '')
      this.lectrue_room_service.Delete(this.to_update_room.id).subscribe(finish => { this.GetAllLectureRooms(); }, err => { console.log(err); });
  }

}
