using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleManager.Models;
using VehicleManager.Models.DTOs;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet("{userId}", Name = "GetUserDetails")]
    public async Task<ActionResult<ServiceResponse<User>>> GetUserDetails(string userId)
    {
        var serviceResponse = new ServiceResponse<User>();
        try
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            serviceResponse.Data = user;
            return Ok(serviceResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching user details.");
        }
    }

    [HttpPost("update")]
    public async Task<ActionResult<ServiceResponse<User>>> Update([FromBody] UserDto model)
    {
        var serviceResponse = new ServiceResponse<User>();
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            serviceResponse.ErrorList.Add("User not found.");
            return NotFound(serviceResponse);
        }

        user.Name = model.Name;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Ok(serviceResponse);
        }
        result.Errors.ToList().ForEach(error => serviceResponse.ErrorList.Add(error.ToString()));
        return BadRequest(serviceResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto model)
    {
        var newUser = new User { UserName = model.UserName, Email = model.Email, Name = model.Name };
        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = GenerateJwtToken(user);
            return Ok(new { Message = "Usuário registrado com sucesso", Token = token });
        }

        return BadRequest(new { Errors = result.Errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.Name);
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized(new { Message = "User or password incorrect" });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(90),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
