using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Helper;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            return CreateStudents();

            IEnumerable<Student> CreateStudents()
            {
                return Enumerable.Range(1, 3)
                        .Select(_ =>
                        {
                            long tuitionAgencyId = rng.Next(1, 5000);
                            return new Student
                            {
                                StudentId = rng.Next(1, 5000),
                                Person = CreatePerson(),
                                TuitionAgency = CreateAgency(tuitionAgencyId),
                                CoursesRegistered = CreateCourses(tuitionAgencyId)
                            };
                        });
            }

            Person CreatePerson()
            {
                return new Person
                {
                    PersonId = rng.Next(1, 5000),
                    BirthDate = RandomValueGenerator.RandomDay(),
                    Name = Guid.NewGuid().ToString()
                };
            }

            TuitionAgency CreateAgency(long agencyId)
            {
                return new TuitionAgency
                {
                    TuitionAgencyId = agencyId,
                    Name = Guid.NewGuid().ToString(),
                    Branches = CreateBranches(agencyId),
                    Courses = CreateCourses(agencyId)
                };
            }

            ICollection<TuitionAgencyBranch> CreateBranches(long agencyId)
            {
                return Enumerable.Range(1, 3).Select(_ =>
                {
                    return new TuitionAgencyBranch
                    {
                        TuitionAgencyBranchId = rng.Next(1, 5000),
                        BranchAddress = Guid.NewGuid().ToString(),
                        BranchName = Guid.NewGuid().ToString(),
                        TuitionAgencyId = agencyId
                    };
                }).ToArray();
            }

            ICollection<Course> CreateCourses(long agencyId)
            {
                return Enumerable.Range(1, 3).Select(_ =>
                {
                    long courseId = rng.Next(1, 5000);
                    return new Course
                    {
                        CourseId = courseId,
                        Name = Guid.NewGuid().ToString(),
                        TuitionAgencyId = agencyId,
                        Subjects = CreateSubjects(courseId)
                    };
                }).ToArray();
            }

            ICollection<Subject> CreateSubjects(long courseId)
            {
                return Enumerable.Range(1, 3).Select(_ =>
                {
                    long courseId = rng.Next(1, 5000);
                    return new Subject
                    {
                        SubjectId = courseId,
                        Name = Guid.NewGuid().ToString(),
                        CreditHours = rng.Next(1, 5),
                        CourseId = courseId,
                    };
                }).ToArray();
            }
        }

        [HttpGet("{id}")]
        public Student Get(long studentId)
        {
            _logger.LogInformation($"{this.GetType().Name}: Get {studentId}");
            var rng = new Random();
            var tuitionCentreId = rng.Next(1, 5000);
            return new Student 
            { 
                StudentId = studentId,
                Person = CreatePerson(),
                TuitionAgency = new TuitionAgency { },
                CoursesRegistered = CreateCourses(tuitionCentreId)
            };

            Person CreatePerson()
            {
                return new Person
                {
                    PersonId = rng.Next(1, 5000),
                    BirthDate = RandomValueGenerator.RandomDay(),
                    Name = Guid.NewGuid().ToString()
                };
            }

            ICollection<Course> CreateCourses(long agencyId)
            {
                return Enumerable.Range(1, 3).Select(_ =>
                {
                    long courseId = rng.Next(1, 5000);
                    return new Course
                    {
                        CourseId = courseId,
                        Name = Guid.NewGuid().ToString(),
                        TuitionAgencyId = agencyId,
                        Subjects = CreateSubjects(courseId)
                    };
                }).ToArray();
            }

            ICollection<Subject> CreateSubjects(long courseId)
            {
                return Enumerable.Range(1, 3).Select(_ =>
                {
                    long courseId = rng.Next(1, 5000);
                    return new Subject
                    {
                        SubjectId = courseId,
                        Name = Guid.NewGuid().ToString(),
                        CreditHours = rng.Next(1, 5),
                        CourseId = courseId,
                    };
                }).ToArray();
            }
        }
    }
}