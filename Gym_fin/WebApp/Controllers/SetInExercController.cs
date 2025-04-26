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
    public class SetInExercController : Controller
    {
        private readonly AppDbContext _context;

        public SetInExercController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SetInExerc
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SetInExerc.Include(s => s.ExerInWorkout);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SetInExerc/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setInExerc = await _context.SetInExerc
                .Include(s => s.ExerInWorkout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setInExerc == null)
            {
                return NotFound();
            }

            return View(setInExerc);
        }

        // GET: SetInExerc/Create
        public IActionResult Create()
        {
            ViewData["ExerInWorkoutId"] = new SelectList(_context.ExerInWorkout, "Id", "Id");
            return View();
        }

        // POST: SetInExerc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Weight,ExerInWorkoutId,Id")] SetInExerc setInExerc)
        {
            if (ModelState.IsValid)
            {
                setInExerc.Id = Guid.NewGuid();
                _context.Add(setInExerc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerInWorkoutId"] = new SelectList(_context.ExerInWorkout, "Id", "Id", setInExerc.ExerInWorkoutId);
            return View(setInExerc);
        }

        // GET: SetInExerc/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setInExerc = await _context.SetInExerc.FindAsync(id);
            if (setInExerc == null)
            {
                return NotFound();
            }
            ViewData["ExerInWorkoutId"] = new SelectList(_context.ExerInWorkout, "Id", "Id", setInExerc.ExerInWorkoutId);
            return View(setInExerc);
        }

        // POST: SetInExerc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Weight,ExerInWorkoutId,Id")] SetInExerc setInExerc)
        {
            if (id != setInExerc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setInExerc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetInExercExists(setInExerc.Id))
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
            ViewData["ExerInWorkoutId"] = new SelectList(_context.ExerInWorkout, "Id", "Id", setInExerc.ExerInWorkoutId);
            return View(setInExerc);
        }

        // GET: SetInExerc/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setInExerc = await _context.SetInExerc
                .Include(s => s.ExerInWorkout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setInExerc == null)
            {
                return NotFound();
            }

            return View(setInExerc);
        }

        // POST: SetInExerc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var setInExerc = await _context.SetInExerc.FindAsync(id);
            if (setInExerc != null)
            {
                _context.SetInExerc.Remove(setInExerc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetInExercExists(Guid id)
        {
            return _context.SetInExerc.Any(e => e.Id == id);
        }
    }
}
