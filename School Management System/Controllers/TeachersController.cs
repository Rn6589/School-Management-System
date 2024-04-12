
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Context;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    public class TeachersController : Controller
    {
        private readonly DatabseContext _context;

        public TeachersController(DatabseContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            var teacherList = _context.Teachers.FromSqlRaw("EXEC GetAllTeachers").ToList();

            return View(teacherList);
        }

       

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {

                _context.Database.ExecuteSqlInterpolated($"EXEC InsertTeacher {teacher.Name}, {teacher.Subject}, {teacher.Email}");
                return RedirectToAction("Index");

            }
            return View(teacher);
        }

        // GET: Teachers/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherId = _context.Teachers.FromSqlInterpolated($"EXEC GetTeacherById {id}").AsEnumerable().FirstOrDefault()?.TeacherId;
            var teacher = _context.Teachers.Find(teacherId);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($"EXEC UpdateTeacher {teacher.TeacherId}, {teacher.Name}, {teacher.Subject}, {teacher.Email}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }

        // GET: Teacher/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher); // Pass only the teacher object to the view
        }


        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            // Call the stored procedure to delete the student
            _context.Database.ExecuteSqlInterpolated($"EXEC DeleteTeacher {id}");

            return RedirectToAction(nameof(Index));
        }

    }
}

