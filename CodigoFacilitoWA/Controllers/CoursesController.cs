using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodigoFacilitoWA.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodigoFacilitoWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly CourseContext _context;

        public CoursesController(CourseContext context)
        {
            _context = context;
            if(_context.Courses.Count() == 0)
            {
                var courseEx = new Course
                {
                    Name = "Curso de ejemplo",
                    Duration = 120,
                    InstructorName = "Noemi Leon",
                    IsActive = false
                };
                _context.Courses.Add(courseEx);
                _context.SaveChanges();
            }
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course == null)
            {
                return NotFound();
            }
            return course;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseById(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
