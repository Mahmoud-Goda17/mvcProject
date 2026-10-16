using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
           _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDtos> Users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                Users = _userManager.Users.Select(U => new UserToReturnDtos
                {
                    Id = U.Id,
                    UserName=U.UserName,
                    FristName=U.FristName,
                    LastName=U.LastName,
                    Email=U.Email,
                    Roles=_userManager.GetRolesAsync(U).Result

                });
            }
            else
            {
                Users = _userManager.Users.Select(U => new UserToReturnDtos
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FristName = U.FristName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).Where(U=>U.FristName.ToLower().Contains(SearchInput.ToLower()));

            }
            return View(Users);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invaild Id");
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"Employeewith Id{id} is not found"
            }
            );
            var dto = new UserToReturnDtos()
            {
                Id = user.Id,
                UserName = user.UserName,
                FristName = user.FristName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result


            };
                
            return View(ViewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDtos model )
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invaild Id");
                user.UserName = model.UserName;
                user.FristName = model.FristName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
                return View( model);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDtos model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invaild Id");
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
            return View(model);

        }

    }
}
