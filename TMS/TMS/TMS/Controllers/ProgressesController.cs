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
    public class ProgressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Progresses
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Index(int?tid)
        {
            var applicationDbContext =await _context.Progress.OrderBy(t => t.TraineeId).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Task).Include(p => p.Trainee).ToListAsync();
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
        public async Task<IActionResult> InstructorProgressIndex(int? id)
        {
            var Iid = HttpContext.Session.GetInt32("InstructorId");
            var IPI = _context.Progress.OrderBy(t => t.TraineeId).Where(p => p.InstructorId == Iid || p.InstructorId == id).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Task).Include(p => p.Trainee);
            return View(await IPI.ToListAsync());
        }

        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> TraineeProgressIndex(int? id)
        {
            var Tid = HttpContext.Session.GetInt32("TraineeId");
            var TPI = _context.Progress.OrderBy(t => t.TraineeId).Where(p => p.TraineeId == Tid || p.TraineeId == id).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Task).Include(p => p.Trainee);
            return View(await TPI.ToListAsync());
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult Search(int? cid, int? bid)
        {
            if (cid != null && bid != null)
            {
                var searchdata = _context.Progress.OrderBy(t => t.TraineeId).Where(c => c.CourseId == cid && c.BatchId == bid).Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Task).Include(p => p.Trainee).ToList();
                return View("Index", searchdata);
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        // GET: Progresses/Details/5
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progress
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorProgressDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progress
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> TraineeProgressDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progress
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // GET: Progresses/Create
        [Authorize(Roles = "Instructor")]
        public IActionResult Create()
        {
            ViewBag.msg = "";
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name");
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id");
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id");
            return View();
        }

        // POST: Progresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Completed,Date,CourseId,BatchId,TraineeId,TaskId,InstructorId")] Progress progress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progress);
                await _context.SaveChangesAsync();
                ViewBag.msg = "The Progress has been submitted successfully!";
                ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", progress.BatchId);
                ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", progress.CourseId);
                ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", progress.InstructorId);
                ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", progress.TaskId);
                ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", progress.TraineeId);
                return View("Create");
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", progress.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", progress.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", progress.InstructorId);
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", progress.TaskId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", progress.TraineeId);
            return View(progress);
        }

        // GET: Progresses/Edit/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progress.SingleOrDefaultAsync(m => m.Id == id);
            if (progress == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", progress.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", progress.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", progress.InstructorId);
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", progress.TaskId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", progress.TraineeId);
            return View(progress);
        }

        // POST: Progresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Completed,Date,CourseId,BatchId,TraineeId,TaskId,InstructorId")] Progress progress)
        {
            if (id != progress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgressExists(progress.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", progress.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", progress.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", progress.InstructorId);
            ViewData["TaskId"] = new SelectList(_context.Set<Tasks>(), "Id", "Id", progress.TaskId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", progress.TraineeId);
            return View(progress);
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult Status()
        {
            ViewBag.msg = "The data has been changed successfully!";
            return View();
        }

        // GET: Progresses/Delete/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progress
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Instructor)
                .Include(p => p.Task)
                .Include(p => p.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // POST: Progresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var progress = await _context.Progress.SingleOrDefaultAsync(m => m.Id == id);
            _context.Progress.Remove(progress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult CRSearch(int? Tid)
        {
            if (Tid != null)
            {
                var searchdata = from s in _context.Progress.Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Task).Include(p => p.Trainee)
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
                var searchdata = from s in _context.Progress.Include(p => p.Batch).Include(p => p.Course).Include(p => p.Instructor).Include(p => p.Task).Include(p => p.Trainee)
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

        private bool ProgressExists(int id)
        {
            return _context.Progress.Any(e => e.Id == id);
        }
    }
}
