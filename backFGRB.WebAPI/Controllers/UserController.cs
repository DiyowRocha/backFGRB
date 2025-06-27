using backFGRB.Application.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backFGRB.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateUserDto dto)
    {
        var result = await _userService.CreateAsync(dto);

        return Created("api/User", result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _userService.GetAllUsersAsync();

        return Ok(result);
    }

    [HttpGet("{user}")]
    public async Task<ActionResult> GetByStringAsync([FromRoute] string user)
    {
        var result = await _userService.GetByStringAsync(user);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateUserDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);

        return Ok(result);
    }

    [HttpPut("Deactive/{id}")]
    public async Task<ActionResult> DeactiveAsync([FromRoute] int id)
    {
        var result = await _userService.DeactiveAsync(id);

        if (!result)
            return BadRequest();

        return Ok("User deactive.");
    }
}