using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ILogger<CourseRepository> _logger;
        public CourseRepository(ILogger<CourseRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Dispose()
        {
        }

        public Task<Course> Get(long id)
        {
            _logger.LogInformation("Get {Id}", id);
            return Task.FromResult(CreateCourse());

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

        public Task<IEnumerable<Course>> List()
        {
            _logger.LogInformation("List");
            return Task.FromResult(CreateCourses());

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

        public Task<Course> Update(long id, Course update)
        {
            if (update is null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            return Task.FromResult(update);
        }
    }
}
