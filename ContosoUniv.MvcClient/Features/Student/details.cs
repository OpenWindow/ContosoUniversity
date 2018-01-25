using AutoMapper.QueryableExtensions;
using ContosoUniv.MvcClient.Data;
using ContosoUniv.MvcClient.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniv.MvcClient.Features.Student
{
  public class Details
  {
    public class Query : IRequest<Model>
    {
      public int Id { get; set; }
    }

    public class Model
    {
      public int ID { get; set; }
      public string FirstMidName { get; set; }
      public string LastName { get; set; }
      public DateTime EnrollmentDate { get; set; }
      public List<Enrollment> Enrollments { get; set; }

      public class Enrollment
      {
        public string CourseTitle { get; set; }
        public Grade? Grade { get; set; }
      }
    }

    public class Handler : AsyncRequestHandler<Query, Model>
    {
      private readonly SchoolContext _db;

      public Handler(SchoolContext db) => _db = db;
      
      protected override async Task<Model> HandleCore(Query request) => await _db.Students
          .Include(m => m.Enrollments)
          .ThenInclude(e => e.Course)
          .Where(s => s.ID == request.Id)
          .ProjectTo<Model>()
          .SingleOrDefaultAsync();

    }
  }
}
