using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal interface IResponseParser
    {
        T ParseToObject<T>(string response);
    }
}
