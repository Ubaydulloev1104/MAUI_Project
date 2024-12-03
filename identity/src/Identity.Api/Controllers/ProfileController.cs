using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Application.Contracts.Profile.Queries;
using Identity_Application.Contracts.UserScores.Commands.Create;
using Identity_Application.Contracts.UserScores.Commands.Delete;
using Identity_Application.Contracts.UserScores.Commands.Update;
using Identity_Application.Contracts.UserScores.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetProfileByUserName([FromQuery] string userName = null)
    {
        var result = await _mediator.Send(new GetPofileQuery { UserName = userName });
        return Ok(result);
    }
    [HttpPost("CreateUserScoreDetail")]
    public async Task<IActionResult> CreateUserScoreDetail([FromBody] CreateUserScoreDetailCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("DeleteUserScoreDetail/{id}")]
    public async Task<IActionResult> DeleteUserScoreDetail([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteUserScoreCommand { Id = id });
        return Ok(result);
    }

    [HttpPut("UpdateUserScoreDetail")]
    public async Task<IActionResult> UpdateUserScoreDetail([FromBody] UpdateUserScoreDetailCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetUserScoreByUser")]
    public async Task<IActionResult> GetUserScoresByUser([FromQuery] GetUserScoresByUsernameQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("GetAllUserScore")]
    public async Task<IActionResult> GetAllUserScore()
    {
        var result = await _mediator.Send(new GetAllUsersScoresQuery());
        return Ok(result);
    }

}
