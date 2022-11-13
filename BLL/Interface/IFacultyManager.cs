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
        public List<FacultyDTO> GetAllFaculties();

        public bool AddLectureRoomToFaculty(LectureRoomDTO lectureRoom,FacultyDTO faculty);
    }
}
