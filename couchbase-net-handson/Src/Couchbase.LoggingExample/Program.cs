using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couchbase.LoggingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //insert a ClientConfiguration element here for Cluster initialization. 
            using (var cluster = new Cluster())
            {
                using (var bucket = cluster.OpenBucket())
                {
                    bucket.Upsert("somekey", "somevalue");
                }
            }
            Console.Read();
        }
    }
}
