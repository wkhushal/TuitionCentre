using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public static class Enums
    {
        public enum TeacherType
        {
            Permanent=0,
            Visiting=1,
            Trainee=2,
            Assistant=3
        }
    }
}
