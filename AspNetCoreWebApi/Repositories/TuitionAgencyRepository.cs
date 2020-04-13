using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DBContexts;
using AspNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Repositories
{
    public class TuitionAgencyRepository : IRepository<TuitionAgency>
    {
        private readonly TuitionAgencyContext _context;
        private readonly ILogger<TuitionAgencyRepository> _logger;
        public TuitionAgencyRepository(TuitionAgencyContext context, ILogger<TuitionAgencyRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<TuitionAgency> Create(TuitionAgency create, CancellationToken token = default)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            // if it already exists?
            var existingCourse = await _context.TuitionAgencies.FindAsync(new object[] { create.TuitionAgencyId }, token).ConfigureAwait(false);

            if (!(existingCourse is null))
            {
                _logger.LogInformation("tuition agency already exists {@Agency}", create);
                return default;
            }

            try
            {
                var created = await _context.TuitionAgencies.AddAsync(create, token).ConfigureAwait(false);
                var count = await _context.SaveChangesAsync(token).ConfigureAwait(false);
                if (count <= 0)
                {
                    return default;
                }
                return created.Entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Course Update {@Exception}", ex);
                return default;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<TuitionAgency> Get(long id, CancellationToken token = default)
        {
            _logger.LogInformation("Get {Id}", id);
            return await _context.TuitionAgencies.FindAsync(new object[] { id }, token).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TuitionAgency>> List(CancellationToken token = default)
        {
            _logger.LogInformation("List");
            try
            {
                return await _context
                                .TuitionAgencies
                                .Include(agency => agency.Branches)
                                .Include(agency => agency.Courses)
                                .ToListAsync(token)
                                .ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("TuitionAgency List {@Exception}", ex);
                return default;
            }
        }

        public Task<TuitionAgency> Update(long id, TuitionAgency update, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
