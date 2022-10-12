using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Edges
{
    public class Edge
    {
        public string Id { get; set; } = string.Empty;
        public string? Lable { get; set; }
        public string? Type { get; set; }
        public string? InVertexLable { get; set; }
        public string? OutVertexLable { get; set; }
        public string? InVertexId { get; set; }
        public string? OutVertexId{ get; set; }
    }
}
