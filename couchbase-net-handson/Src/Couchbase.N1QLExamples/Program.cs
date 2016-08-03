using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Couchbase.N1QLExamples
{
    class Program
    {
        static Cluster _cluster = new Cluster();

        static void Main(string[] args)
        {
            //initialize your cluster here. 
            using (var bucket = _cluster.OpenBucket())
            {
                //play around with N1QL queries here! 
                const string query = "SELECT c FROM default as c";
                var result = bucket.Query<dynamic>(query);
                foreach (var row in result.Rows)
                {
                    Console.WriteLine(row);
                }
            }
            _cluster.Dispose();
            Console.Read();
        }
    }
}
