using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public static class Enums
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TeacherType
        {
            Permanent=0,
            Visiting=1,
            Trainee=2,
            Assistant=3
        }
    }
}
