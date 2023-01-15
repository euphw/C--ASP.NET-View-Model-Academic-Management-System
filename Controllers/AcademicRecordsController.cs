using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab6.Models.DataAccess;
using System.Security.Cryptography.X509Certificates;
using Lab6.Models.ViewModel;

namespace Lab6.Controllers
{
    public class AcademicRecordsController : Controller
    {
        private readonly StudentRecordContext _context;

        public AcademicRecordsController(StudentRecordContext context)
        {
            _context = context;
        }
        //public string Sort;
        // GET: AcademicRecords
        public async Task<IActionResult> Index(string sort)
        {
            //var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);

            //----------------------my code start -------------------------------------------------//

            if (sort != null)
            {
                HttpContext.Session.SetString("sort", sort);
            }
            else if (HttpContext.Session.GetString("sort") != null)
            {
                sort = HttpContext.Session.GetString("sort");
            }
            else
            {
                sort = null;
            }

            if (sort == "course")
            {
                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).OrderBy(item => item.CourseCodeNavigation.Title);
                return View(await studentRecordContext.ToListAsync());
            }
            else if (sort == "name")
            {

                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).OrderByDescending(item => item.Student.Name);
                return View(await studentRecordContext.ToListAsync());
            }
            else if (sort == "grade")
            {
                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).OrderByDescending(item => item.Grade);
                return View(await studentRecordContext.ToListAsync());
            }
            else
            {
                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);
                return View(await studentRecordContext.ToListAsync());
            }

            //------------------------------my code end ------------------------------------//
           // return View(await studentRecordContext.ToListAsync());
        }

        public async Task<IActionResult> EditAll(string sort)
        {
           
            //----------------------my code start -------------------------------------------------//

            if (sort != null)
            {
                HttpContext.Session.SetString("sort", sort);
            }
            else if (HttpContext.Session.GetString("sort") != null)
            {
                sort = HttpContext.Session.GetString("sort");
            }
            else
            {
                sort = null;
            }

            if (sort == "course")
            {
                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).OrderBy(item => item.CourseCodeNavigation.Title);
                return View(await studentRecordContext.ToListAsync());
            }
            else if (sort == "name")
            {

                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).OrderByDescending(item => item.Student.Name);
                return View(await studentRecordContext.ToListAsync());
            }
            else if (sort == "grade")
            {
                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).OrderByDescending(item => item.Grade);
                return View(await studentRecordContext.ToListAsync());
            }
            else
            {
                var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);
                return View(await studentRecordContext.ToListAsync());
            }
            //------------------------------my code end ------------------------------------//
            // return View(await studentRecordContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAll([Bind("CourseCode,StudentId,Grade")] List<AcademicRecord> AcademicRecords)
        {
            //for (int i = 0; i < AcademicRecords.Count; i++)
            //{
            //    if (AcademicRecords[i].Grade.GetType() !=  )
            //    {
            //        ModelState.AddModelError("AcademicRecords[i].Grade", "You must select at least one role!");
            //    }
            //}


            if (ModelState.IsValid)
            {
                foreach (AcademicRecord record in AcademicRecords)
                {

                    _context.Update(record);

                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(AcademicRecords);

        }



        // GET: AcademicRecords/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.AcademicRecords == null)
        //    {
        //        return NotFound();
        //    }

        //    var academicRecord = await _context.AcademicRecords
        //        .Include(a => a.CourseCodeNavigation)
        //        .Include(a => a.Student)
        //        .FirstOrDefaultAsync(m => m.StudentId == id);
        //    if (academicRecord == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(academicRecord);
        //}

        // GET: AcademicRecords/Create
        public IActionResult Create()
        {
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: AcademicRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        // GET: AcademicRecords/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }

            var academicRecord = await _context.AcademicRecords.FindAsync(id);
            if (academicRecord == null)
            {
                return NotFound();
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        // POST: AcademicRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (id != academicRecord.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicRecordExists(academicRecord.StudentId))
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
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        // GET: AcademicRecords/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _context.AcademicRecords == null)
        //    {
        //        return NotFound();
        //    }

        //    var academicRecord = await _context.AcademicRecords
        //        .Include(a => a.CourseCodeNavigation)
        //        .Include(a => a.Student)
        //        .FirstOrDefaultAsync(m => m.StudentId == id);
        //    if (academicRecord == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(academicRecord);
        //}

        // POST: AcademicRecords/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.AcademicRecords == null)
        //    {
        //        return Problem("Entity set 'StudentRecordContext.AcademicRecords'  is null.");
        //    }
        //    var academicRecord = await _context.AcademicRecords.FindAsync(id);
        //    if (academicRecord != null)
        //    {
        //        _context.AcademicRecords.Remove(academicRecord);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AcademicRecordExists(string id)
        {
            return _context.AcademicRecords.Any(e => e.StudentId == id);
        }
    }
}
