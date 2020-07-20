using Application.Dtos.Common;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly FilmunityDataContext _context;

        public Repository(FilmunityDataContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public async Task<bool> Contains(ISpecification<TEntity> specification = null)
        {
            return await Count(specification) > 0 ? true : false;
        }

        public async Task<bool> Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return await Count(predicate) > 0 ? true : false;
        }

        public async Task<int> Count(ISpecification<TEntity> specification = null)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).CountAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification = null)
        {
            if (specification == null)
                return await _context.Set<TEntity>().ToListAsync();

            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<PagedListDto<TEntity>> FindAsyncWithPagination(ISpecification<TEntity> specification)
        {
            var queryable = ApplySpecification(specification);

            return await CreatePagedResult(queryable, specification.PageNumber, pageSize: specification.Take, specification.Count);
        }

        public async Task<TEntity> FindOneAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAllByIdAsync(List<int> ids)
        {
            return await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }

        private static async Task<PagedListDto<TEntity>> CreatePagedResult(IQueryable<TEntity> source, int pageNumber, int pageSize, int count)
        {
            var items = await source.ToListAsync();
            return new PagedListDto<TEntity>(items, count, pageNumber, pageSize);
        }

    }
}
