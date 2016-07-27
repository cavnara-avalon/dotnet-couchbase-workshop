using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Datageneration;

namespace CouchbaseNETDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionManager.InitializeConfig("http://localhost:8091");

            var customers = BulkGenerator.GetCustomers(1000);

            customers.ToList().ForEach(
                c =>
                {
                    Console.WriteLine($"First Name: {c.FirstName}");
                    Console.WriteLine($"Last Name: {c.LastName}");
                    Console.WriteLine($"Customer Id: {c.CustomerId}");
                    Console.WriteLine($"Email Address: {c.EmailAddress}");
                    Console.WriteLine();


                }
            );

            Console.ReadLine();
        }
    }

    class ConnectionManager
    {
        public static void InitializeConfig(string uri)
        {
            var config = new ClientConfiguration
            {
                Servers = new List<Uri>()
                {
                    new Uri(uri)
                },
                UseSsl = false
            };

            ClusterHelper.Initialize(config);
        }
    }
}
