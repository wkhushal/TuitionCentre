using AspNetCoreWebApi.DTOs.Mapper;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AspNetCoreWebApiTests.DTOs.Mapper
{
    public class MapToQueryDtoTests
    {
        [Theory, AutoData]
        public void SubjectQueryDto(Subject subject)
        {
            Action();
            Asserts();

            SubjectQueryDto result;
            void Action()
            {
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

        [Theory, AutoData]
        public void TeacherQueryDto(Teacher teacher)
        {
            Action();
            Asserts();
            
            TeacherQueryDto result;
            void Action()
            {
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

        [Theory, AutoData]
        public void StudentQueryDto(Student student)
        {
            Action();
            Asserts();
            
            StudentQueryDto result;
            void Action()
            {
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

        [Theory, AutoData]
        public void StudentQueryDtoSparseMembers(IFixture fixture)
        {
            Arrange();
            Action();
            Asserts();

            Student student;
            void Arrange()
            {
                student = new Student
                {
                    StudentId = fixture.Create<long>()
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
    }
}
