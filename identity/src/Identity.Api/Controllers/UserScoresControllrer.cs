using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserScoresControllrer:ControllerBase
{

}
