using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TuitionAgencyController : ControllerBase
    {
        private readonly ILogger<TuitionAgencyController> _logger;

        public TuitionAgencyController(ILogger<TuitionAgencyController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TuitionAgency>> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            return Ok(CreateTuitionAgencies());

            IEnumerable<TuitionAgency> CreateTuitionAgencies()
            {
                return Enumerable.Range(1, 3).Select(index => CreateAgency());
            }

            TuitionAgency CreateAgency()
            {
                long agencyId = rng.Next(1, 5000);
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
    }
}