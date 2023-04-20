using System.ComponentModel.DataAnnotations;

namespace backend.Core.Dtos.Owner
{
    public class OwnerCreateDto
    {
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }
    }
}
