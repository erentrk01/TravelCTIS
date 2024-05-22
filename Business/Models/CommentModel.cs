using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Models
{
    public class CommentModel : Record
    {
        #region Entity Properties

        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(400, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Content { get; set; }


        [DisplayName("Writed At")]
        public DateTime? PublishDate { get; set; }

        [DisplayName("Last Updated At")]
        public DateTime? LastUpdateDate { get; set; }
        #endregion

        #region Extra 

        [DisplayName("Commented By")]
        public string Username { get; set; }

        #endregion
    }
}
