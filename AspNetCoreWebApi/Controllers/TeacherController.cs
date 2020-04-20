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
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.DTOs.Mapper;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<TeacherQueryDto>> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            try
            {
                Array values = Enum.GetValues(typeof(Enums.TeacherType));
                return Ok(CreateTeachers()?.Select(teacher => teacher.ToQueryDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            IEnumerable<Teacher> CreateTeachers()
            {
                var rng = new Random();
                return Enumerable.Range(1, 3)
                    .Select(index =>
                    new Teacher
                    {
                        TeacherId = rng.Next(1, 5000),
                        PersonId = rng.Next(1, 5000),
                        TeacherType = RandomValueGenerator.RandomEnumValue<Enums.TeacherType>()
                    });
            }
        }
    }
}