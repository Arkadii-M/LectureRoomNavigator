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
        private readonly IFacultyEdgeDal _faculty_edge_dal;
        private readonly IFacultyDal _faculty_dal;

        public LectureRoomManger(ILectureRoomNodeDal lecture_room, IFacultyEdgeDal faculty_edge_dal, IFacultyDal faculty_dal)
        {
            _lecture_room = lecture_room;
            _faculty_edge_dal = faculty_edge_dal;
            _faculty_dal = faculty_dal;
        }

        public LectureRoomDTO AddLectureRoom(LectureRoomDTO node)
        {
            node.Id = Guid.NewGuid().ToString();
            var room = _lecture_room.AddLectureRoomNode(node);
            if(node.Faculty is not null)
            {
                var faculty = _faculty_dal.GetFacultyByName(node.Faculty.Name);
                _faculty_edge_dal.AddLectureRoomToFaculty(node, faculty);
            }
            return room;
        }

        public List<LectureRoomDTO> GetAllLectureRoom()
        {
            var all_rooms = _lecture_room.GetAllLectureRoomNodes();
            all_rooms.ForEach(room => { this.AttachFacultyProperty(ref room);  });
            return all_rooms;
        }

        public List<LectureRoomDTO> GetAllLectureRoomsOnFloor(uint floor)
        {
            //return _lecture_room.GetAllLectureRoomNodesOnFloor(floor);
            throw new NotImplementedException();
        }

        public LectureRoomDTO GetLectureRoomById(string id)
        {
            var room = _lecture_room.GetLectureRoomNodeById(id);
            this.AttachFacultyProperty(ref room);
            return room;
        }

        public List<LectureRoomDTO> GetLectureRoomsbyIds(string[] ids)
        {
            var res = new List<LectureRoomDTO>();
            foreach (var id in ids)
                res.Add(_lecture_room.GetLectureRoomNodeById(id));
            return res;
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

        private void AttachFacultyProperty(ref LectureRoomDTO room)
        {
            room.Faculty = this._faculty_edge_dal.GetFacultyForLectureRoom(room);
        }
    }
}
