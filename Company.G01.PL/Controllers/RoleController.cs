using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
           _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDtos> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDtos
                {
                    Id = U.Id,
                    Name = U.Name,
                });
            }
            else
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDtos
                {
                    Id = U.Id,
                    Name = U.Name,

                }).Where(U => U.Name.ToLower().Contains(SearchInput.ToLower()));

            }
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invaild Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"Employeewith Id{id} is not found"
            }
            );
            var dto = new RoleToReturnDtos()
            {
                Id = role.Id,
                Name = role.Name,


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
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDtos model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invaild Id");
                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDtos model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invaild Id");
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
            return View(model);

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(RoleToReturnDtos model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");

                    }
                }


            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleid)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role is null) return NotFound();
            ViewData["RoleId"] = roleid;
            var UsersInRole = new List<UserInRoleDtos>();
            var Users =  _userManager.Users.ToList();
            foreach (var user in Users)
            {
                var userinrole = new UserInRoleDtos()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userinrole.IsSelected = true;
                }
                else
                {
                    userinrole.IsSelected = false;
                }
                UsersInRole.Add(userinrole);
            }
            return View(UsersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleid,List<UserInRoleDtos> users)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role is null) return NotFound();
            if(ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    if (appuser is not null)
                    {
                        if(user.IsSelected&& ! await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        }
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);

                        }
                    }

                }
                return RedirectToAction("Edit", new {id=roleid});

            }
            return View(users);
        }


    }
}
