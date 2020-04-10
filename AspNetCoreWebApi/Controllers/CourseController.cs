using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DTOs.Mapper;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.DTOs.Upsert;
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
                CancellationToken cts = new CancellationToken();
                var courses = await _courseRepository.List(cts).ConfigureAwait(false);
                return Ok(courses.Select(course => course.ToQueryDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseQueryDto>> Get(long id)
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            try
            {
                CancellationToken cts = new CancellationToken();
                var course = await _courseRepository.Get(id, cts).ConfigureAwait(false);
                if(course is null)
                {
                    return NoContent();
                }
                return Ok(course.ToQueryDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<CourseQueryDto>> Update(long id, [FromBody] CourseDto update)
        {
            _logger.LogInformation($"{this.GetType().Name}: Update");
            try
            {
                if (update is null)
                {
                    throw new ArgumentNullException(nameof(update));
                }
                CancellationToken cts = new CancellationToken();
                var course = await _courseRepository.Update(id, update.FromDto(), cts).ConfigureAwait(false);
                if(course is null)
                {
                    return NoContent();
                }

                return new AcceptedAtActionResult(
                    "Update", 
                    nameof(CourseController),
                    new { Id = update.CourseId},
                    course.ToQueryDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}