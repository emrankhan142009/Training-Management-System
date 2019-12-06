using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TMS.Models;

namespace TMS.Controllers
{
   // [Authorize(Roles = "Coordinator")]
    public class TraineesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TraineesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult LandingPage()
        {
            return View();
        }

        // GET: Trainees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trainee.Include(t => t.Batch).Include(t => t.Course).OrderBy(t => t.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Search(int?cid, int? bid)
        {
            if(cid != null && bid != null)
            {
                var searchdata = _context.Trainee.Where(c => c.CourseId == cid && c.BatchId == bid).Include(t => t.Batch).Include(t => t.Course).OrderBy(t => t.Id).ToList();
                return View("Index", searchdata);
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Number");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        // GET: Trainees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainee
                .Include(t => t.Batch)
                .Include(t => t.Course)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        // GET: Trainees/Create
        public IActionResult Create()
        {
            ViewBag.course = _context.Course.ToList();
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Number");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        //public IActionResult getBatch(int id)
        //{
        //        var batchlist = _context.Batch.Where(b => b.CourseId == id).ToList();
        //        return Json(batchlist); 
        //}

        // POST: Trainees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Contact,Email,CourseId,BatchId")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Number", trainee.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", trainee.CourseId);
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainee.SingleOrDefaultAsync(m => m.Id == id);
            if (trainee == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", trainee.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", trainee.CourseId);
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Contact,Email,CourseId,BatchId")] Trainee trainee)
        {
            if (id != trainee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraineeExists(trainee.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "Id", trainee.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", trainee.CourseId);
            return View(trainee);
        }

        // GET: Trainees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainee
                .Include(t => t.Batch)
                .Include(t => t.Course)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainee = await _context.Trainee.SingleOrDefaultAsync(m => m.Id == id);
            _context.Trainee.Remove(trainee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TraineeExists(int id)
        {
            return _context.Trainee.Any(e => e.Id == id);
        }
    }
}
