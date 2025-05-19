using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.Domain.EF;

namespace WebApp.Controllers
{
    public class ExerInWorkoutController : Controller
    {
        private readonly AppDbContext _context;

        public ExerInWorkoutController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExerInWorkout
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExerInWorkout.Include(e => e.Exercise).Include(e => e.Workout);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ExerInWorkout/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerInWorkout = await _context.ExerInWorkout
                .Include(e => e.Exercise)
                .Include(e => e.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerInWorkout == null)
            {
                return NotFound();
            }

            return View(exerInWorkout);
        }

        // GET: ExerInWorkout/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Name");
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "Name");
            return View();
        }

        // POST: ExerInWorkout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Desc,WorkoutId,ExerciseId,Id")] ExerInWorkout exerInWorkout)
        {
            if (ModelState.IsValid)
            {
                exerInWorkout.Id = Guid.NewGuid();
                _context.Add(exerInWorkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Name", exerInWorkout.ExerciseId);
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "Name", exerInWorkout.WorkoutId);
            return View(exerInWorkout);
        }

        // GET: ExerInWorkout/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerInWorkout = await _context.ExerInWorkout.FindAsync(id);
            if (exerInWorkout == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Name", exerInWorkout.ExerciseId);
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "Name", exerInWorkout.WorkoutId);
            return View(exerInWorkout);
        }

        // POST: ExerInWorkout/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Desc,WorkoutId,ExerciseId,Id")] ExerInWorkout exerInWorkout)
        {
            if (id != exerInWorkout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerInWorkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerInWorkoutExists(exerInWorkout.Id))
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
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Name", exerInWorkout.ExerciseId);
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "Name", exerInWorkout.WorkoutId);
            return View(exerInWorkout);
        }

        // GET: ExerInWorkout/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerInWorkout = await _context.ExerInWorkout
                .Include(e => e.Exercise)
                .Include(e => e.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerInWorkout == null)
            {
                return NotFound();
            }

            return View(exerInWorkout);
        }

        // POST: ExerInWorkout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exerInWorkout = await _context.ExerInWorkout.FindAsync(id);
            if (exerInWorkout != null)
            {
                _context.ExerInWorkout.Remove(exerInWorkout);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerInWorkoutExists(Guid id)
        {
            return _context.ExerInWorkout.Any(e => e.Id == id);
        }
    }
}
