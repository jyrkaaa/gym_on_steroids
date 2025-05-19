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
    public class ExerTargerController : Controller
    {
        private readonly AppDbContext _context;

        public ExerTargerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExerTarger
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerTarget.ToListAsync());
        }

        // GET: ExerTarger/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerTarget = await _context.ExerTarget
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerTarget == null)
            {
                return NotFound();
            }

            return View(exerTarget);
        }

        // GET: ExerTarger/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerTarger/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MuscleName,Id")] ExerTarget exerTarget)
        {
            if (ModelState.IsValid)
            {
                exerTarget.Id = Guid.NewGuid();
                _context.Add(exerTarget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerTarget);
        }

        // GET: ExerTarger/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerTarget = await _context.ExerTarget.FindAsync(id);
            if (exerTarget == null)
            {
                return NotFound();
            }
            return View(exerTarget);
        }

        // POST: ExerTarger/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MuscleName,Id")] ExerTarget exerTarget)
        {
            if (id != exerTarget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerTarget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerTargetExists(exerTarget.Id))
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
            return View(exerTarget);
        }

        // GET: ExerTarger/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerTarget = await _context.ExerTarget
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerTarget == null)
            {
                return NotFound();
            }

            return View(exerTarget);
        }

        // POST: ExerTarger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exerTarget = await _context.ExerTarget.FindAsync(id);
            if (exerTarget != null)
            {
                _context.ExerTarget.Remove(exerTarget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerTargetExists(Guid id)
        {
            return _context.ExerTarget.Any(e => e.Id == id);
        }
    }
}
