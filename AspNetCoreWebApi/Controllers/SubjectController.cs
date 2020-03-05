using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ILogger<SubjectController> _logger;
        public SubjectController(ILogger<SubjectController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IEnumerable<Subject> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            return CreateSubjects();

            IEnumerable<Subject> CreateSubjects()
            {
                return Enumerable.Range(1, 3)
                        .Select(_ => new Subject
                        {
                            SubjectId = rng.Next(1, 5000),
                            CourseId = rng.Next(1, 5000),
                            CreditHours = rng.Next(1, 5),
                            Name = Guid.NewGuid().ToString(),
                        });
            }
        }

        [HttpGet("{id}")]
        public Subject Get(long id)
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            return CreateSubject();

            Subject CreateSubject()
            {
                return new Subject
                {
                    SubjectId = id,
                    CourseId = rng.Next(1, 5000),
                    CreditHours = rng.Next(1, 5),
                    Name = Guid.NewGuid().ToString(),
                };
            }
        }
    }
}