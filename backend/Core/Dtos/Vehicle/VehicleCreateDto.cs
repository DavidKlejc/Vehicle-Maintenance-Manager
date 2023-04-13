namespace backend.Core.Dtos.Vehicle
{
    public class VehicleCreateDto
    {

        public long OwnerId { get; set; }

        public string Vin { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string ServiceRecords { get; set; }
    }
}
