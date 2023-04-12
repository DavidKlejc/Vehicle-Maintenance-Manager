using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Owner;
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
    }
}
