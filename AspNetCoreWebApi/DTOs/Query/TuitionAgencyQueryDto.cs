using AspNetCoreWebApi.DTOs.Mapper;
using AspNetCoreWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class TuitionAgencyQueryDto
    {
        public TuitionAgencyQueryDto(long tuitionAgencyId, string name, ICollection<TuitionAgencyBranch> branches, ICollection<Course> courses)
        {
            TuitionAgencyId = tuitionAgencyId;
            Name = name;
            Branches = branches?.Select(branch => branch.ToQueryDto()).ToList();
            Courses = courses?.Select(course => course.ToQueryDto()).ToList();
        }

        public long TuitionAgencyId { get; private set; }
        public string Name { get; private set; }
        public ICollection<TuitionAgencyBranchQueryDto> Branches { get; private set; }
        public ICollection<CourseQueryDto> Courses { get; private set; }
    }
}
