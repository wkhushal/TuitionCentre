using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class Subject
    {
        public long SubjectId { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public long CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
