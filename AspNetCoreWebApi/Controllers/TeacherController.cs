using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreWebApi.Helper;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ILogger<TeacherController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            var rng = new Random();
            Array values = Enum.GetValues(typeof(Enums.TeacherType));
            return Ok(CreateTeachers());

            IEnumerable<Teacher> CreateTeachers()
            {
                return Enumerable.Range(1, 3)
                    .Select(index => 
                    new Teacher { 
                        TeacherId = rng.Next(1, 5000),
                        PersonId= rng.Next(1, 5000),
                        TeacherType = RandomValueGenerator.RandomEnumValue<Enums.TeacherType>()
                    });
            }
        }
    }
}