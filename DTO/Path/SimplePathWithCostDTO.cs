using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Path
{
    public class SimplePathWithCostDTO : IDynamicParse
    {
        public List<Tuple<double, SimplePathDTO>> PathesWithCost { get; set; } = new List<Tuple<double, SimplePathDTO>>();
        public bool IsAnyPathExists => PathesWithCost.Any();
        public Tuple<double, SimplePathDTO>? GetPathWithMinimumCost() => PathesWithCost.MinBy(key => key.Item1);

        public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;

            try
            {
                double curr_cost = Convert.ToDouble(dynamicObject["cost"]);
                SimplePathDTO s_path = new SimplePathDTO();
                if(s_path.TryParseDynamicToCurrent(dynamicObject["path"]))
                    PathesWithCost.Add(new Tuple<double, SimplePathDTO>(curr_cost, s_path));
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
