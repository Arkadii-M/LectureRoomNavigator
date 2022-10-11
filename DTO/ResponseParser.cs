using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    internal class ResponseParser : IResponseParser
    {
        public class VertexData: Vertex
        {
            public object Properties { get; set; }
        }
        public T ParseToObject<T>(string response)
        {
            //T? result = default(T);

            //if(typeof(T) == typeof(Vertex)) // Parse vertex
            //{
            //    result = JsonConvert.DeserializeObject<T>(response);
            //}
            //else if(typeof(T) == typeof(Edge))
            //{
            //}
            throw new NotImplementedException();
        }
    }

}
