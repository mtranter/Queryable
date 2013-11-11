using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queryable.Models
{
    public class PersonSearchModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int MaxResults { get; set; }
    }
}