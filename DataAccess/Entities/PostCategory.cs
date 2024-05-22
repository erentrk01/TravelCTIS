#nullable disable
using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class PostCategory : Record
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
