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
        public virtual ICollection<TuitionAgencyBranch> Branches { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
