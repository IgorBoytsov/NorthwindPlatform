using MediatR;
using Microsoft.AspNetCore.Mvc;
using NorthwindPlatform.Staff.Service.Api.Models;
using NorthwindPlatform.Staff.Service.Application.Features.Employees.Commands.Create;

namespace NorthwindPlatform.Staff.Service.Api.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
        {
            var command = new CreateEmployeeCommand(
                Guid.Parse(request.UserId),
                 request.Name, 
                 request.Surname, 
                 request.Patronymic, 
                 DateOnly.Parse(request.DateOfBirth), 
                 request.Gender, 
                 request.Citizenship);
            
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}