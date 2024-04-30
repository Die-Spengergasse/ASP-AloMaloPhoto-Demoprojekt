using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.General.IdentityProvider.Models;
using Spg.General.IdentityProvider.Services;
using System.Diagnostics;

namespace Spg.General.IdentityProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger, 
            JwtProvider jwtProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            ViewData["Roles"] = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "User", Value = "user" },
                new SelectListItem() { Text = "Admin", Value = "admin" },
            };

            LoginDto model = new LoginDto("", "", "user", _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]);
            return View(model);
        }

        [HttpPost()]
        public IActionResult Index(LoginDto dto)
        {
            try
            {
                UserInformationDto userInformationDto = _jwtProvider
                    .GenerateToken(dto, new TimeSpan(0, 5, 0));
                return View(nameof(Details), userInformationDto);
            }
            catch (AuthenticationException ex)
            {
                ModelState.AddModelError("WrongUser", ex.Message);
                return View(dto);
            }
        }

        [HttpGet()]
        public IActionResult Details(UserInformationDto userInformationDto)
        {
            return View(userInformationDto);
        }

        [HttpGet()]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
