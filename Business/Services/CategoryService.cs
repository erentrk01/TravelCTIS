

using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface ICategoryService
    {
        IQueryable<CategoryModel> Query();
        Result Add(CategoryModel model);
        Result Update(CategoryModel model);
        Result Delete(int id);
        List<CategoryModel> GetList() => Query().ToList();
        CategoryModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);

    }
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(Db db) : base(db)
        {
        }

        public Result Add(CategoryModel model)
        {
            if (_db.Categories.Any(u => u.Id != model.Id && u.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Category with the same  name exists!");

             Category entity = new Category()
         {


                Name = model.Name.Trim()

                // TODO: Games
            };

            _db.Categories.Add(entity);
            _db.SaveChanges();

         

            return new SuccessResult("Categories updated successfully.");
        }

        public Result Delete(int id)
        {
            Category entity = _db.Categories.Include(u => u.PostCategories).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return new ErrorResult("Tag not found!");

            _db.PostCategories.RemoveRange(entity.PostCategories);
            _db.Categories.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Tag deleted successfully."); ;
        }

        public IQueryable<CategoryModel> Query()
        {
            return _db.Categories
                .OrderByDescending(t => t.Name)
                .Select(t => new CategoryModel()
                {
                    // Entity properties
                    Guid = t.Guid,
                    Id = t.Id,
                    Name = t.Name,
                    PostCount = t.PostCategories.Count(),
                    PostsOutput = t.PostCategories
                        .Select(pc => new PostModel()
                        {
                            Guid = pc.Post.Guid,
                            Id = pc.Post.Id,
                            Name = pc.Post.Name,
                            PublishDate = pc.Post.PublishDate,
                            LastUpdateDate = pc.Post.LastUpdateDate,
                            YouTubeURL = pc.Post.YouTubeURL,
                            ImageURL = pc.Post.ImageURL,
                            SustainabilityScore = pc.Post.SustainabilityScore,
                            BudgetPerDay = pc.Post.BudgetPerDay,
                            Currency = pc.Post.Currency,
                            InspirationLevel = pc.Post.InspirationLevel,
                            Location = pc.Post.Location,
                            MustDos = pc.Post.MustDos,
                            Donts = pc.Post.Donts,
                            MainContent = pc.Post.MainContent,
                            AuthorId = pc.Post.UserId,
                            Rating = pc.Post.Rating,
                            Categories = pc.Post.PostCategories.Select(pcc => new CategoryModel()
                            {
                                Name = pcc.Category.Name,
                            }).ToList()
                        }).ToList() // Ensure the collection is converted to a List
                });
        }
        public Result Update(CategoryModel model)
        {
            if (_db.Categories.Any(u => u.Id != model.Id && u.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Category with the same  name exists!");

            Category entity = _db.Categories.SingleOrDefault(t => t.Id == model.Id);
            if (entity is null)
                return new ErrorResult("Category not found!");


            entity.Name = model.Name.Trim();

            _db.Categories.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("Category updated successfully.");
        
    }
    }
}
