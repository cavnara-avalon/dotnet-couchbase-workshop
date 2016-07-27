using System.Collections.Generic;
using DataObjects;
using System.Runtime.Remoting;
using FizzWare.NBuilder;

namespace Datageneration
{
    public class BulkGenerator
    {
        public static IList<Customer> GetCustomers(int count)
        {
            return Builder<Customer>.CreateListOfSize(count).All()
                .With(c => c.CustomerId = Faker.NumberFaker.Number().ToString())
                .With(c => c.EmailAddress = Faker.InternetFaker.Email())
                .With(c => c.FirstName = Faker.NameFaker.FirstName())
                .With(c => c.LastName = Faker.NameFaker.LastName()).Build();
        }
    }
}
