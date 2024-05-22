using DataAccess.Records.Bases;
#nullable disable

namespace DataAccess.Entities
{
    public class Tag : Record
    {
        public string Name { get; set; }

        // Relationships
        public List<PostTag> PostTags { get; set; }
    }
}
