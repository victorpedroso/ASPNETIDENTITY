using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SecurityWEB.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace SecurityWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            return View(name);
        }

        private void Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<IdentityUser> members = new List<IdentityUser>();
            List<IdentityUser> nonMembers = new List<IdentityUser>();

            var users = await _userManager.Users.ToListAsync();

            foreach (IdentityUser user in users) 
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;

                list.Add(user);
            }

            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification roleModification)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in roleModification.AddIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, roleModification.RoleName);

                        if (!result.Succeeded) 
                        {
                            Errors(result);
                        }
                    }
                }
                foreach (string userId in roleModification.DeleteIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByEmailAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, roleModification.RoleName);
                    }
                }
            }
            if (ModelState.IsValid) 
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Update(roleModification.RoleId);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role não encontrada");
                return View("Index", _roleManager.Roles);
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Role não encontrada");
            }
            return View("Index", _roleManager.Roles);
        }
    }
}
