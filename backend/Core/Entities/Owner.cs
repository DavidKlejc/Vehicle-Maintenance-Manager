using Microsoft.AspNetCore.Identity;

namespace backend.Core.Entities
{
    public class Owner : BaseEntity
    {
        public String FirstName { get; set; }

        public string LastName { get; set; }

        // Relations
        public ICollection<Vehicle> Vehicles { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
