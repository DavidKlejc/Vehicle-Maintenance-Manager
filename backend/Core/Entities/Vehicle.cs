namespace backend.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public String Vin { get; set; }

        public String Make { get; set; }

        public String Model { get; set; }

        public int Year { get; set; }

        public String ServiceRecords { get; set; }

        // Relation
        public long OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
