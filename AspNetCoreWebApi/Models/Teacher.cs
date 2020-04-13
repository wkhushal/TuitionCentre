using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AspNetCoreWebApi.Models.Enums;

namespace AspNetCoreWebApi.Models
{
    public class Teacher
    {
        public long TeacherId { get; set; }
        public TeacherType TeacherType { get; set; } = TeacherType.Permanent;
        public long PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
