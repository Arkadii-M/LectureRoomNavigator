using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vertices
{
    public class Vertex :IDynamicParse
    {
        public string Id { get; set; } = string.Empty;
        public string? Lable { get; set; }
        public string? Type { get; set; }
        public string? PartitionKey { get; set; } = string.Empty;
        public object? Properties { get; set; }

        public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;

            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                Properties = dynamicObject["properties"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
}
