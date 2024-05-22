using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;


namespace Business.Services

{
    public interface IPostService
    {
        IQueryable<PostModel> Query();
        Result Add(PostModel model, int userId);
        Result Update(PostModel model, string currentUserName);
        Result Delete(int id, string currentUserName);
        List<PostModel> GetList() => Query().ToList();
        PostModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);
        List<PostModel> SearchByName(string name);

    }
     public class PostService : ServiceBase, IPostService
    {
        public PostService(Db db) : base(db)
        {
        }

        public Result Add(PostModel model, int userId)
        {
            if (_db.Posts.Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Post with the same name exists!");
            Post entity = new Post()
            {
                Id = model.Id,
                Name = model.Name.Trim(),
                PublishDate = model.PublishDate,
                Rating = model.Rating,
                LastUpdateDate = model.LastUpdateDate,
                YouTubeURL = model.YouTubeURL,
                ImageURL = model.ImageURL,
                SustainabilityScore = model.SustainabilityScore,
                BudgetPerDay = model.BudgetPerDay,
                Currency = model.Currency,
                InspirationLevel = model.InspirationLevel,
                Location = model.Location,
                MustDos = model.MustDos,
                Donts = model.Donts,
                MainContent = model.MainContent,
                UserId = userId,
                PostTags = model.TagsInput?.Select(tagInput => new PostTag()
                {
                    TagId = tagInput,
                    PostId = model.Id,
                }).ToList(),
                
                PostCategories = model.CategoriesInput?.Select(categoryInput => new PostCategory()
                {
                    CategoryId = categoryInput,
                    PostId=model.Id,
                }).ToList(),
                

            };

            _db.Posts.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult();
        }

        public List<PostModel> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<PostModel>();

            string lowerName = name.ToLower();

            return Query()
         .Where(p => p.Name.ToLower().Contains(lowerName) || p.MainContent.ToLower().Contains(lowerName))
         .ToList();
        }

        public Result Delete(int id, string currentUserName)
        {
            Post entity = _db.Posts.Include(g => g.PostTags).Include(g => g.PostCategories).Include(g => g.User).SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return new ErrorResult("Post not found!");
            if (entity.User.UserName != currentUserName) 
                return new ErrorResult("You re not the owner of this Post!"); 

            _db.PostTags.RemoveRange(entity.PostTags);
            _db.PostCategories.RemoveRange(entity.PostCategories);

            _db.Posts.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Post deleted successfully.");
        }

        public IQueryable<PostModel> Query()
        {

            return _db.Posts.Include(p => p.Comments).Include(p => p.PostTags).Include(p => p.PostCategories).OrderByDescending(p => p.Rating).ThenByDescending(p => p.SustainabilityScore).ThenBy(p => p.Name).OrderByDescending(p => p.PublishDate)
                .Select(p => new PostModel()
                {
                    // entity properties
                    Guid = p.Guid,
                    Id = p.Id,
                    Name = p.Name,
                    PublishDate = p.PublishDate,
                    LastUpdateDate = p.LastUpdateDate,
                    YouTubeURL = p.YouTubeURL,
                    ImageURL = p.ImageURL,
                    SustainabilityScore = p.SustainabilityScore,
                    BudgetPerDay = p.BudgetPerDay,
                    Currency = p.Currency,
                    InspirationLevel = p.InspirationLevel,
                    Location = p.Location,
                    MustDos = p.MustDos,
                    Donts= p.Donts,
                    MainContent = p.MainContent,
                    AuthorId = p.UserId,
                    Rating = p.Rating,
                    Categories = p.PostCategories.Select(pc => new CategoryModel()
                    {
                        Name = pc.Category.Name,
                    
                    }).ToList(),

                    Tags = p.PostTags.Select(pc => new TagModel()
                    {
                        Name = pc.Tag.Name,
                        Id = pc.Tag.Id,

                    }).ToList(),


                    // extra properties
                    AuthorOutput = p.User.UserName,
                    CommentCountOutput = p.Comments.Count(),
                    CommentsOutput = string.Join("<br />", p.Comments.Select(p => p.Content)),
                    CategoriesOutput = string.Join("<br />", p.PostCategories.Select(p => p.Category.Name)),
                    TagsOutput = string.Join("<br />", p.PostTags.Select(p => p.Tag.Name)),


                    // Way 1:
                    //TotalSalesPriceOutput = g.TotalSalesPrice != null ? g.TotalSalesPrice.Value.ToString("C2", new CultureInfo("en-US")) : "", // tr-TR
                    // Way 2:
                    //TotalSalesPriceOutput = g.TotalSalesPrice.HasValue ? g.TotalSalesPrice.Value.ToString("C2", new CultureInfo("en-US")) : string.Empty,
                    // Way 3: 
                    //TotalSalesPriceOutput = g.TotalSalesPrice.HasValue ? g.TotalSalesPrice.Value.ToString("C2") : string.Empty, // N: number format, C: currency format, 2: number of decimal digits
                    // Way 4: Managing CultureInfo in MvcControllerBase


                    // Way 1:
                    //PublishDateOutput = g.PublishDate.HasValue ? g.PublishDate.Value.ToString("MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US")) : string.Empty, // 2 digits month/2 digits day/4 digits year 2 digits hour:2 digits minute:2 digits second
                    // Way 2:
                    //PublishDateOutput = g.PublishDate.HasValue ? g.PublishDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : string.Empty
                    // Way 3: Managing CultureInfo in MvcControllerBase

                });
        }

        public Result Update(PostModel model, string currentUserName)
        {
			if (_db.Posts.Any(u => u.Id != model.Id && u.Name.ToUpper() == model.Name.ToUpper().Trim() ))
				return new ErrorResult("the same post name exists!");

            

			Post entity = _db.Posts.Include(g => g.User).SingleOrDefault(u => u.Id == model.Id);
			if (entity is null)
				return new ErrorResult("User not found!");

            if (entity.User.UserName != currentUserName)
                return new ErrorResult("You re not the owner of this Post, Only the owner can edit!");


            entity.MainContent = model.MainContent;
            entity.LastUpdateDate = DateTime.Now;
			entity.MustDos =  model.MustDos;
            entity.Donts = model.Donts;
            entity.Currency =  entity.Currency.Trim();
            entity.BudgetPerDay = model.BudgetPerDay;
            entity.YouTubeURL = model.YouTubeURL.Trim();
			entity.Location = model.Location;
			entity.Name = model.Name.Trim();
            entity.ImageURL = model.ImageURL.Trim();
            entity.InspirationLevel = model.InspirationLevel;
            entity.PublishDate = model.PublishDate;
            entity.Rating = model.Rating;
            entity.SustainabilityScore = model.SustainabilityScore;
            entity.Currency = model.Currency;

            entity.PostTags = model.TagsInput?.Select(tagInput => new PostTag()
            {
                TagId = tagInput,
                PostId = model.Id
            }).ToList();
            entity.PostCategories = model.CategoriesInput?.Select(categoryInput => new PostCategory()
            {
                CategoryId = categoryInput,
                PostId = model.Id
            }).ToList();



			// TODO: Games

			_db.Posts.Update(entity);
			_db.SaveChanges();

			return new SuccessResult("Post updated successfully.");
		}
    }
}
