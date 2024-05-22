#nullable disable
using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class Comment : Record
    {

        public string Content { get; set; }
        public DateTime? PublishDate { get; set; } 
        public DateTime? LastUpdateDate { get; set; }

        // Relationships
        public int PostId { get; set; }

        public Post Post { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }




    }
}
