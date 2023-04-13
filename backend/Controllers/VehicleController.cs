using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Owner;
using backend.Core.Dtos.Vehicle;
using backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public VehicleController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleCreateDto dto)
        {
            var newVehicle = _mapper.Map<Vehicle>(dto);
            newVehicle.OwnerId = dto.OwnerId;
            await _context.Vehicles.AddAsync(newVehicle);
            await _context.SaveChangesAsync();

            return Ok("Vehicle successfully created");
        }

        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<OwnerGetDto>>> GetVehicles()
        {
            var vehicles = await _context.Vehicles.Include(vehicle => vehicle.Owner).OrderByDescending(q => q.CreatedAt).ToListAsync();
            var convertedVehicles = _mapper.Map<IEnumerable<VehicleGetDto>>(vehicles);

            return Ok(convertedVehicles);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute] long id, [FromBody] VehicleCreateDto dto)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(q => q.ID == id);

            if (vehicle is null) return NotFound("Vehicle not found");

            vehicle.Make = dto.Make;
            vehicle.Model = dto.Model;
            vehicle.Year = dto.Year;
            vehicle.ServiceRecords = dto.ServiceRecords;
            vehicle.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Vehicle successfully updated");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] long id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(q => q.ID == id);

            if (vehicle is null) return NotFound("Vehicle not found");

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok("Vehicle successfully deleted");
        }
    }
}
