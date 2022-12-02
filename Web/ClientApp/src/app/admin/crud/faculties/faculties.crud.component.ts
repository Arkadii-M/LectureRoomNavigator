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

  constructor(
    private faculty_service: FacultyService) {

    this.faculty_service.getAll().subscribe(results => { // read all faculties from database
      this.faculties = results;
      console.log(results);
    }, err => { console.error(err); });
  }

  ngOnInit(): void {
  }
}
