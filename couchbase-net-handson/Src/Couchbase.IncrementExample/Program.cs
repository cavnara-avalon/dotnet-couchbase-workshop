using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Core;

namespace Couchbase.IncrementExample
{
    class Program
    {
        static Cluster _cluster = new Cluster();

        static void Main(string[] args)
        {
            //Initialize your Cluster here. 
            using (var bucket = _cluster.OpenBucket())
            {
                var key = "stats::counter1";
                Increment(bucket, key);
                Increment(bucket, key);
                Decrement(bucket, key);
                Decrement(bucket, key);
                Decrement(bucket, key);
            }
            _cluster.Dispose();
            Console.Read();
        }

        static void Increment(IBucket bucket, string key)
        {
            //replace this line with one that uses Increment on the key given. 
            var result = new { Success = false, Value="Foo" };
            if (result.Success)
            {
                Console.WriteLine(result.Value);
            }
        }

        static void Decrement(IBucket bucket, string key)
        {
            //replace this line with one that uses Decrement on the key given. 
            var result = new { Success = false, Value = "Foo" };
            if (result.Success)
            {
                Console.WriteLine(result.Value);
            }
        }
    }
}
