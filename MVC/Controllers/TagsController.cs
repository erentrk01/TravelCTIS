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
using System.Security.Policy;
using DataAccess.Results.Bases;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize]
    public class TagsController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: Tags
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<TagModel> tagList = _tagService.GetList();
            // TODO: Add get collection service logic here
            return View(tagList);
        }

        // GET: Tags/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            TagModel tag = _tagService.GetItem(id); // TODO: Add get item service logic here
            if (tag == null)
            {
                return NotFound();
                // TODO: Error View
            }
            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagModel tag)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _tagService.Add(tag);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = tag.Id });
            }

            return View(tag);
        }

        // GET: Tags/Edit/5
        public IActionResult Edit(int id)
        {
            TagModel tag = _tagService.GetItem(id);
            if (tag == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(tag);
        }

        // POST: Tags/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagModel tag)
        {
            if (ModelState.IsValid)
            {
                Result result = _tagService.Update(tag);
                if (result.IsSuccessful)
                {
                    TempData["EditMessage"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = tag.Id });
                }

                ModelState.AddModelError("", result.Message);

            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(tag);
        }

        // GET: Tags/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            TagModel tag = _tagService.GetItem(id);

            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            var result = _tagService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
	}
}
