import { Component, Inject, OnInit } from '@angular/core';
import { Faculty } from '../../../dto/faculty.dto';
import { FacultyService } from '../../../services/faculty.service';

@Component({
  selector: 'faculties-crud',
  templateUrl: './faculties.crud.component.html',
  styleUrls: ['./faculties.crud.component.css'],
  providers: [FacultyService]
})
export class FacultiesCRUDComponent {

  public faculties: Faculty[] = [];
  public faculty: Faculty = new Faculty;
  public submitted: boolean = false;
  public addDialog: boolean = false;

  constructor(
    private faculty_service: FacultyService) {
  }
  UpdateFaculties() {
    this.faculty_service.getAll().subscribe(results => { // read all faculties from database
      this.faculties = results;
      console.log(results);
    }, err => { console.error(err); });
  }
  ngOnInit(): void {
    this.UpdateFaculties();
  }
  delete(del_faculty: Faculty) {
    this.faculty_service.Delete(del_faculty.id).subscribe(res => { this.UpdateFaculties() });
  }

  openNew() {
    this.faculty = new Faculty;
    this.submitted = false;
    this.addDialog = true;
  }
  hideDialog() {
    this.addDialog = false;
    this.submitted = false;
  }

  saveFaculty() {
    this.submitted = true;
    this.addDialog = false;

    if (this.faculty.name.length < 3) {
      console.error("Invalid name length!")
      return;
    }
    this.faculty_service.addOne(this.faculty).subscribe(res => this.UpdateFaculties());
  }
}
