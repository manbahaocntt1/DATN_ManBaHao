using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;

[ApiController]
[Route("api/[controller]")]
public class UserBehaviorController : ControllerBase
{
    private readonly UserBehaviorService _behaviorService;
    public UserBehaviorController(UserBehaviorService behaviorService) => _behaviorService = behaviorService;

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var behaviors = await _behaviorService.GetByUserIdAsync(userId);
        return Ok(behaviors);
    }

    [HttpGet("common-actions")]
    public async Task<IActionResult> GetMostCommonActions([FromQuery] int top = 10)
    {
        var actions = await _behaviorService.GetMostCommonActionsAsync(top);
        return Ok(actions);
    }

    [HttpGet("searched-keywords")]
    public async Task<IActionResult> GetMostSearchedKeywords([FromQuery] int top = 10)
    {
        var keywords = await _behaviorService.GetMostSearchedKeywordsAsync(top);
        return Ok(keywords);
    }
}
