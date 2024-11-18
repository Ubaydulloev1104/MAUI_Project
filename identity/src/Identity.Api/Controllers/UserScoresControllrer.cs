using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Application.Contracts.Profile.Queries;
using Identity_Application.Contracts.User.Queries;
using Identity_Application.Contracts.UserScores.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserScoresControllrer:ControllerBase
{
    private readonly IMediator _mediator;

    public UserScoresControllrer(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUserScores()
    {
        var users = await _mediator.Send(new GetAllUsersScoresQuery());
        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserScores([FromQuery] string userName = null)
    {
        var result = await _mediator.Send(new GetPofileQuery { UserName = userName });
        return Ok(result);
    }
}
