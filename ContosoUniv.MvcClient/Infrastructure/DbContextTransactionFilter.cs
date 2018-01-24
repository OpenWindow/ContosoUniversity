using ContosoUniv.MvcClient.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniv.MvcClient.Infrastructure
{
  public class DbContextTransactionFilter : IAsyncActionFilter
  {
    private readonly SchoolContext _dbContext;

    public DbContextTransactionFilter(SchoolContext context)
    {
      _dbContext = context;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      try
      {

        await _dbContext.BeginTransactionAsync();

        var actionExecuted = await next();

        if(actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
        {
          _dbContext.RollbackTransaction();
        }
        else
        {
          await _dbContext.CommitTransactionAsync();
        }

      }
      catch (Exception)
      {
        _dbContext.RollbackTransaction();
        throw;
      }
    }
  }
}
