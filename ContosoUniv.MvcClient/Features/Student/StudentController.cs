using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniv.MvcClient.Data;
using ContosoUniv.MvcClient.Models;
using MediatR;

namespace ContosoUniv.MvcClient.Features.Student
{
    public class StudentController : Controller
    {
        private readonly IMediator  _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Students
        public async Task<IActionResult> Index(Index.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

		public async Task<IActionResult> Edit(Edit.Query query)
		{
			var model = await _mediator.Send(query);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Edit.Command command)
		{
			await _mediator.Send(command);
			return this.RedirectToAction(nameof(Index));
		}
    }
}
