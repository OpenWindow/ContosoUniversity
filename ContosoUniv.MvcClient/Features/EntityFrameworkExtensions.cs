using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniv.MvcClient.Features
{
    public static class EntityFrameworkExtensions
    {
		public static async Task<TDestination> ProjectToSingleOrDefault<TDestination>(this IQueryable queryable)
		{
			return await queryable.ProjectTo<TDestination>().SingleOrDefaultAsync();
		}
    }
}
