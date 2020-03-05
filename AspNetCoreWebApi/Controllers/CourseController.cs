using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Attributes;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            Array values = Enum.GetValues(typeof(Enums.TeacherType));
            return CreateCourses();

            IEnumerable<Course> CreateCourses()
            {
                return Enumerable.Range(1, 3)
                    .Select(index =>
                    {
                        var courseId = rng.Next(1, 5000);
                        return new Course
                        {
                            CourseId = courseId,
                            Name = Guid.NewGuid().ToString(),
                            TuitionAgencyId = rng.Next(1, 5000),
                            Subjects = Enumerable.Range(1, 3)
                                             .Select(_ => new Subject
                                             {
                                                 SubjectId = rng.Next(1, 5000),
                                                 CourseId = courseId,
                                                 CreditHours = rng.Next(1, 5),
                                                 Name = Guid.NewGuid().ToString(),
                                             }).ToArray()
                        };
                    });
            }
        }

        [HttpGet("{id}")]
        public Course Get(long id)
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            Array values = Enum.GetValues(typeof(Enums.TeacherType));
            return CreateCourse();

            Course CreateCourse()
            {
                var courseId = id;
                return new Course
                {
                    CourseId = courseId,
                    Name = Guid.NewGuid().ToString(),
                    TuitionAgencyId = rng.Next(1, 5000),
                    Subjects = Enumerable.Range(1, 3)
                                     .Select(_ => new Subject
                                     {
                                         SubjectId = rng.Next(1, 5000),
                                         CourseId = courseId,
                                         CreditHours = rng.Next(1, 5),
                                         Name = Guid.NewGuid().ToString(),
                                     }).ToArray()
                };
            }
        }
    }
}