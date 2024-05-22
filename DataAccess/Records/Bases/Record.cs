namespace DataAccess.Records.Bases
{
    public abstract class Record
    {
        //private int _id; // field

        //public void SetId(int id) // behaviors, setter
        //{
        //    _id = id;
        //}

        //public int GetId() // behaviors, getter
        //{
        //    return _id;
        //}

        public int Id { get; set; } // property, is required

        // Way 2: Instead of assigning Guid in services' Create method, Guid can be assigned in Record abstract base class
        public string? Guid { get; set; } = System.Guid.NewGuid().ToString(); // property, is not required
    }
}