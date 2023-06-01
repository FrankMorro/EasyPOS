using Application.Customers.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ApiController
{
    private readonly ISender _mediator;

    public CustomerController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createCustomerResul = await _mediator.Send(command);

        return createCustomerResul.Match(
            customer => Ok(),
            errors => Problem(errors)
            );
    }
}
