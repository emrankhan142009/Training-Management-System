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
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Index(int?tid)
        {
            var applicationDbContext =await _context.Tasks.OrderBy(t => t.TraineeId).Include(t => t.Batch).Include(t => t.Course).Include(t => t.Instructor).Include(t => t.Trainee).OrderBy(t =>t.TraineeId).ToListAsync();
            bool onoff = false;
            if(tid != null)
            {
                applicationDbContext = applicationDbContext.OrderBy(t => t.TraineeId).Where(t => t.TraineeId == tid).ToList();
                onoff = true;
            }
            ViewBag.status = onoff;
            ViewBag.stext = tid;
            return View(applicationDbContext);
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorTaskIndex(int? id)
        {
            //var Iid = Convert.ToInt32(TempData["Instructorid"]);
            var Iid = HttpContext.Session.GetInt32("InstructorId");
            var ITI = _context.Tasks.OrderBy(t => t.TraineeId).Where(i => i.InstructorId == Iid || i.InstructorId == id).Include(t => t.Batch).Include(t => t.Course).Include(t => t.Instructor).Include(t => t.Trainee);
            return View(await ITI.ToListAsync());
        }

        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> TraineeTaskIndex(int? id)
        {
            var Tid = HttpContext.Session.GetInt32("TraineeId");
            var TTI = _context.Tasks.OrderBy(t => t.TraineeId).Where(t => t.TraineeId == Tid || t.TraineeId == id).Include(t => t.Batch).Include(t => t.Course).Include(t => t.Instructor).Include(t => t.Trainee);
            return View(await TTI.ToListAsync());
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult Search(int? cid, int? bid)
        {
            if (cid != null && bid != null)
            {
                var searchdata = _context.Tasks.OrderBy(t => t.TraineeId).Where(c => c.CourseId == cid && c.BatchId == bid).Include(t => t.Batch).Include(t => t.Course).Include(t => t.Instructor).Include(t => t.Trainee).ToList();
                return View("Index", searchdata);
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        // GET: Tasks/Details/5
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Batch)
                .Include(t => t.Course)
                .Include(t => t.Instructor)
                .Include(t => t.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Details for Instructor/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> InstructorTaskDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Batch)
                .Include(t => t.Course)
                .Include(t => t.Instructor)
                .Include(t => t.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            
            return View(tasks);
        }

        // GET: Tasks/Details for Trainee/5
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> TraineeTaskDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Batch)
                .Include(t => t.Course)
                .Include(t => t.Instructor)
                .Include(t => t.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create\
        [Authorize(Roles = "Instructor")]
        public IActionResult Create()
        {
            //ViewData["ReturnUrl"] = returnUrl;
            ViewBag.msg = "";
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name");
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Number,Description,AssignDate,SubmissionDate,CourseId,BatchId,TraineeId,InstructorId")] Tasks tasks)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                ViewBag.msg = "The task has been successfully assigned!";
                ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", tasks.BatchId);
                ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", tasks.CourseId);
                ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", tasks.InstructorId);
                ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", tasks.TraineeId);
                //return RedirectToAction(nameof(Index));
                //return Content("Task has been successfully assigned!");
                //return RedirectToLocal(returnUrl);
                return View("Create");
            }
            
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", tasks.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", tasks.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", tasks.InstructorId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", tasks.TraineeId);
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", tasks.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", tasks.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", tasks.InstructorId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", tasks.TraineeId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Number,Description,AssignDate,SubmissionDate,CourseId,BatchId,TraineeId,InstructorId")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", tasks.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", tasks.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "Name", tasks.InstructorId);
            ViewData["TraineeId"] = new SelectList(_context.Set<Trainee>(), "Id", "Id", tasks.TraineeId);
            return View(tasks);
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult Status()
        {
            ViewBag.msg = "The data has been changed successfully!";
            return View();
        }

        // GET: Tasks/Delete/5
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Batch)
                .Include(t => t.Course)
                .Include(t => t.Instructor)
                .Include(t => t.Trainee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [Authorize(Roles = "Instructor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Coordinator")]
        public IActionResult CRSearch(int? Tid)
        {
            if (Tid != null)
            {
                var searchdata = from s in _context.Tasks.Include(t => t.Batch).Include(t => t.Course).Include(t => t.Instructor).Include(t => t.Trainee)
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
                var searchdata = from s in _context.Tasks.Include(t => t.Batch).Include(t => t.Course).Include(t => t.Instructor).Include(t => t.Trainee)
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

        //private IActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(HomeController.Index), "Home");
        //    }
        //}

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
