using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.repository.Contracts;

public interface IRepositoryBase<T> where T : class
{
    public void Add<T>(T entity) where T : class;
    public void AddRange<T>(IEnumerable<T> entities) where T : class;
    public void Update<T>(T entity) where T : class;
    public void UpdateRange<T>(IEnumerable<T> entities) where T : class;
    public void Delete<T>(T entity) where T : class;
    public void DeleteRange<T>(IEnumerable<T> entities) where T : class;
    public Task<bool> SaveChangesAsync();
}
