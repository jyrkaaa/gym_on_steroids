using App.DAL;
using App.Domain.EF.Identity;
using App.Domain.Identity;
using App.DTO.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Identity;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController: ControllerBase
{
    
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly Random _random = new Random();
    private readonly AppDbContext _context;

    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger, AppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    
    public async Task<ActionResult<JWTResponse>> Login(
        [FromBody]
        LoginInfo loginInfo,
        [FromQuery]
        int jwtExpiresInSeconds,
        [FromQuery]
        int refreshTokenExpiresInSeconds
    )
    {
        if (jwtExpiresInSeconds <= 0) jwtExpiresInSeconds = int.MaxValue;
        jwtExpiresInSeconds = jwtExpiresInSeconds < _configuration.GetValue<int>("JWTSecurity:ExpiresInSeconds")
            ? jwtExpiresInSeconds
            : _configuration.GetValue<int>("JWTSecurity:ExpiresInSeconds");

        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            await Task.Delay(_random.Next(1000,5000));
            return NotFound("User/Password problem");
        } 
        
        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginInfo.Password,
                loginInfo.Email);
            await Task.Delay(_random.Next(1000,5000));
            return NotFound("User/Password problem");
        }

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (!_context.Database.ProviderName!.Contains("InMemory"))
        {
            var deletedRows = await _context.RefreshTokens
                .Where(t => t.UserId == appUser.Id && t.Expiration < DateTime.UtcNow)
                .ExecuteDeleteAsync();
            _logger.LogInformation("Deleted {} refresh tokens", deletedRows);
        }
        else
        {
            //TODO: inMemory delete for testing
        }
        
        
        // todo: set refresh token expiration
        var refreshToken = new AppRefreshToken()
        {
            UserId = appUser.Id
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        
        
        
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWTSecurity:Key")!,
            _configuration.GetValue<string>("JWTSecurity:Issuer")!,
            _configuration.GetValue<string>("JWTSecurity:Audience")!,
            jwtExpiresInSeconds
        );
        
        var responseData = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken
        };
        
        return Ok(responseData);
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] LoginInfo registerInfo)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid registration data.");

        var existingUser = await _userManager.FindByEmailAsync(registerInfo.Email);
        if (existingUser != null)
        {
            return BadRequest("Email already registered.");
        }

        var newUser = new AppUser
        {
            Email = registerInfo.Email,
            UserName = registerInfo.Email
        };

        var result = await _userManager.CreateAsync(newUser, registerInfo.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { Errors = errors });
        }

        return Ok("User registered successfully.");
    }
    /// <summary>
    /// Refreshes JWT using a valid refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token string.</param>
    /// <returns>New JWT and the same (or new) refresh token.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(JWTResponse), 200)]
    [ProducesResponseType(401)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> RefreshToken([FromBody] string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken && t.Expiration > DateTime.UtcNow);

        if (storedToken == null || storedToken.User == null)
        {
            return Unauthorized("Invalid or expired refresh token");
        }

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(storedToken.User);

        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWTSecurity:Key")!,
            _configuration.GetValue<string>("JWTSecurity:Issuer")!,
            _configuration.GetValue<string>("JWTSecurity:Audience")!,
            _configuration.GetValue<int>("JWTSecurity:ExpiresInSeconds")
        );

        storedToken.Expiration = DateTime.UtcNow.AddDays(7);
        await _context.SaveChangesAsync();

        return Ok(new JWTResponse
        {
            JWT = jwt,
            RefreshToken = refreshToken
        });
    }


}
