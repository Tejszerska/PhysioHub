using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhysioHub.Intranet.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                // building JWT token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(8),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // save JWT token in cookie
                Response.Cookies.Append("jwtToken", tokenString, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, //  HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddHours(8)
                });

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login attempt.";
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction("Login");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name"); // 1. value 2. co wyświetla
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string role)
        {
            if (ModelState.IsValid) // jeśli dane z formularza przeszły walidację
            {
                var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }

                    ViewBag.Success = $"Account for {email} has been successfully created with role {role}.";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description); // mechanizm do błędów
                    }
                }
            }
            return View();
        }
    }
}