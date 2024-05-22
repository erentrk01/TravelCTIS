#nullable disable

using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserModel : Record
    {
        #region Entity Properties
        [DisplayName("User Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Password { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        public Experiences Experience { get; set; } // junior, senior, master

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Role")]
        public int? RoleId { get; set; } // type should be nullable for Required data annotation
        #endregion

        #region Extra Properties
        public RoleModel RoleOutput { get; set; }

        [DisplayName("Active")]
        public string IsActiveOutput { get; set; }

        
        public List<Post> PostsOutput { get; set; }
        public List<CommentModel> CommentsOutput { get; set; }
        #endregion
    }
}