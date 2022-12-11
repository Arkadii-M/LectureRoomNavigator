using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IFacultyEdgeDal
    {
        FacultyDTO GetFacultyForLectureRoom(LectureRoomDTO lectureRoom);
        bool AddLectureRoomToFaculty(LectureRoomDTO lectureRoom, FacultyDTO faculty);
        bool RemoveFacultyEdgeForLectureRoom(LectureRoomDTO lectureRoom, FacultyDTO faculty);
    }
}
