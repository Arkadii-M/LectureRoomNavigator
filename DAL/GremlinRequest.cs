using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GremlinRequest
    {
        public static Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, string query)
        {
            try
            {
                return gremlinClient.SubmitAsync<dynamic>(query);
            }
            catch (ResponseException e)
            {
                Console.WriteLine("\tRequest Error!");

                // Print the Gremlin status code.
                Console.WriteLine($"\tStatusCode: {e.StatusCode}");
                throw;
            }
        }
        public static string ConvertDoubleToIntegerExpNotation(double value) // Cosmos DB emulator throw error when parsing floats in query. But exponential notation works ok. TODO: test on web version
        {
            var value_split = value.ToString().Split(',');
            return (value_split.Length == 2) ? (value_split[0] + value_split[1] + "e-" + value_split[1].Length) : value_split[0];
        }
    }
}
