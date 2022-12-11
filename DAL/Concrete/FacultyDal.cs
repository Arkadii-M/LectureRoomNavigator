 using DAL.Interface;
using DTO.Vertices;
using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class FacultyDal : IFacultyDal
    {
        private static readonly string label = "faculty";
        private IGremlinClient _client;
        public FacultyDal(IGremlinClient client)
        {
            _client = client;
        }

        public FacultyDTO AddFaculty(FacultyDTO faculty)
        {
            var gremlinCode = $@"
				g.addV('{label}')
                    .property('id','{faculty.Id}')
                    .property('name','{faculty.Name}')
			";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            faculty.TryParseDynamicToCurrent(result.SingleOrDefault());
            return faculty;
        }

        public List<FacultyDTO> GetAllFaculties()
        {
            List<FacultyDTO> res = new List<FacultyDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('{label}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            foreach (var faculty in result)
            {
                var curr = new FacultyDTO();
                curr.TryParseDynamicToCurrent(faculty);
                res.Add(curr);
            }
            return res;
        }

        public FacultyDTO GetFacultyById(string id)
        {
            FacultyDTO res = new FacultyDTO();

            var gremlinCode = $@"
				g.V('{id}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            res.TryParseDynamicToCurrent(result.SingleOrDefault());

            return res;
        }

        public FacultyDTO GetFacultyByName(string name)
        {
            FacultyDTO res = new FacultyDTO();
            var gremlinCode = $@"
				g.V().hasLabel('{label}').has('name','{name}')
			";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            res.TryParseDynamicToCurrent(result.SingleOrDefault());

            return res;
        }

        public bool RemoveFacultyById(string id)
        {
            var gremlinCode = $@"g.V('{id}').drop()";
            var res = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(res.StatusAttributes);
        }

        public FacultyDTO UpdateFaculty(FacultyDTO faculty)
        {
            var gremlinCode = $@"
				g.V('{faculty.Id}')
                    .property('name',{faculty.Name})
			";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            faculty.TryParseDynamicToCurrent(result.SingleOrDefault());
            return faculty;
        }
    }
}
