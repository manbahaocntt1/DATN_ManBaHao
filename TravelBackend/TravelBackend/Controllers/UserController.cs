using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Entities;
using TravelBackend.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService) => _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            await _userService.RegisterAsync(dto.FullName, dto.Email, dto.Password, dto.Nationality, dto.PreferredLang, dto.AvatarUrl);
            return Ok("Registration successful.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);
        if (user == null) return Unauthorized("Invalid credentials or inactive account.");
        return Ok(user);
    }

    [HttpPost("social-login")]
    public async Task<IActionResult> SocialLogin([FromBody] SocialLoginDto dto)
    {
        var user = await _userService.SocialLoginAsync(dto.Provider, dto.SocialId, dto.FullName, dto.Email, dto.AvatarUrl);
        return Ok(user);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var result = await _userService.ResetPasswordAsync(dto.Email, dto.NewPassword);
        if (!result) return NotFound("Email not found.");
        return Ok("Password reset successful.");
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        await _userService.ChangePasswordAsync(dto.UserId, dto.NewPassword);
        return Ok("Password changed.");
    }

    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
        var user = new Users
        {
            UserId = dto.UserId,
            FullName = dto.FullName,
            Nationality = dto.Nationality,
            PreferredLang = dto.PreferredLang,
            AvatarUrl = dto.AvatarUrl
        };
        await _userService.UpdateProfileAsync(user);
        return Ok("Profile updated.");
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound("User not found.");
        return Ok(user);
    }


    [HttpPost("disable/{userId}")]
    public async Task<IActionResult> DisableUser(int userId)
    {
        await _userService.DisableUserAsync(userId);
        return Ok("User disabled.");
    }

    [HttpPost("enable/{userId}")]
    public async Task<IActionResult> EnableUser(int userId)
    {
        await _userService.EnableUserAsync(userId);
        return Ok("User enabled.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] bool? isActive = null)
    {
        var users = await _userService.GetAllUsersAsync(isActive);
        return Ok(users);
    }
    [HttpPost("upload-avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"{Request.Scheme}://{Request.Host}/images/avatars/{uniqueFileName}";
        return Ok(new { url = fileUrl });
    }

}
