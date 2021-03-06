﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Queryable.Web.Models
{
    public class PersonSearchModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
     
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}