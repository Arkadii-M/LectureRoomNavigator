using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Vertex
    {
        public string? Id { get; set; }
        public string? Lable { get; set; }
        public string? Type { get; set; }
        public string? PartitionKey { get; set; } = string.Empty;

        //virtual public bool TryParseDynamicToCurrent(ref dynamic? dynamicObject)
        //{
        //    if (dynamicObject == null) return false;

        //    try
        //    {
        //        Id = dynamicObject["id"];
        //        Lable = dynamicObject["label"];
        //        Type = dynamicObject["type"];
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return false;
        //    }
        //    return true;
        //}
    }
}
