using DataAccess.Records.Bases;
#nullable disable

namespace DataAccess.Entities
{
    public class Category : Record
    {
        public string Name { get; set; }
        // Relationships
        public List<PostCategory> PostCategories { get; set; }
    }
}
