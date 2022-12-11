import { Component, Inject, ViewChild, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Floor, MapComponent } from '../../../map/map.component';
import { LectureRoomService } from '../../../services/lecture-room.service'
import { LectureRoom } from '../../../dto/lectrure-room.dto'
import { Faculty } from '../../../dto/faculty.dto';
import { FacultyService } from '../../../services/faculty.service';

@Component({
  selector: 'lecture-room-add',
  templateUrl: './lecture-room-add.component.html',
  styleUrls: ['./lecture-room-add.component.css'],
  providers: [LectureRoomService, FacultyService]
})
export class LectureRoomAddComponent {
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

  public to_add_room: LectureRoom;

  current_floor: number = Floor.FirstFloor;
  readonly show_rooms: boolean = true;
  data_ready: boolean = false;
  click_coordinates: number[] = [];

  private UpdateLectureRooms() {
    this.lectrue_room_service.getAll().subscribe(results => {
      this.lecture_rooms = results;
      this.data_ready = true;
    }, err => { console.error(err); });

  }

  constructor(
    private lectrue_room_service: LectureRoomService,
    private faculty_service: FacultyService) {
    this.to_add_room = new LectureRoom;
    this.to_add_room.faculty = new Faculty;

    this.faculty_service.getAll().subscribe(results => {
      this.faculties = results;
      this.setFaculty(this.faculties[0]);
    }, err => { console.error(err); });
  }

  ngOnInit(): void {
    this.UpdateLectureRooms();
    this.to_add_room.floor = this.current_floor;
  }

  setFloor(event: any) {
    this.current_floor = Number(event.value.value);
    this.to_add_room.floor = this.current_floor;
    this.to_add_room.x = 0;
    this.to_add_room.y = 0;
  }
  setFaculty(event: any) {
    this.to_add_room.faculty = event.value;
  }
  ChildEventHandler(value: number[]) {
    this.to_add_room.x = value[0];
    this.to_add_room.y = value[1];
  }
  sumbit() {
    this.lectrue_room_service.addOne(this.to_add_room).subscribe(finish => { this.UpdateLectureRooms(); }, err => { console.log(err); });
  }

}
