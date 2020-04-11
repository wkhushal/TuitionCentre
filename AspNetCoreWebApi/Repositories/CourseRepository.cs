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
    public class CourseRepository : IRepository<Course>
    {
        private readonly TuitionAgencyContext _context;
        private readonly ILogger<CourseRepository> _logger;
        public CourseRepository(TuitionAgencyContext context, ILogger<CourseRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<Course> Get(long id, CancellationToken token = default)
        {
            _logger.LogInformation("Get {Id}", id);
            return await _context.Courses.FindAsync(new object[] { id }, token).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Course>> List(CancellationToken token = default)
        {
            _logger.LogInformation("List");
            try
            {
                var result = await _context
                                    .Courses
                                    .ToListAsync(token)
                                    .ConfigureAwait(false);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Course List {@Exception}", ex);
                return default;
            }
        }

        public async Task<Course> Update(long id, Course update, CancellationToken token = default)
        {
            if (update is null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var existingCourse = await _context.Courses.FindAsync(new object[] { id }, token).ConfigureAwait(false);

            if (existingCourse is null)
            {
                _logger.LogInformation("course doesnt exist {Id} update {@Course}", id, update);
                return default;
            }

            existingCourse.Name = update.Name;
            existingCourse.Subjects = update.Subjects;
            existingCourse.TuitionAgencyId = update.TuitionAgencyId;
            try
            {
                var count = await _context.SaveChangesAsync(token).ConfigureAwait(false);
                if(count <= 0)
                {
                    return default;
                }
                return existingCourse;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Course Update {@Exception}", ex);
                return default;
            }
        }

        public async Task<Course> Create(Course create, CancellationToken token = default)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            // if it already exists?
            var existingCourse = await _context.Courses.FindAsync(new object[] { create.CourseId }, token).ConfigureAwait(false);

            if (!(existingCourse is null))
            {
                _logger.LogInformation("course already exists {@Course}", create);
                return default;
            }

            try
            {
                var created = await _context.Courses.AddAsync(create, token).ConfigureAwait(false);
                var count = await _context.SaveChangesAsync(token).ConfigureAwait(false);
                if(count <= 0)
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
    }
}
