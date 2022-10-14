using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ILectrueRoomManger
    {
        LectureRoomDTO AddLectureRoom(LectureRoomDTO node);

        LectureRoomDTO GetLectureRoomById(string id);
        List<LectureRoomDTO> GetAllLectureRoom();
        List<LectureRoomDTO> GetAllLectureRoomsOnFloor(uint floor);

        LectureRoomDTO UpdateLectureRoom(LectureRoomDTO node);
        bool RemoveLectureRoom(LectureRoomDTO node);
        bool RemoveLectureRoomById(string id);
    }
}
