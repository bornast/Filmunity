﻿using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> FindAllByIdAsync(List<int> ids);
        Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification = null);
        Task<PagedListDto<TEntity>> FindAsyncWithPagination(ISpecification<TEntity> specification);
        Task<TEntity> FindOneAsync(ISpecification<TEntity> specification = null);       
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<bool> Contains(ISpecification<TEntity> specification = null);
        Task<bool> Contains(Expression<Func<TEntity, bool>> predicate);
        Task<int> Count(ISpecification<TEntity> specification = null);
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
    }
}