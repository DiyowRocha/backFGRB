using backFGRB.Application.Services.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backFGRB.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _logService.GetAllAsync();

        if (!result.Any())
            return NotFound("Logs not found.");

        return Ok(result);
    }

    [HttpGet("user")]
    public async Task<ActionResult> GetByUserAsync([FromBody]GetLogByUserDto dto)
    {
        var result = await _logService.GetLogByUserAsync(dto);

        if (result == null)
            return NotFound("Logs not found.");

        return Ok(result);
    }
}