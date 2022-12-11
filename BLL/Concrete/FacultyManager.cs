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
            faculty.Id = Guid.NewGuid().ToString();
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

        public FacultyDTO GetFacultyById(string id)
        {
            return _faculty_dal.GetFacultyById(id);
        }

        public bool RemoveFaculty(FacultyDTO faculty)
        {
            return this.RemoveFacultyById(faculty.Id);
        }

        public bool RemoveFacultyById(string id)
        {
            return _faculty_dal.RemoveFacultyById(id);
        }

        public FacultyDTO UpdateFacluty(FacultyDTO faculty)
        {
            return _faculty_dal.UpdateFaculty(faculty);
        }
    }
}
