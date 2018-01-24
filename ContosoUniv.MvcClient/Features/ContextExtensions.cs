using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniv.MvcClient.Features
{
    public static class ContextExtensions
    {
		public static void AddOrUpdate(this DbContext ctx, object entity)
		{
			var entry = ctx.Entry(entity);
			switch (entry.State)
			{
				case EntityState.Detached:
					ctx.Add(entity);
					break;
				case EntityState.Modified:
					ctx.Update(entity);
					break;
				case EntityState.Added:
					ctx.Add(entity);
					break;
				case EntityState.Unchanged:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
    }
}
