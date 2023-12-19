using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;

namespace cliqx.gds.repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly DefaultContext _context;

    public RepositoryBase(DefaultContext Context)
    {
        _context = Context;
    }


    public void Add<T>(T entity) where T : class
    {
        _context.AddAsync(entity);
    }

    public void AddRange<T>(IEnumerable<T> entities) where T : class
    {
        _context.AddRangeAsync(entities);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public void DeleteRange<T>(IEnumerable<T> entities) where T : class
    {
        _context.RemoveRange(entities);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void UpdateRange<T>(IEnumerable<T> entities) where T : class
    {
        _context.UpdateRange(entities);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
