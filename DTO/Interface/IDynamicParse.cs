using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Interface
{
    public interface IDynamicParse
    {
        bool TryParseDynamicToCurrent(dynamic? dynamicObject);
    }
}
