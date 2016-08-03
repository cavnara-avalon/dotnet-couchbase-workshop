using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using Couchbase.Core;

namespace Couchbase.Examples.BulkOperations
{
    internal class Program
    {
        private static IBucket _bucket;

        private static void Main(string[] args)
        {
            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    /* place your server addresses here */
                }
            });
            _bucket = ClusterHelper.GetBucket("default");

            var tasks = GetTasks(1000);
            var results = BulkUpsertAsync(tasks);
            WriteResults(results);
            Console.WriteLine();

            Console.Read();
            ClusterHelper.Close();
        }

        static Dictionary<string, Post> GetTasks(int count)
        {
            var posts = new Dictionary<string, Post>();
            for (int i = 0; i < count; i++)
            {
                var key = "post" + i;
                posts.Add(key, new Post
                {
                    Author = "Author" + i,
                    PostId = key,
                    Content = "[Add content here]"
                });
            }

            return posts;
        }

        static IDictionary<string, IOperationResult<Post>> BulkUpsertAsync(Dictionary<string, Post> posts)
        {
            // do the bulk Upsert here! 
            return new Dictionary<string, IOperationResult<Post>>();
        }

        static void WriteResults(IDictionary<string, IOperationResult<Post>> results)
        {
            foreach (var operationResult in results.Values)
            {
                Console.WriteLine(operationResult.Success);
            }
        }
    }
}
