using DataAccess.Records.Bases;
#nullable disable


namespace DataAccess.Entities
{
    public class PostTag : Record
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
