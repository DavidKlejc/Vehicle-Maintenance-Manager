using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Owner;
using backend.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

            var userId = User.FindFirst("Id")?.Value;
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (currentUser == null)
            {
                return BadRequest("Current user not found");
            }

            // Map the source (Owner) to its destination dto
            Owner newOwner = _mapper.Map<Owner>(dto);
            newOwner.UserId = currentUser.Id;
            await _context.Owners.AddAsync(newOwner);
            await _context.SaveChangesAsync();

            return Ok("Owner successfully created");
        }

        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<OwnerGetDto>>> GetOwners()
        {
            var userId = User.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return BadRequest("User ID not found in JWT token");
            }

            var owners = await _context.Owners
                .Where(owner => owner.UserId == userId)
                .OrderByDescending(owner => owner.CreatedAt)
                .ToListAsync();

            // Map owners to respective dto
            var convertedOwners = _mapper.Map<IEnumerable<OwnerGetDto>>(owners);

            return Ok(convertedOwners);
        }


        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOwner([FromRoute] long id, [FromBody] OwnerCreateDto dto)
        {
            var userId = User.FindFirst("Id")?.Value;
            var owner = await _context.Owners.FirstOrDefaultAsync(q => q.ID == id && q.UserId == userId);

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
            var userId = User.FindFirst("Id")?.Value;
            var owner = await _context.Owners.FirstOrDefaultAsync(q => q.ID == id && q.UserId == userId);

            if (owner is null) return NotFound("Owner not found");

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return Ok("Owner successfully deleted");
        }

    }
}
