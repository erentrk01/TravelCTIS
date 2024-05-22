#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using MVC.Controllers.Bases;
using Azure;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class CategoriesController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public IActionResult Index()
        {

            List<CategoryModel> categoryList = _categoryService.GetList();
            // TODO: Add get collection service logic here
            return View(categoryList);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int id)
        {
            CategoryModel category = _categoryService.GetItem(id); // TODO: Add get item service logic here
            if (category == null)
            {
                return NotFound();
                // TODO: Error View
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        // GET: Categories/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _categoryService.Add(category);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = category.Id });
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {

            CategoryModel category = _categoryService.GetItem(id);
            if (category == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(category);
        }

        // POST: Categories/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                Result result = _categoryService.Update(category);
                if (result.IsSuccessful)
                {
                    TempData["CategoryMessage"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = category.Id });
                }

                ModelState.AddModelError("", result.Message);

            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            CategoryModel category = _categoryService.GetItem(id); // TODO: Add get item service logic here
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            var result = _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
	}
}
