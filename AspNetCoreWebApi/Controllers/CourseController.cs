using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DTOs.Mapper;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.DTOs.Upsert;
using AspNetCoreWebApi.Models;
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
        private readonly IRepository<Course> _courseRepository;
        public CourseController(IRepository<Course> courseRepository, ILogger<CourseController> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<CourseQueryDto>>> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            try
            {
                CancellationToken cts = new CancellationToken();
                var courses = await _courseRepository.List(cts).ConfigureAwait(false);
                if (courses is null)
                {
                    return NoContent();
                }
                return Ok(courses.Select(course => course.ToQueryDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CourseQueryDto>> GetById(long id)
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
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(CourseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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
                    return NotFound(update);
                }

                return AcceptedAtAction(
                    nameof(GetById), 
                    new { Id = update.CourseId},
                    course.ToQueryDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CourseQueryDto>> Create([FromBody] CourseDto create)
        {
            _logger.LogInformation($"{this.GetType().Name}: Create");
            try
            {
                if (create is null)
                {
                    throw new ArgumentNullException(nameof(create));
                }
                CancellationToken cts = new CancellationToken();
                var course = await _courseRepository.Create(create.FromDto(), cts).ConfigureAwait(false);
                if (course is null)
                {
                    return Problem("Not Created");
                }

                return CreatedAtAction(
                    nameof(GetById),
                    new { Id = create.CourseId },
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