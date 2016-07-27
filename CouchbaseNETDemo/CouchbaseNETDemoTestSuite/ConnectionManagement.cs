using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Configuration.Client.Providers;
using Couchbase.IO;
using Couchbase.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datageneration;
using DataObjects;

namespace CouchbaseNETDemoTestSuite
{
    [TestClass]
    public class ConnectionManagement
    {


        [TestMethod]
        public void AppConfigConnection()
        {
            ClusterHelper.Initialize("couchbaseClients/couchbase");

            var bucket = ClusterHelper.GetBucket("default");
            try
            {
                Assert.IsNotNull(bucket);
            }
            finally
            {
                ClusterHelper.Close();
            }
        }

        [TestMethod]
        public void ProgrammaticConnection()
        {
            var config = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri("http://localhost:8091")
                },
                UseSsl = false
            };

            ClusterHelper.Initialize(config);

            var bucket = ClusterHelper.GetBucket("default");
            try
            {
                Assert.IsNotNull(bucket);
            }
            finally
            {
                ClusterHelper.Close();
            }
        }
    }

    [TestClass]
    public class AtomicOperations
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ClusterHelper.Initialize("couchbaseClients/couchbase");
            TestHelpers.ClearBucket("default");
            var bucket = ClusterHelper.GetBucket("default");

            //insert some data for the update. 
            bucket.Remove("updateTest");
            bucket.Insert("updateTest", new Customer
            {
                CustomerId = "updateTest",
                FirstName = "First",
                LastName = "Last"
            });
            //insert some data for the delete
            bucket.Insert("removeTest", new Customer() {CustomerId = "DELETEME"});
        }

        [TestMethod]
        public void Insert()
        {

            var item = BulkGenerator.GetCustomers(1).First();

            var bucket = ClusterHelper.GetBucket("default");
            bucket.Insert("insertTest", item);

            var result = bucket.Get<Customer>("insertTest");
            Assert.IsNotNull(result);

            Assert.AreEqual(item, result.Value);
        }

        [TestMethod]
        public void Update()
        {
            //ensure that the required document is in the bucket
            var bucket = ClusterHelper.GetBucket("default");
            var update = bucket.Get<Customer>("updateTest");

            Assert.IsNotNull(update);
            Assert.IsTrue(string.IsNullOrEmpty(update.Value.EmailAddress));

            update.Value.EmailAddress = "test@test.com";

            bucket.Upsert("updateTest", update.Value);

            //check to see if the value was updated. 

            var result = bucket.Get<Customer>("updateTest");

            Assert.AreEqual("test@test.com", result.Value.EmailAddress);
        }

        [TestMethod]
        public void Remove()
        {
            //ensure that the required document is in the bucket
            var bucket = ClusterHelper.GetBucket("default");

            var result = bucket.Get<Customer>("removeTest");

            Assert.IsNotNull(result.Value);

            bucket.Remove("removeTest");

            var newResult = bucket.Get<Customer>("removeTest");

            Assert.AreEqual(ResponseStatus.KeyNotFound, newResult.Status);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            ClusterHelper.Close();
        }

    }

    [TestClass]
    public class BulkOperations
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ClusterHelper.Initialize("couchbaseClients/couchbase");
            TestHelpers.ClearBucket("default");
        }

        //use Datageneration project here? Just use Faker and NBuilder? 

        [TestMethod]
        public void Upsert()
        {
            var bucket = ClusterHelper.GetBucket("default");
            var customers = BulkGenerator.GetCustomers(1000).ToDictionary(c => $"customer-{c.CustomerId}");

            var upsertResult = bucket.Upsert(customers);

            foreach (var customer in upsertResult)
            {
                Assert.IsTrue(customer.Value.Success);
            }
        }

        [TestMethod]
        public void Get()
        {
            //insert a bunch of documents into the database 
            var bucket = ClusterHelper.GetBucket("default");
            var customers = BulkGenerator.GetCustomers(1000).ToDictionary(c => $"customer-{c.CustomerId}");

            var upsertResult = bucket.Upsert(customers);

            foreach (var customer in upsertResult)
            {
                Assert.IsTrue(customer.Value.Success);
            }

            var getResult = bucket.Get<Customer>(customers.Keys.ToList());
            foreach (var customer in getResult)
            {
                Assert.IsTrue(customer.Value.Success);
            }

        }

        [TestCleanup]
        public void TestCleanup()
        {
            ClusterHelper.Close();
        }

    }

    [TestClass]
    public class Queries
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ClusterHelper.Initialize("couchbaseClients/couchbase");
        }

        [TestMethod]
        public void QueryView()
        {
            var bucket = ClusterHelper.GetBucket("beer-sample");

            var query = bucket.CreateQuery("beer", "brewery_beers", false);

            var result = bucket.Query<dynamic>(query);
            Assert.IsTrue(result.TotalRows > 0);
            foreach (var row in result.Rows)
            {
                Assert.IsNotNull(row);
            }
        }

        [TestMethod]
        public void QueryN1QL()
        {
            Assert.Inconclusive();
        }

    }

    [TestClass]
    public class TimeoutManagement
    {

    }

    [TestClass]
    public class Locking
    {

    }

    [TestClass]
    public class Durability
    {
        //fault tolerance, replication, etc. Can't really demonstrate with a single server, but can at least show code. 
        //Can this be config-based, too? 
        //this will be important for Investigo! 
    }

    [TestClass]
    public class Logging
    {

    }

    public static class TestHelpers
    {
        public static void ClearBucket(string bucketName)
        {
            using (var client = new WebClient())
            {
                client.UploadData(
                    $"http://localhost:8091/pools/default/buckets/{bucketName}/controller/doFlush",
                    "POST", new byte[0]);
            }
        }
    }
}
