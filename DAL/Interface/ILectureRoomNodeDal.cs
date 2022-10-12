using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    internal interface ILectureRoomNodeDal
    {
        LectureRoomDTO AddLectureRoomNode(LectureRoomDTO node);

        LectureRoomDTO GetLectureRoomNodeById(string id);
        List<LectureRoomDTO> GetAllLectureRoomNodes();
        List<LectureRoomDTO> GetAllLectureRoomNodesOnFloor(uint floor);

        LectureRoomDTO UpdateLectureRoomNode(LectureRoomDTO node);
        bool RemoveLectureRoomNode(LectureRoomDTO node);
        bool RemoveLectureRoomNodeById(string id);

        bool RemoveAllLectureRoomNodesFromDatabase();
    }
}
