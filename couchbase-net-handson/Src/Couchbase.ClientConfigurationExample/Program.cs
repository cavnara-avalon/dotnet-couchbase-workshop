using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using Couchbase.Core;

namespace Couchbase.ClientConfigurationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    /* place your server addresses here */
                },
            };

            using (var cluster = new Cluster(config))
            {
                Console.WriteLine("Cluster initialized");
                IBucket bucket = null;
                try
                {
                    bucket = cluster.OpenBucket();
                    //use the bucket here
                }
                finally
                {
                    if (bucket != null)
                    {
                        cluster.CloseBucket(bucket);
                        Console.WriteLine("Bucket disposed");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
    