using Final02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final02.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Final02Context _context;       

        public CoursesController(Final02Context dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses
             .FromSqlRaw("exec GetCourses")
             .ToList();
            return View(courses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( Course obj)
        {
            _context.Database.ExecuteSqlInterpolated($"InsertCourse {obj.CourseName}");
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            Course course = _context.Courses.FirstOrDefault(e => e.CourseId == id);
            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Course obj)
        {
           

            _context.Database.ExecuteSqlInterpolated($"UpdateCourse {obj.CourseId} ,{obj.CourseName}");
            return RedirectToAction("Index");

        }


        public IActionResult Delete (int id)
        {
            _context.Database.ExecuteSqlInterpolated($"DeleteCourse {id}");
            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            _context.Database.ExecuteSqlInterpolated($"GetCoursesbyId {id}");
            return RedirectToAction("Index");

        }
    }

}
