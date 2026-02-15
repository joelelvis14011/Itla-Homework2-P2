using Microsoft.AspNetCore.Mvc;
using CleanCityAPI.Data;
using CleanCityAPI.Models.DTOs;
using CleanCityAPI.Models.Entities;

namespace CleanCityAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly CleanCityDbContext _context;

        public OperatorController(CleanCityDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var operators = _context.Operators
                .Select(x => new OperatorReadDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Position = x.Position,
                    IsActive = x.IsActive
                })
                .ToList();

            return Ok(operators);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var op = _context.Operators.Find(id);

            if (op == null)
                return NotFound();

            var dto = new OperatorReadDto
            {
                Id = op.Id,
                Name = op.Name,
                Position = op.Position,
                IsActive = op.IsActive
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(OperatorCreateDto dto)
        {
            var op = new Operator
            {
                Name = dto.Name,
                Position = dto.Position,
                IsActive = dto.IsActive
            };

            _context.Operators.Add(op);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = op.Id }, op);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var op = _context.Operators.Find(id);

            if (op == null)
                return NotFound();

            _context.Operators.Remove(op);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
