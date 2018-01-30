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
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
  }
}
