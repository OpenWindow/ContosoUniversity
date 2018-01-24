using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ContosoUniv.MvcClient.Features
{
    public static class MapperExtensions
    {
        public static Task<IPagedList<TDestination>> ProjectToPagedListAsync<TDestination>(this IQueryable queryable, int pageNumber, int pageSize)
        {
            return queryable.ProjectTo<TDestination>()
                   .ToPagedListAsync(pageNumber, pageSize);
                
        }
    }
}
