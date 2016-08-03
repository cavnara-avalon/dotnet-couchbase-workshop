using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Annotations;
using Couchbase.Core;
using Couchbase.Core.Buckets;

namespace Couchbase.DocumentExamples
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }

    class Program
    {
        static Cluster _cluster = new Cluster();
       
        static void Main(string[] args)
        {
            /* initialize the Cluster here. */
            using (var bucket = _cluster.OpenBucket())
            {
                /* place a breakpoint here to watch the fun! */
                var document = InsertDocument(bucket);
                GetDocument(bucket, document.Id);
                ReplaceDocument(bucket, document);
                DeleteDocument(bucket, document);
            }
            _cluster.Dispose();
            Console.Read();
        }

        static Document<Person> InsertDocument(IBucket bucket)
        {
            var document = new Document<Person>
            {
                Id = "P1",
                Content = new Person
                {
                    FirstName = "John",
                    LastName = "Adams",
                    Age = 21
                }
            };

            // insert the document here 
            var result = new { Success = false };
            if (result.Success)
            {
                Console.WriteLine("Inserted document '{0}'", document.Id);
            }
            return document;
        }

        static void GetDocument(IBucket bucket, string id)
        {
            //get the document here
            var result = new { Success = false,  Content = new { FirstName = String.Empty,  LastName=String.Empty}};
            if (result.Success)
            {
                var person = result.Content;
                Console.WriteLine("Retrieved document '{0}': {1} {2}", id, person.FirstName, person.LastName);
            }
        }

        private static void ReplaceDocument(IBucket bucket, Document<Person> document)
        {
            var person = document.Content;
            person.FirstName = "Tom";
            person.LastName = "Finnigan";

            //replace the document here! 
            var replaceResult = new { Success = false };

            if (replaceResult.Success)
            {
                var result = bucket.GetDocument<Person>("P1");
                if (result.Success)
                {
                    person = result.Content;
                    Console.WriteLine("Replaced document '{0}': {1} {2}", 
                        document.Id, person.FirstName, person.LastName);
                }
            }
        }

        static void DeleteDocument(IBucket bucket, Document<Person> document)
        {
            //Remove the document here! 
            var result = new { Success = false };
            if (result.Success)
            {
               Console.WriteLine("Deleted document '{0}'", document.Id);
            }
        }
    }
}
