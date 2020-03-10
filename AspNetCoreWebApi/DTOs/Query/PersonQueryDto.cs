using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class PersonQueryDto
    {
        public long PersonId { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
