using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class TuitionAgencyController : ControllerBase
    {
        private readonly IRepository<TuitionAgency> _tuitionAgencyRepository;
        private readonly ILogger<TuitionAgencyController> _logger;

        public TuitionAgencyController(IRepository<TuitionAgency> tuitionAgencyRepository, ILogger<TuitionAgencyController> logger)
        {
            _tuitionAgencyRepository = tuitionAgencyRepository ?? throw new ArgumentNullException(nameof(tuitionAgencyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TuitionAgencyQueryDto>>> Get()
        {
            _logger.LogInformation($"{this.GetType().Name}: Get");
            try
            {
                CancellationToken token = new CancellationToken();
                var result = await _tuitionAgencyRepository.List(token).ConfigureAwait(false);
                if (result is null || !result.Any())
                {
                    return NoContent();
                }
                return Ok(result.Select(agency => agency.ToQueryDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}