using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.ViewModels;

namespace TodoApp.Controllers
{
    public class AccountController : Controller
    {
        // Сервис отвечает за создание-удаление пользователей,
        // управление паролями, токенами, ролями, блокировка и тд
        private UserManager<IdentityUser> userManager;

        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
                SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser()
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email
                };

                // попытка создать пользователя
                IdentityResult result = await userManager.CreateAsync(identityUser, viewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                // Делаем модель не валидной ручками!
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.Email,
                    viewModel.Password, viewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    return Content("OK!");
                }
                else
                {
                    return Content("Error!");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
