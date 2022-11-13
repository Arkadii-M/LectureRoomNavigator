using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Interface
{
    public interface IMapElement
    {
        public uint Floor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
