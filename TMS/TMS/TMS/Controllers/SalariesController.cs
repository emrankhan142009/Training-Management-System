using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TMS.Models;

namespace TMS.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salaries
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Index(int?Iid)
        {
            var applicationDbContext = await _context.Salary.OrderBy(i => i.InstructorId).Include(s => s.Instructor).ToListAsync();
            bool onoff = false;
            if (Iid != null)
            {
                applicationDbContext = applicationDbContext.OrderBy(i => i.InstructorId).Where(t => t.InstructorId == Iid).ToList();
                onoff = true;
            }
            ViewBag.status = onoff;
            ViewBag.stext = Iid;
            return View(applicationDbContext);
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorSalaryIndex(int? id)
        {
            var Iid = HttpContext.Session.GetInt32("InstructorId");
            var ISI = _context.Salary.OrderBy(i => i.InstructorId).Where(s => s.InstructorId == Iid || s.InstructorId == id).Include(s => s.Instructor);
            return View(await ISI.ToListAsync());
        }

        // GET: Salaries/Details/5
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Instructor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorSalaryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Instructor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // GET: Salaries/Create
        [Authorize(Roles = "Coordinator")]
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BasicSalary,Bonus,Date,InstructorId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", salary.InstructorId);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary.SingleOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", salary.InstructorId);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BasicSalary,Bonus,Date,InstructorId")] Salary salary)
        {
            if (id != salary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.Id))
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
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", salary.InstructorId);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Instructor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Coordinator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salary.SingleOrDefaultAsync(m => m.Id == id);
            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult CRSearch(int? Tid)
        {
            if (Tid != null)
            {
                var searchdata = from s in _context.Salary.Include(t => t.Instructor)
                                 where s.InstructorId == Tid
                                 select s;
                var tname = from t in _context.Instructor
                            where t.Id == Tid
                            select t.Name;
                ViewBag.tn = tname.FirstOrDefault().ToString();
                ViewBag.ti = Tid;
                var sd = searchdata.ToList();
                return View("CReport", sd);
            }
            ViewData["TraineeId"] = new SelectList(_context.Trainee, "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult CReport()
        {
            return View();
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.Id == id);
        }
    }
}
