namespace ContosoUniv.MvcClient.Features.Student
{
	using ContosoUniv.MvcClient.Data;
	using MediatR;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentValidation;
	using AutoMapper;
	using ContosoUniv.MvcClient.Features;

	public class Edit
	{
		public class Query : IRequest<Command>
		{
			public int? Id { get; set; }
		}

		public class QueryValidator : AbstractValidator<Query>
		{
			public QueryValidator()
			{
				RuleFor(m => m.Id).NotNull();
			}
		}

		public class QueryHandler: AsyncRequestHandler<Query, Command>
		{
			private readonly SchoolContext _db;
			public QueryHandler(SchoolContext db)
			{
				_db = db;
			}

			protected override async Task<Command> HandleCore(Query request) =>
				await _db.Students
					.Where(s => s.ID == request.Id)
					.ProjectToSingleOrDefault<Command>();
		}

		public class Command: IRequest
		{
			public int ID { get; set; }
			public string LastName { get; set; }

			[Display(Name ="First Name")]
			public string FirstMidName { get; set; }
			public DateTime? EnrollmentDate { get; set; }
		}

		public class Validator : AbstractValidator<Command>
		{
			public Validator()
			{
				RuleFor(m => m.LastName).NotNull().Length(1, 50);
				RuleFor(m => m.FirstMidName).NotNull().Length(1, 50);
				RuleFor(m => m.EnrollmentDate).NotNull();
			}
		}

		public class CommandHandler : AsyncRequestHandler<Command>
		{
			private readonly SchoolContext _db;

			public CommandHandler(SchoolContext db)
			{
				_db = db;
			}

			protected override async Task HandleCore(Command request)
			{
				var student = await _db.Students.FindAsync(request.ID);
				Mapper.Map(request, student);
			}
		}
	}
}
