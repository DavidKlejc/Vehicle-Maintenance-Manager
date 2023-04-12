namespace backend.Core.Dtos.Owner
{
    public class OwnerGetDto
    {
        public long ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
