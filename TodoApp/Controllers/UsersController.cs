using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.ViewModels;

namespace TodoApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        
        public ActionResult Index()
        {
            // IdentityCore: получить всех пользователей
            var users = userManager.Users.ToList();

            return View(users);
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = userManager.Users.ToList();

            var usersForView = new List<UserWithRolesViewModel>();

            foreach(var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                usersForView.Add(new UserWithRolesViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = string.Join(", ", roles)
                });
            }

            return View(usersForView);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            //IdentityUser identityUser = userManager.Users.FirstOrDefault(u => u.Id == id);

            IdentityUser identityUser = await userManager.FindByIdAsync(id);

            // проверка админ или нет
            bool isAdmin = await userManager.IsInRoleAsync(identityUser, "admin");

            // если мы = админ когда авторизованы
            // проверка пользователя который только что открыл метод Delete
            if (User.IsInRole("admin"))
            {
                
            }


            if (identityUser != null)
            {
                await userManager.DeleteAsync(identityUser);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityUser identityUser = await userManager.FindByIdAsync(id);

            if (identityUser != null)
            {

                EditUserViewModel viewModel = new EditUserViewModel()
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    UserName = identityUser.UserName
                };



                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel userViewModel)
        {
            IdentityUser identityUser = await userManager.FindByIdAsync(userViewModel.Id);

            identityUser.Email = userViewModel.Email;
            identityUser.UserName = userViewModel.UserName;

            await userManager.UpdateAsync(identityUser);

            // работа с паролем
            if (!string.IsNullOrEmpty(userViewModel.NewPassword))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(identityUser);
                var passwordResult = await userManager.ResetPasswordAsync(identityUser, token, userViewModel.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    // ModelState изменение
                }
            }

            return RedirectToAction("Index");
        }
    }
}
