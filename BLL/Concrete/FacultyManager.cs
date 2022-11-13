using BLL.Interface;
using DAL.Interface;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class FacultyManager : IFacultyManager
    {

        private readonly IFacultyEdgeDal _faculty_edge_dal;
        private readonly IFacultyDal _faculty_dal;

        public FacultyManager(IFacultyDal facultyDal, IFacultyEdgeDal faculty_edge_dal)
        {
            _faculty_dal = facultyDal;
            _faculty_edge_dal = faculty_edge_dal;
        }
        public FacultyDTO AddFaculty(FacultyDTO faculty)
        {
            return this._faculty_dal.AddFaculty(faculty);
        }

        public bool AddLectureRoomToFaculty(LectureRoomDTO lectureRoom, FacultyDTO faculty)
        {
            return this.AddLectureRoomToFaculty(lectureRoom, faculty);
        }

        public List<FacultyDTO> GetAllFaculties()
        {
            return this._faculty_dal.GetAllFaculties();
        }

        public bool RemoveFaculty(FacultyDTO faculty)
        {
            return this._faculty_dal.RemoveFacultyById(faculty.Id);
        }
    }
}
