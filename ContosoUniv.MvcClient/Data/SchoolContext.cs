using ContosoUniv.MvcClient.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniv.MvcClient.Data
{
  public class SchoolContext : DbContext
  {
    private IDbContextTransaction _currentTransaction;

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Course>().ToTable("Course");
      modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
      modelBuilder.Entity<Student>().ToTable("Student");
    }

    public async Task BeginTransactionAsync()
    {
      if(_currentTransaction !=null)
      {
        return;
      }

      _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
      try
      {
        await SaveChangesAsync().ConfigureAwait(false);
        _currentTransaction?.Commit();
      }
      catch (Exception)
      {
        RollbackTransaction();
        throw;
      }
      finally
      {
        if(_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }

    public void RollbackTransaction()
    {
      try
      {
        _currentTransaction?.Rollback();
      }
      finally
      {
        if(_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }


  }
}

