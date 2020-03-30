using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Upsert
{
    public class CourseDto
    {
        public long CourseId { get; set; }
        public string Name { get; set; }
        public long TuitionAgencyId { get; set; }
        public ICollection<long> Subjects { get; set; }
    }
}
