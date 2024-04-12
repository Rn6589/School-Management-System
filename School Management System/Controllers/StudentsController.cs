using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Context;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DatabseContext _context;
        public StudentsController(DatabseContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            var studentList = _context.Students.FromSqlRaw("EXEC GetAllStudents").ToList();
            return View(studentList);
        }
        // Get
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Students student)
        {
            if (ModelState.IsValid)
            {

                    _context.Database.ExecuteSqlInterpolated($"EXEC InsertStudent {student.Name}, {student.DOB}, {student.Phone}");
                    return RedirectToAction("Index");
                
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentId = _context.Students.FromSqlInterpolated($"EXEC GetStudentById {id}").AsEnumerable().FirstOrDefault()?.StudentId;
            var student = _context.Students.Find(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StudentId,Name,DOB,Phone")] Students student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($"EXEC UpdateStudent {student.StudentId}, {student.Name}, {student.DOB}, {student.Phone}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            return View(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student); // Pass only the student object to the view
        }


        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            // Call the stored procedure to delete the student
            _context.Database.ExecuteSqlInterpolated($"EXEC DeleteStudent {id}");

            return RedirectToAction(nameof(Index));
        }

    }
}
