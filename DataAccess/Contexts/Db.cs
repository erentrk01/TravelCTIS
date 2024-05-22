using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }


        public Db(DbContextOptions options) : base(options) // super in Java
        {

        }
    }
}