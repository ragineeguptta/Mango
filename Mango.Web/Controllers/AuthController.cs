using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();

            return View(loginRequestDto);
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>
            {
                new SelectListItem { Value = SD.RoleAdmin, Text = SD.RoleAdmin },
                new SelectListItem { Value = SD.RoleCustomer, Text = SD.RoleCustomer }
            };
            ViewBag.RoleList = roleList;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;

            if (result.IsSuccess && result!=null)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                assignRole = await _authService.AssignRoleAsync(obj);
                if (assignRole.IsSuccess && assignRole != null)
                {
                    TempData["success"] = "Registration successful";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["error"] = assignRole.Message;
                }
            }

            var roleList = new List<SelectListItem>
            {
                new SelectListItem { Value = SD.RoleAdmin, Text = SD.RoleAdmin },
                new SelectListItem { Value = SD.RoleCustomer, Text = SD.RoleCustomer }
            };
            ViewBag.RoleList = roleList;

            return View(obj);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
