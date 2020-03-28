using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Attributes;
using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DTOs.Mapper;
using AspNetCoreWebApi.DTOs.Query;
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
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository, ILogger<CourseController> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseQueryDto>>> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            try
            {
                var courses = await _courseRepository.List().ConfigureAwait(false);
                return Ok(courses.Select(course => course.ToQueryDto()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            
            IEnumerable<Course> CreateCourses()
            {
                var rng = new Random();
                Array values = Enum.GetValues(typeof(Enums.TeacherType));
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
        public async Task<ActionResult<CourseQueryDto>> Get(long id)
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            
            try
            {
                var course = await _courseRepository.Get(id).ConfigureAwait(false);
                return Ok(course.ToQueryDto());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            Course CreateCourse()
            {
                var rng = new Random();
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