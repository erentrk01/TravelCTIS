#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Post : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? PublishDate { get; set; } // not required
        public DateTime? LastUpdateDate { get; set; } // not required
        public string Location { get; set; }
        public int Rating { get; set; }
        public int SustainabilityScore { get; set; }
        public string ImageURL { get; set; }

        public string YouTubeURL { get; set; }
        public double BudgetPerDay { get; set; }

        public InspirationLevels InspirationLevel { get; set; }
        public string Donts { get; set; }
        public string MustDos { get; set; }

        public string Currency { get; set; }

        public string MainContent { get; set; }

        // Relationships
        public int? UserId { get; set; }
        public User User { get; set; }

        public List<PostTag> PostTags { get; set; }

        public List<PostCategory> PostCategories { get; set; }

        public List<Comment> Comments { get; set; }



    }
}