using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.Domain.EF;
using Microsoft.AspNetCore.Authorization;
using WebApp.Areas.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ExerciseController : Controller
    {
        private readonly AppDbContext _context;

        public ExerciseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Exercise
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Exercise.Include(e => e.ExerciseCategory).Include(e => e.ExerGuide).Include(e => e.ExerTarget);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Exercise/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .Include(e => e.ExerciseCategory)
                .Include(e => e.ExerGuide)
                .Include(e => e.ExerTarget)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Admin/Exercise/Create
        public IActionResult Create()
        {
            var vm = new ExerciseCreateViewModel()
            {
                Exercise = new Exercise(),
                ExerciseCategories = new SelectList(_context.ExerciseCategory, "Id", "Name"),
                ExerTargets = new SelectList(""),
                ExerGuides = new SelectList(""),
            };
            return View(vm);
        }

        // POST: Admin/Exercise/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Exercise.Id = Guid.NewGuid();
                vm.Exercise.Date = vm.Exercise.Date.ToUniversalTime();
                vm.Exercise.CreatedAt = vm.Exercise.CreatedAt.ToUniversalTime();
                _context.Add(vm.Exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Repopulate dropdowns if form validation failed
            vm.ExerciseCategories = new SelectList(_context.ExerciseCategory, "Id", "Name");
            vm.ExerTargets = new SelectList(_context.ExerTarget, "Id", "MuscleName");
            vm.ExerGuides = new SelectList(_context.ExerGuide, "Id", "Link");
            return View(vm);
        }
        

        // GET: Admin/Exercise/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseCategoryId"] = new SelectList(_context.ExerciseCategory, "Id", "Name", exercise.ExerciseCategoryId);
            ViewData["ExerGuideId"] = new SelectList(_context.ExerGuide, "Id", "Link", exercise.ExerGuideId);
            ViewData["ExerTargetId"] = new SelectList(_context.ExerTarget, "Id", "MuscleName", exercise.ExerTargetId);
            return View(exercise);
        }

        // POST: Admin/Exercise/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Desc,Date,ExerTargetId,ExerGuideId,ExerciseCategoryId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exercise.Date = DateTime.UtcNow;
                    _context.Update(exercise);
                    await _context.SaveChangesAsyncWithhoutHardcode();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            ViewData["ExerciseCategoryId"] = new SelectList(_context.ExerciseCategory, "Id", "CreatedBy", exercise.ExerciseCategoryId);
            ViewData["ExerGuideId"] = new SelectList(_context.ExerGuide, "Id", "CreatedBy", exercise.ExerGuideId);
            ViewData["ExerTargetId"] = new SelectList(_context.ExerTarget, "Id", "CreatedBy", exercise.ExerTargetId);
            return View(exercise);
        }

        // GET: Admin/Exercise/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .Include(e => e.ExerciseCategory)
                .Include(e => e.ExerGuide)
                .Include(e => e.ExerTarget)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Admin/Exercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercise.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(Guid id)
        {
            return _context.Exercise.Any(e => e.Id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditToAdmin(Guid id)
        {
            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exercise.CreatedBy = "admin";
                    _context.Update(exercise);
                    await _context.SaveChangesAsyncWithhoutHardcode();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            return BadRequest();
        }
    }
}
