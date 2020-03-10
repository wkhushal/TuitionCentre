using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class SubjectQueryDto
    {
        public SubjectQueryDto(long subjectId, string name, int creditHours, long courseId)
        {
            if(subjectId < 0)
            {
                throw new ArgumentException("argument cannot be less than zero", nameof(subjectId));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("value cannot be null or Empty", nameof(name));
            }

            if (creditHours < 0)
            {
                throw new ArgumentException("argument cannot be less than zero", nameof(creditHours));
            }

            if (courseId < 0)
            {
                throw new ArgumentException("argument cannot be less than zero", nameof(courseId));
            }

            SubjectId = subjectId;
            Name = name;
            CreditHours = creditHours;
            CourseId = courseId;
        }

        public long SubjectId { get; private set; }
        public string Name { get; private set; }
        public int CreditHours { get; private set; }
        public long CourseId { get; private set; }
    }
}
