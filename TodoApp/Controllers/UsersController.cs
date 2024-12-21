using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.ViewModels;

namespace TodoApp.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            //IdentityUser identityUser = userManager.Users.FirstOrDefault(u => u.Id == id);

            IdentityUser identityUser = await userManager.FindByIdAsync(id);

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

            return RedirectToAction("Index");
        }
    }
}
