using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Core;

namespace Couchbase.ViewExamples
{
    class Program
    {
        static Cluster _cluster = new Cluster();

        static void Main(string[] args)
        {
            using (var bucket = _cluster.OpenBucket("beer-sample"))
            {
                BasicQuery(bucket);
            }
            _cluster.Dispose();
            Console.Read();
        }

        static void BasicQuery(IBucket bucket)
        {
            //replace this line and create a query object and then get the results from it. 
            var result = new { Rows = new List<string>() };
            foreach (var row in result.Rows)
            {
                Console.WriteLine(row);
            }
        }
    }
}
