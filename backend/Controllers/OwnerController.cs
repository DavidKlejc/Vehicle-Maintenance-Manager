using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Owner;
using backend.Core.Dtos.Vehicle;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public OwnerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the source (Owner) to its destination dto
            Owner newOwner = _mapper.Map<Owner>(dto);
            await _context.Owners.AddAsync(newOwner);
            await _context.SaveChangesAsync();

            return Ok("Owner successfully created");
        }

        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<OwnerGetDto>>> GetOwners()
        {
            // Return list of owners based on creation date
            var owners = await _context.Owners.OrderByDescending(owner => owner.CreatedAt).ToListAsync();
            // Map owners to respective dto
            var convertedOwners = _mapper.Map<IEnumerable<OwnerGetDto>>(owners);

            return Ok(convertedOwners);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOwner([FromRoute] long id, [FromBody] OwnerCreateDto dto)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(q => q.ID == id);

            if (owner is null) return NotFound("Owner not found");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            owner.FirstName = dto.FirstName;
            owner.LastName = dto.LastName;
            owner.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Owner successfully updated");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOwner([FromRoute] long id)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(q => q.ID == id);

            if (owner is null) return NotFound("Owner not found");

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return Ok("Owner successfully deleted");
        }
    }
}
