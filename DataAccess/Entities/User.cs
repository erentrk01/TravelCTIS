#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DataAccess.Entities
{
    public class User : Record
    {
        [Required]
        [StringLength(18)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public Experiences Experience { get; set; } 


        // Relationships

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }
    }
}