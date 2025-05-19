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
    public class UsersInWorkoutController : Controller
    {
        private readonly AppDbContext _context;

        public UsersInWorkoutController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsersInWorkout
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UsersInWorkout.Include(u => u.NetUser).Include(u => u.Workout);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UsersInWorkout/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersInWorkout = await _context.UsersInWorkout
                .Include(u => u.NetUser)
                .Include(u => u.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersInWorkout == null)
            {
                return NotFound();
            }

            return View(usersInWorkout);
        }

        // GET: UsersInWorkout/Create
        public IActionResult Create()
        {
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "CreatedBy");
            return View();
        }

        // POST: UsersInWorkout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NetUserId,WorkoutId")] UsersInWorkout usersInWorkout)
        {
            if (ModelState.IsValid)
            {
                usersInWorkout.Id = Guid.NewGuid();
                _context.Add(usersInWorkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id", usersInWorkout.NetUserId);
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "CreatedBy", usersInWorkout.WorkoutId);
            return View(usersInWorkout);
        }

        // GET: UsersInWorkout/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersInWorkout = await _context.UsersInWorkout.FindAsync(id);
            if (usersInWorkout == null)
            {
                return NotFound();
            }
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id", usersInWorkout.NetUserId);
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "CreatedBy", usersInWorkout.WorkoutId);
            return View(usersInWorkout);
        }

        // POST: UsersInWorkout/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NetUserId,WorkoutId")] UsersInWorkout usersInWorkout)
        {
            if (id != usersInWorkout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersInWorkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersInWorkoutExists(usersInWorkout.Id))
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
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id", usersInWorkout.NetUserId);
            ViewData["WorkoutId"] = new SelectList(_context.Workout, "Id", "CreatedBy", usersInWorkout.WorkoutId);
            return View(usersInWorkout);
        }

        // GET: UsersInWorkout/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersInWorkout = await _context.UsersInWorkout
                .Include(u => u.NetUser)
                .Include(u => u.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersInWorkout == null)
            {
                return NotFound();
            }

            return View(usersInWorkout);
        }

        // POST: UsersInWorkout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usersInWorkout = await _context.UsersInWorkout.FindAsync(id);
            if (usersInWorkout != null)
            {
                _context.UsersInWorkout.Remove(usersInWorkout);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersInWorkoutExists(Guid id)
        {
            return _context.UsersInWorkout.Any(e => e.Id == id);
        }
    }
}
