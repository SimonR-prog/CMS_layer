﻿using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        try
        {
            //If entity is null throws exception.
            if (entity == null)
            {
                throw new Exception();
            }
            //Add entity to the database and then save the changes.
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
            {
                throw new Exception();
            }
            //Updates the existing entity in the database and then save the changes.
            _db.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return entity;
        }
    }
    public virtual async Task<bool> RemoveAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
            {
                throw new Exception();
            }
            //Removes the entity from the database and saves the changes.
            _db.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            //Gets all the entities from the database and returns them.
            var entities = await _db.ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            //Returns empty list incase of exception.
            Debug.WriteLine(ex);
            return new List<TEntity>();
        }
    }
    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            //Finds the first entity and returns it.
            var entity = await _db.FirstOrDefaultAsync(expression);
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {   
            //Checks if there is any kind of entity, returns either true or false.
            var result = await _db.AnyAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
