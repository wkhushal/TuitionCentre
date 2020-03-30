using AspNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Core.Interfaces
{
    public interface ICourseRepository : IDisposable
    {
        Task<IEnumerable<Course>> List();
        Task<Course> Get(long id);
        Task<Course> Update(long id, Course update);
    }
}
