using AspNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Core.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class, new()
    {
        Task<IEnumerable<T>> List(CancellationToken token = default);
        Task<T> Get(long id, CancellationToken token = default);
        Task<T> Update(long id, T update, CancellationToken token = default);
        Task<T> Create(T create, CancellationToken token = default);
    }
}
