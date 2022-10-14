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
    public class LectureRoomManger : ILectrueRoomManger
    {
        private readonly ILectureRoomNodeDal _lecture_room;

        public LectureRoomManger(ILectureRoomNodeDal lecture_room)
        {
            _lecture_room = lecture_room;
        }

        public LectureRoomDTO AddLectureRoom(LectureRoomDTO node)
        {
            return _lecture_room.AddLectureRoomNode(node);
        }

        public List<LectureRoomDTO> GetAllLectureRoom()
        {
            return _lecture_room.GetAllLectureRoomNodes();
        }

        public List<LectureRoomDTO> GetAllLectureRoomsOnFloor(uint floor)
        {
            return _lecture_room.GetAllLectureRoomNodesOnFloor(floor);
        }

        public LectureRoomDTO GetLectureRoomById(string id)
        {
            return _lecture_room.GetLectureRoomNodeById(id);
        }

        public bool RemoveLectureRoom(LectureRoomDTO node)
        {
            return _lecture_room.RemoveLectureRoomNode(node);
        }

        public bool RemoveLectureRoomById(string id)
        {
            return _lecture_room.RemoveLectureRoomNodeById(id);
        }

        public LectureRoomDTO UpdateLectureRoom(LectureRoomDTO node)
        {
            return _lecture_room.UpdateLectureRoomNode(node);
        }
    }
}
