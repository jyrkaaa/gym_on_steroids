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
    public class UserWeightController : Controller
    {
        private readonly AppDbContext _context;

        public UserWeightController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserWeight
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserWeight.Include(u => u.NetUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserWeight/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWeight = await _context.UserWeight
                .Include(u => u.NetUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWeight == null)
            {
                return NotFound();
            }

            return View(userWeight);
        }

        // GET: UserWeight/Create
        public IActionResult Create()
        {
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserWeight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeightKg,Desc,Date,NetUserId,Id")] UserWeight userWeight)
        {
            if (ModelState.IsValid)
            {
                userWeight.Id = Guid.NewGuid();
                _context.Add(userWeight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id", userWeight.NetUserId);
            return View(userWeight);
        }

        // GET: UserWeight/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWeight = await _context.UserWeight.FindAsync(id);
            if (userWeight == null)
            {
                return NotFound();
            }
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id", userWeight.NetUserId);
            return View(userWeight);
        }

        // POST: UserWeight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WeightKg,Desc,Date,NetUserId,Id")] UserWeight userWeight)
        {
            if (id != userWeight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userWeight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWeightExists(userWeight.Id))
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
            ViewData["NetUserId"] = new SelectList(_context.Users, "Id", "Id", userWeight.NetUserId);
            return View(userWeight);
        }

        // GET: UserWeight/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWeight = await _context.UserWeight
                .Include(u => u.NetUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWeight == null)
            {
                return NotFound();
            }

            return View(userWeight);
        }

        // POST: UserWeight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userWeight = await _context.UserWeight.FindAsync(id);
            if (userWeight != null)
            {
                _context.UserWeight.Remove(userWeight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWeightExists(Guid id)
        {
            return _context.UserWeight.Any(e => e.Id == id);
        }
    }
}
