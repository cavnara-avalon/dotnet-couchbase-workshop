using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerId { get; set; }
        public string EmailAddress { get; set; }

        public override bool Equals(object obj)
        {
            var compObj = obj as Customer;

            if (compObj == null)
                return false;

            return CustomerId == compObj.CustomerId
                   && FirstName == compObj.FirstName
                   && LastName == compObj.LastName
                   && EmailAddress == compObj.EmailAddress;
        }
    }
}

