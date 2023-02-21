using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Schools.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Schools.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class LessonsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public LessonsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet("Lessons")]
        [EnableCors("MyAllowOrigin")]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {

            return Ok(await _context.Lessons.ToListAsync());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [EnableCors("MyAllowOrigin")]
        
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons is null)
            {
                return NotFound();
            }
            var lessons = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
            if (lessons is null)
            {
                return NotFound();
            }
            return Ok(lessons);
        }

        // POST api/<ValuesController>
        [HttpPost("CreateLesson")]
        [EnableCors("MyAllowOrigin")]
        [Authorize(Roles ="Admin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Lessons lesson)
        {
            if (lesson is null)
            {
                return BadRequest();
            }
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return Ok("Lesson Created");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [EnableCors("MyAllowOrigin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, [FromBody] Lessons lesson)
        {
            if (id == null || _context.Lessons is null)
            {
                return NotFound();
            }
            _context.Update(lesson);
            await _context.SaveChangesAsync();
            return Ok("Lesson Updated");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [EnableCors("MyAllowOrigin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons is null)
            {
                return NotFound();
            }
            var lessons = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
            if (lessons is null)
            {
                return NotFound();
            }
            _context.Remove(lessons);
            await _context.SaveChangesAsync();
            return Ok("Lesson Deleted");
        }
    }
}
