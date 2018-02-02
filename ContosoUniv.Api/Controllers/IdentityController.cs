using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniv.Api.Controllers
{
  [Produces("application/json")]
  [Route("api/Identity")]
  [Authorize]
  public class IdentityController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get() =>
      new JsonResult(from c in User.Claims select new { c.Type, c.Value });
  }
}