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
    public class PerformancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Performances
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Index(int?tid)
        {
            var applicationDbContext =await _context.Performance.OrderBy(t => t.TraineeId).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Progress).Include(p => p.Task).Include(p => p.Trainee).ToListAsync();
            bool onoff = false;
            if (tid != null)
            {
                applicationDbContext = applicationDbContext.OrderBy(t => t.TraineeId).Where(t => t.TraineeId == tid).ToList();
                onoff = true;
            }
            ViewBag.status = onoff;
            ViewBag.stext = tid;
            return View(applicationDbContext);
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorPerformanceIndex(int? id)
        {
            var Iid = HttpContext.Session.GetInt32("InstructorId");
            var IPI = _context.Performance.OrderBy(t => t.TraineeId).Where(p => p.InstructorId == Iid || p.InstructorId == id).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Progress).Include(p => p.Task).Include(p => p.Trainee);
            return View(await IPI.ToListAsync());
        }

        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> TraineePerformanceIndex(int? id)
        {
            var Tid = HttpContext.Session.GetInt32("TraineeId");
            var TPI = _context.Performance.OrderBy(t => t.TraineeId).Where(p => p.TraineeId == Tid || p.TraineeId == id).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Progress).Include(p => p.Task).Include(p => p.Trainee);
            return View(await TPI.ToListAsync());
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult Search(int? cid, int? bid)
        {
            if (cid != null && bid != null)
            {
                var searchdata = _context.Performance.OrderBy(t => t.TraineeId).Where(c => c.CourseId == cid && c.BatchId == bid).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Progress).Include(p => p.Task).Include(p => p.Trainee).ToList();
                return View("Index", searchdata);
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        // GET: Performances/Details/5
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Progress)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorPerformanceDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Progress)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> TraineePerformanceDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Progress)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // GET: Performances/Create
        [Authorize(Roles = "Instructor")]
        public IActionResult Create()
        {
            ViewBag.msg = "";
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name");
            ViewData["ProgressId"] = new SelectList(_context.Set<Progress>(), "Id", "Id");
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id");
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id");
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Accuracy,CourseId,BatchId,TraineeId,TaskId,InstructorId,ProgressId")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performance);
                await _context.SaveChangesAsync();
                ViewBag.msg = "The performance has been submitted successfully!";
                ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", performance.BatchId);
                ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", performance.CourseId);
                ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", performance.InstructorId);
                ViewData["ProgressId"] = new SelectList(_context.Set<Progress>(), "Id", "Id", performance.ProgressId);
                ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", performance.TaskId);
                ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", performance.TraineeId);
                //return RedirectToAction(nameof(Index));
                return View("Create");
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", performance.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", performance.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", performance.InstructorId);
            ViewData["ProgressId"] = new SelectList(_context.Set<Progress>(), "Id", "Id", performance.ProgressId);
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", performance.TaskId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", performance.TraineeId);
            return View(performance);
        }

        // GET: Performances/Edit/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance.SingleOrDefaultAsync(m => m.Id == id);
            if (performance == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", performance.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", performance.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", performance.InstructorId);
            ViewData["ProgressId"] = new SelectList(_context.Set<Progress>(), "Id", "Id", performance.ProgressId);
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", performance.TaskId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", performance.TraineeId);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Accuracy,CourseId,BatchId,TraineeId,TaskId,InstructorId,ProgressId")] Performance performance)
        {
            if (id != performance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceExists(performance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Status));
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", performance.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", performance.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", performance.InstructorId);
            ViewData["ProgressId"] = new SelectList(_context.Set<Progress>(), "Id", "Id", performance.ProgressId);
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", performance.TaskId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", performance.TraineeId);
            return View(performance);
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult Status()
        {
            ViewBag.msg = "The data has been changed successfully!";
            return View();
        }

        // GET: Performances/Delete/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Progress)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performance = await _context.Performance.SingleOrDefaultAsync(m => m.Id == id);
            _context.Performance.Remove(performance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult CRSearch(int? Tid)
        {
            if (Tid != null)
            {
                var searchdata = from s in _context.Performance.Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Progress).Include(p => p.Task).Include(p => p.Trainee)
                                 where s.TraineeId == Tid
                                 select s;
                var tname = from t in _context.Trainee
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

        [Authorize(Roles = "Instructor")]
        public IActionResult IRSearch(int? Tid)
        {
            if (Tid != null)
            {
                var searchdata = from s in _context.Performance.Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Progress).Include(p => p.Task).Include(p => p.Trainee)
                                 where s.TraineeId == Tid
                                 select s;
                var tname = from t in _context.Trainee
                            where t.Id == Tid
                            select t.Name;
                ViewBag.tn = tname.FirstOrDefault().ToString();
                ViewBag.ti = Tid;
                var sd = searchdata.ToList();
                return View("IReport", sd);
            }
            ViewData["TraineeId"] = new SelectList(_context.Trainee, "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult IReport()
        {
            return View();
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performance.Any(e => e.Id == id);
        }
    }
}
