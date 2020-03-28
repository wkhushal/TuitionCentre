using AspNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class CourseQueryDto
    {
        public CourseQueryDto(long courseId, string name, long tuitionAgencyId,
            ICollection<Subject> subjects)
        {
            if (subjects is null)
            {
                throw new ArgumentNullException(nameof(subjects));
            }

            CourseId = courseId;
            Name = name;
            TuitionAgencyId = tuitionAgencyId;
            Subjects = subjects
                        .Select(item => 
                            new SubjectQueryDto(item.SubjectId, item.Name, item.CreditHours, item.CourseId))
                        .ToList();
        }

        public long CourseId { get; set; }
        public string Name { get; set; }
        public long TuitionAgencyId { get; set; }
        public ICollection<SubjectQueryDto> Subjects { get; set; }
    }
}
