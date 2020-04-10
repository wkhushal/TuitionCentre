using AspNetCoreWebApi.DTOs.Mapper;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.Linq;
using Xunit;

namespace AspNetCoreWebApiTests.DTOs.Mapper
{
    public class MapToQueryDtoTests
    {
        IFixture _fixture;

        public MapToQueryDtoTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void SubjectQueryDto()
        {
            Action();
            Asserts();

            SubjectQueryDto result;
            Subject subject;
            void Action()
            {
                subject = _fixture.Create<Subject>();
                result = MapToQueryDto.ToQueryDto(subject);
            }
            void Asserts()
            {
                Assert.NotNull(result);
                Assert.Equal(subject.SubjectId, result.SubjectId);
                Assert.Equal(subject.Name, result.Name);
                Assert.Equal(subject.CourseId, result.CourseId);
                Assert.Equal(subject.CreditHours, result.CreditHours);
            }
        }

        [Fact]
        public void SubjectQueryDtoArgumentNullGuard()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Subject).ToQueryDto());
        }

        [Fact]
        public void TeacherQueryDto()
        {
            Action();
            Asserts();

            Teacher teacher;
            TeacherQueryDto result;
            void Action()
            {
                teacher = _fixture.Create<Teacher>();
                result = MapToQueryDto.ToQueryDto(teacher);
            }
            
            void Asserts()
            {
                Assert.NotNull(result);
                Assert.Equal(teacher.TeacherId, result.TeacherId);
                Assert.Equal(teacher.TeacherType, result.TeacherType);
                Assert.Equal(teacher.PersonId, result.Person.PersonId);
            }
        }
        
        [Fact]
        public void TeacherQueryDtoArgumentNullGuard()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Teacher).ToQueryDto());
        }

        [Fact]
        public void StudentQueryDto()
        {
            Action();
            Asserts();
            
            Student student;
            StudentQueryDto result;
            void Action()
            {
                student = _fixture.Create<Student>();
                result = MapToQueryDto.ToQueryDto(student);
            }
            void Asserts()
            {
                Assert.NotNull(result);
                Assert.Equal(student.StudentId, result.StudentId);
                Assert.Equal(student.Person.PersonId, result.Person.PersonId);
                Assert.Equal(student.TuitionAgency.TuitionAgencyId, result.TuitionAgencyId);
                Assert.Equal(student.CoursesRegistered.Select(c => c.CourseId), result.CoursesRegistered);
            }
        }

        [Fact]
        public void StudentQueryDtoArgumentNullGuard()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Student).ToQueryDto());
        }

        [Fact]
        public void StudentQueryDtoSparseMembers()
        {
            Arrange();
            Action();
            Asserts();

            Student student;
            void Arrange()
            {
                student = new Student
                {
                    StudentId = _fixture.Create<long>()
                };
            }
            StudentQueryDto result;
            void Action()
            {
                result = MapToQueryDto.ToQueryDto(student);
            }
            void Asserts()
            {
                Assert.NotNull(result);
                Assert.Equal(student.StudentId, result.StudentId);
                Assert.Null(student.Person);
                Assert.Null(student.CoursesRegistered);
            }
        }

        [Fact]
        public void CourseQueryDtoArgumentNullGuard()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Course).ToQueryDto());
        }

        [Fact]
        public void CourseQueryDto()
        {
            Action();
            Asserts();

            Course course;
            CourseQueryDto result;
            void Action()
            {
                course = _fixture.Create<Course>();
                result = MapToQueryDto.ToQueryDto(course);
            }
            
            void Asserts()
            {
                Assert.NotNull(result);
                Assert.Equal(course.CourseId, result.CourseId);
                Assert.Equal(course.Name, result.Name);
                Assert.Equal(course.TuitionAgencyId, result.TuitionAgencyId);
                Assert.Equal(course.Subjects.Select(subj => subj.SubjectId), result.Subjects.Select(dto => dto.SubjectId));
                Assert.Equal(course.Subjects.Select(subj => subj.Name), result.Subjects.Select(dto => dto.Name));
                Assert.Equal(course.Subjects.Select(subj => subj.CreditHours), result.Subjects.Select(dto => dto.CreditHours));
                Assert.Equal(course.Subjects.Select(subj => subj.CourseId), result.Subjects.Select(dto => dto.CourseId));
            }
        }
    }
}
