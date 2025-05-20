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

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ExerciseCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public ExerciseCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ExerciseCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerciseCategory.ToListAsync());
        }

        // GET: Admin/ExerciseCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseCategory = await _context.ExerciseCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseCategory == null)
            {
                return NotFound();
            }

            return View(exerciseCategory);
        }

        // GET: Admin/ExerciseCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ExerciseCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] ExerciseCategory exerciseCategory)
        {
            if (ModelState.IsValid)
            {
                exerciseCategory.Id = Guid.NewGuid();
                _context.Add(exerciseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerciseCategory);
        }

        // GET: Admin/ExerciseCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseCategory = await _context.ExerciseCategory.FindAsync(id);
            if (exerciseCategory == null)
            {
                return NotFound();
            }
            return View(exerciseCategory);
        }

        // POST: Admin/ExerciseCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] ExerciseCategory exerciseCategory)
        {
            if (id != exerciseCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseCategoryExists(exerciseCategory.Id))
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
            return View(exerciseCategory);
        }

        // GET: Admin/ExerciseCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseCategory = await _context.ExerciseCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseCategory == null)
            {
                return NotFound();
            }

            return View(exerciseCategory);
        }

        // POST: Admin/ExerciseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exerciseCategory = await _context.ExerciseCategory.FindAsync(id);
            if (exerciseCategory != null)
            {
                _context.ExerciseCategory.Remove(exerciseCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseCategoryExists(Guid id)
        {
            return _context.ExerciseCategory.Any(e => e.Id == id);
        }
    }
}
