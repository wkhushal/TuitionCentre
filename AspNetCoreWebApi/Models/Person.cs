﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class Person
    {
        public long PersonId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
