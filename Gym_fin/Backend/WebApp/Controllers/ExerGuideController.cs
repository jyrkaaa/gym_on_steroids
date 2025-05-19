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
    public class ExerGuideController : Controller
    {
        private readonly AppDbContext _context;

        public ExerGuideController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExerGuide
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerGuide.ToListAsync());
        }

        // GET: ExerGuide/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerGuide = await _context.ExerGuide
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerGuide == null)
            {
                return NotFound();
            }

            return View(exerGuide);
        }

        // GET: ExerGuide/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerGuide/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Link,Id")] ExerGuide exerGuide)
        {
            if (ModelState.IsValid)
            {
                exerGuide.Id = Guid.NewGuid();
                _context.Add(exerGuide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerGuide);
        }

        // GET: ExerGuide/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerGuide = await _context.ExerGuide.FindAsync(id);
            if (exerGuide == null)
            {
                return NotFound();
            }
            return View(exerGuide);
        }

        // POST: ExerGuide/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Link,Id")] ExerGuide exerGuide)
        {
            if (id != exerGuide.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerGuide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerGuideExists(exerGuide.Id))
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
            return View(exerGuide);
        }

        // GET: ExerGuide/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerGuide = await _context.ExerGuide
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerGuide == null)
            {
                return NotFound();
            }

            return View(exerGuide);
        }

        // POST: ExerGuide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exerGuide = await _context.ExerGuide.FindAsync(id);
            if (exerGuide != null)
            {
                _context.ExerGuide.Remove(exerGuide);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerGuideExists(Guid id)
        {
            return _context.ExerGuide.Any(e => e.Id == id);
        }
    }
}
