#nullable disable
using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PostModel : Record
    {
        #region Entity Properties
        [DisplayName("Post Title")]
        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [DisplayName("Publish Date")]
        public DateTime? PublishDate { get; set; }

        [DisplayName("Last Updated At")]
        public DateTime? LastUpdateDate { get; set; } // not required

        [DisplayName("Location")]
        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(40, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Location { get; set; }

        [Range(0, 5, ErrorMessage = "{0} must be between {1} and {2}.")]
        public int Rating { get; set; }


        [DisplayName("Sustainability Score")]
        [Range(0, 10, ErrorMessage = "{0} must be between {1} and {2}.")]
        public int SustainabilityScore { get; set; }

        [DisplayName("Image")]
        public string ImageURL { get; set; }

        [DisplayName("YouTube")]
        public string YouTubeURL { get; set; }

        [DisplayName("Budget Per Day")]

        [Range(0, double.MaxValue, ErrorMessage = "{0} must be positive!")]
        public double BudgetPerDay { get; set; }

        [DisplayName("Inspiration Level")]
        public InspirationLevels InspirationLevel { get; set; }

        [DisplayName("Don`ts !")]
        public string Donts { get; set; }

        [DisplayName("Must-Dos")]
        public string MustDos { get; set; }

        [DisplayName("Currency")]
        public string Currency { get; set; }

        [DisplayName("Content")]
        public string MainContent { get; set; }

        [DisplayName("Author")]
        public int? AuthorId { get; set; }


        #endregion

        #region Extra Properties

        [DisplayName("Author")]
        public string AuthorOutput { get; set; }
        public int CommentCountOutput { get; set; }
        public string TagsOutput { get; set; }

        public string CategoriesOutput { get; set; }

        public string CommentsOutput { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<CategoryModel> Categories{ get; set; }

        public List<int> TagsInput {  get; set; }

        public List<int> CategoriesInput { get; set; }
        #endregion
    }
}
