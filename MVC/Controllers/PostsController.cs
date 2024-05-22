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
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Results.Bases;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class PostsController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public PostsController(IPostService postService, ITagService tagService, ICategoryService categoryService)
        {
            _postService = postService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        // GET: Posts
        public IActionResult Index(string searchString)
        {
            List<PostModel> postList;
            if (!string.IsNullOrEmpty(searchString))
            {
                postList = _postService.SearchByName(searchString);
            }
            else
            {
                postList = _postService.GetList();
            }
            ViewData["ResultCount"] = postList.Count;

            return View(postList);
        }

        // GET: Posts/Details/5
        public IActionResult Details(int id)
        {
            PostModel post = _postService.GetItem(id); // TODO: Add get item service logic here
            if (post == null)
            {
                // Way 1:
                //return NotFound();
                // Way 2:
                //return View("Error", "Game with ID " + id + " not found!");
                // Way 3:
                return View("Error", $"Post with ID {id} not found! 😔");
            }
            return View(post); ;
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name");
            ViewBag.Categories = new MultiSelectList(_categoryService.GetList(), "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(PostModel post)
        {
            if (ModelState.IsValid)
            {
				
				// Retrieve the current user's ID from the authentication context
				string userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
				int userId = int.Parse(userIdValue);


				Result result = _postService.Add(post, userId);
                if (result.IsSuccessful)
                {
                    // Way 1:
                    //return RedirectToAction("Index", "Species");
                    // Way 2:
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public IActionResult Edit(int id)
        {
            PostModel post = _postService.GetItem(id); // TODO: Add get item service logic here
            if (post == null)
            {
                return NotFound();
            }
			// TODO: Add get related items service logic here to set ViewData if necessary
			ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name");
			ViewBag.Categories = new MultiSelectList(_categoryService.GetList(), "Id", "Name");
            return View(post);
        }

        // POST: Posts/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(PostModel post)
        {
           
            if (ModelState.IsValid)
            {
                var currentUserName = User?.Identity.Name;
                Result result = _postService.Update(post, currentUserName);
				if (result.IsSuccessful)
				{
					TempData["PostMessage"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = post.Id });
				}

				ModelState.AddModelError("", result.Message);

			}
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["UserId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
			ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name");
			ViewBag.Categories = new MultiSelectList(_categoryService.GetList(), "Id", "Name");
			return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public IActionResult Delete(int id)
        {
            // TODO: Add get item service logic here
            PostModel post = _postService.GetItem(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        
        // POST: Posts/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var currentUserName = User?.Identity.Name;
            // TODO: Add delete service logic here
            var result = _postService.Delete(id, currentUserName);
            if(!result.IsSuccessful) {
                return View("Error", $"You are not the owner of this post, the post cannot be deleted! 😔");
            }
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
