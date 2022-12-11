using DAL.Interface;
using DTO.Edges;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class FacultyEdgeDal : IFacultyEdgeDal
    {
        private static readonly string label = "from_faculty";
        private IGremlinClient _client;
        public FacultyEdgeDal(IGremlinClient client)
        {
            _client = client;
        }

        public bool AddLectureRoomToFaculty(LectureRoomDTO lectureRoom, FacultyDTO faculty)
        {
            var gremlinCode = $@"
                        g.V('{lectureRoom.Id}')
                        .addE('{label}')
                        .to(g.V('{faculty.Id}'))
			";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(result.StatusAttributes);
        }

        public FacultyDTO GetFacultyForLectureRoom(LectureRoomDTO lectureRoom)
        {
            FacultyDTO res = new FacultyDTO();
            var gremlinCode = $@"g.V('{lectureRoom.Id}').outE('{label}').inV()";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            res.TryParseDynamicToCurrent(result.SingleOrDefault());
            return res;
        }
        public bool RemoveFacultyEdgeForLectureRoom(LectureRoomDTO lectureRoom, FacultyDTO faculty)
        {
            var gremlinCode = $@"g.V('{lectureRoom.Id}').bothE('{label}').where(otherV().hasId('{faculty.Id}')).drop()";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(result.StatusAttributes);
        }
    }
}
