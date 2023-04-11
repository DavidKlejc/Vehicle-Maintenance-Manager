namespace backend.Core.Entities
{
    public class Owner : BaseEntity
    {
        public String FirstName { get; set; }

        public string LastName { get; set; }

        // Relation
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
