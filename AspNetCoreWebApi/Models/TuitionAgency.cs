using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class TuitionAgency
    {
        public long TuitionAgencyId { get; set; }
        public string Name { get; set; }
        public ICollection<TuitionAgencyBranch> Branches { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
