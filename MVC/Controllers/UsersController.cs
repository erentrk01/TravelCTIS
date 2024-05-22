#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business.Services;
using Business.Models;
using MVC.Controllers.Bases;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize(Roles = "Admin,Author")]
    public class UsersController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
       
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.GetList(); 
            return View(userList); 
            
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
                // TODO: Error View
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                return RedirectToAction(nameof(Index));
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            UserModel user = null; // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                return RedirectToAction(nameof(Index));
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            UserModel user = null; // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
