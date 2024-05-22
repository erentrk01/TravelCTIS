using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;


namespace Business.Services
{
    public interface ITagService
    {
        IQueryable<TagModel> Query();
        Result Add(TagModel model);
        Result Update(TagModel model);
        Result Delete(int id);
        List<TagModel> GetList() => Query().ToList();
        TagModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);

    }
    public class TagService : ServiceBase, ITagService
    {
        public TagService(Db db) : base(db)
        {
        }

        public Result Add(TagModel model)
        {
            if (_db.Tags.Any(t => t.Name.ToUpper() == model.Name.ToUpper().Trim() ))
                return new ErrorResult("Active user with the same user name exists!");

            Tag entity = new Tag()
            {
              
               
                Name = model.Name.Trim()

                // TODO: Games
            };

            _db.Tags.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult("Tag added successfully.");
        }

        public Result Delete(int id)
        {
            Tag entity = _db.Tags.Include(u => u.PostTags).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return new ErrorResult("Tag not found!");

            _db.PostTags.RemoveRange(entity.PostTags);
            _db.Tags.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Tag deleted successfully."); 
        }

        public IQueryable<TagModel> Query()
        {
            return _db.Tags.OrderByDescending(t => t.Name)
                .Select(t => new TagModel()
                {
                    // entity properties
                    Guid = t.Guid,
                    Id = t.Id,
                    Name = t.Name,
                    PostCount = t.PostTags.Count(),
                    PostsOutput = t.PostTags.Select(p => new PostModel()
                    {
                        Guid = p.Post.Guid,
                        Id = p.Post.Id,
                        Name = p.Post.Name,
                        PublishDate = p.Post.PublishDate,
                        LastUpdateDate = p.Post.LastUpdateDate,
                        YouTubeURL = p.Post.YouTubeURL,
                        ImageURL = p.Post.ImageURL,
                        SustainabilityScore = p.Post.SustainabilityScore,
                        BudgetPerDay = p.Post.BudgetPerDay,
                        Currency = p.Post.Currency,
                        InspirationLevel = p.Post.InspirationLevel,
                        Location = p.Post.Location,
                        MustDos = p.Post.MustDos,
                        Donts = p.Post.Donts,
                        MainContent = p.Post.MainContent,
                        AuthorId = p.Post.UserId,
                        Rating = p.Post.Rating,
                        Categories = p.Post.PostCategories.Select(pc => new CategoryModel()
                        {
                            Name = pc.Category.Name,

                        }).ToList(),

                        Tags = p.Post.PostTags.Select(pc => new TagModel()
                        {
                            Name = pc.Tag.Name

                        }).ToList(),


                        // extra properties
                        AuthorOutput = p.Post.User.UserName,
                        CommentCountOutput = p.Post.Comments.Count(),
                        CommentsOutput = string.Join("<br />", p.Post.Comments.Select(p => p.Content)),
                        CategoriesOutput = string.Join("<br />", p.Post.PostCategories.Select(p => p.Category.Name)),
                        TagsOutput = string.Join("<br />", p.Post.PostTags.Select(p => p.Tag.Name)),


                    }).ToList()
                   

                }); 
        }

        public Result Update(TagModel model)
        {
            if (_db.Tags.Any(u => u.Id != model.Id && u.Name.ToUpper() == model.Name.ToUpper().Trim() ))
                return new ErrorResult("Tag with the same  name exists!");

            Tag entity = _db.Tags.SingleOrDefault(t => t.Id == model.Id);
            if (entity is null)
                return new ErrorResult("Tag not found!");

            
            entity.Name = model.Name.Trim();

            // TODO: Games

            _db.Tags.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("User updated successfully.");
        }
    }
}
