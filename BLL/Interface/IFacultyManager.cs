using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IFacultyManager
    {
        public FacultyDTO AddFaculty(FacultyDTO faculty);
        public bool RemoveFaculty(FacultyDTO faculty);
        public bool RemoveFacultyById(string id);
        public List<FacultyDTO> GetAllFaculties();
        public FacultyDTO GetFacultyById(string id);
        public FacultyDTO UpdateFacluty(FacultyDTO faculty);

        public bool AddLectureRoomToFaculty(LectureRoomDTO lectureRoom,FacultyDTO faculty);
    }
}
