using DAL.Interface;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class LectureRoomDal : ILectureRoomNodeDal
    {
        private static readonly string label = "lectrue_room";
        private GremlinServer _server;
        public LectureRoomDal(GremlinServer server)
        {
            _server = server;
        }

        public LectureRoomDTO AddLectureRoomNode(LectureRoomDTO node)
        {
            var gremlinCode = $@"
				g.addV('{label}')
                    .property('id', '{node.Id}')
                    .property('floor', {node.Floor})
                    .property('x', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.X)})
                    .property('y', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.Y)})
                    .property('number',{node.Number})
                    .property('name',{node.Name})
			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                node.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return node;
        }

        public bool RemoveLectureRoomNode(LectureRoomDTO node)
        {
            return RemoveLectureRoomNodeById(node.Id);
        }

        public bool RemoveLectureRoomNodeById(string id)
        {
            var gremlinCode = $@"g.V('{id}').drop()";
            using (var gremlinClient = new GremlinClient(
                _server,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
            {
                var res = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                return GremlinRequest.IsResponseOk(res.StatusAttributes);
            }
        }

        public bool RemoveAllLectureRoomNodesFromDatabase()
        {
            var gremlinCode = $@"g.V().hasLabel('{label}').drop()";
            using (var gremlinClient = new GremlinClient(
                _server,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
            {
                var res = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                return GremlinRequest.IsResponseOk(res.StatusAttributes);
            }
        }

        public List<LectureRoomDTO> GetAllLectureRoomNodes()
        {
            List<LectureRoomDTO> res = new List<LectureRoomDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('{label}')
			";
            using (var gremlinClient = new GremlinClient(
                            _server,
                            new GraphSON2Reader(),
                            new GraphSON2Writer(),
                            GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                foreach (var node in result)
                {
                    var curr = new LectureRoomDTO();
                    curr.TryParseDynamicToCurrent(node);
                    res.Add(curr);
                }
            }
            return res;
        }

        public LectureRoomDTO GetLectureRoomNodeById(string id)
        {
            LectureRoomDTO res = new LectureRoomDTO();

            var gremlinCode = $@"
				g.V('{id}')
			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                res.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return res;
        }

        public LectureRoomDTO UpdateLectureRoomNode(LectureRoomDTO node)
        {
            var gremlinCode = $@"
				g.V('{node.Id}')
                    .property('x', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.X)})
                    .property('y', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.Y)})
                    .property('number',{node.Number})
                    .property('name',{node.Name})

			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                node.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return node;
        }

        public List<LectureRoomDTO> GetAllLectureRoomNodesOnFloor(uint floor)
        {
            List<LectureRoomDTO> res = new List<LectureRoomDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('{label}').where('floor',{floor})
			";
            using (var gremlinClient = new GremlinClient(
                            _server,
                            new GraphSON2Reader(),
                            new GraphSON2Writer(),
                            GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                foreach (var node in result)
                {
                    var curr = new LectureRoomDTO();
                    curr.TryParseDynamicToCurrent(node);
                    res.Add(curr);
                }
            }
            return res;
        }
    }
}
