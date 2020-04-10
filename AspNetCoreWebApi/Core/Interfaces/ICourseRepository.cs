using AspNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Core.Interfaces
{
    public interface ICourseRepository : IDisposable
    {
        Task<IEnumerable<Course>> List(CancellationToken token = default);
        Task<Course> Get(long id, CancellationToken token = default);
        Task<Course> Update(long id, Course update, CancellationToken token = default);
        Task<Course> Create(Course create, CancellationToken token = default);
    }
}
