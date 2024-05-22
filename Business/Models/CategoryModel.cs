using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Records.Bases;
using DataAccess.Entities;
#nullable disable
namespace Business.Models
{
    public class CategoryModel : Record
    {
        #region Entity Properties
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; }
        #endregion
        #region Extra Properties
        [DisplayName("Post Count")]
        public int PostCount { get; set; }
        public List<PostModel> PostsOutput { get; set; }
        #endregion
    }
}
