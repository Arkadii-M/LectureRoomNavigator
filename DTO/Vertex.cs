using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class Vertex
    {
        public object? Id { get; set; }
        public string? Value { get; set; }
        public string? Lable { get; set; }
        public string? PartitionKey { get; set; } = string.Empty;
    }
}
