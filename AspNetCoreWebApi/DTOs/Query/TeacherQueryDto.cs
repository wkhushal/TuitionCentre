using static AspNetCoreWebApi.Models.Enums;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class TeacherQueryDto
    {
        public TeacherQueryDto(long teacherId, TeacherType teacherType, long personId)
        {
            TeacherId = teacherId;
            TeacherType = teacherType;
            Person = new PersonQueryDto { PersonId = personId };
        }

        public long TeacherId { get; private set; }
        public TeacherType TeacherType { get; private set; }
        public PersonQueryDto Person { get; private set; }
    }
}
