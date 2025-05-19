using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.DAL.Contracts;
using App.Domain.EF;
using Base.Helpers;
using ExerciseCategory = App.BLL.DTO.ExerciseCategory;

namespace WebApp.Controllers
{
    public class ExerciseCategoryController : Controller
    {
        private readonly IAppBLL _bll;

        public ExerciseCategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ExerciseCategory
        public async Task<IActionResult> Index()
        {
            var res = await _bll.ExerciseCategoryService.AllAsync(User.GetUserId());
            return View(res);
        }

        // GET: ExerciseCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseCategory = await _bll.ExerciseCategoryService
                .FindAsync(id.Value);
            if (exerciseCategory == null)
            {
                return NotFound();
            }

            return View(exerciseCategory);
        }

        // GET: ExerciseCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerciseCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( App.BLL.DTO.ExerciseCategory exerciseCategory)
        {
            if (ModelState.IsValid)
            {
                exerciseCategory.Id = Guid.NewGuid();
                _bll.ExerciseCategoryService.Add(exerciseCategory, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerciseCategory);
        }

        // GET: ExerciseCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseCategory = await _bll.ExerciseCategoryService.FindAsync(id.Value);
            if (exerciseCategory == null)
            {
                return NotFound();
            }
            return View(exerciseCategory);
        }

        // POST: ExerciseCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ExerciseCategory exerciseCategory)
        {
            if (id != exerciseCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.ExerciseCategoryService.Update(exerciseCategory);
                    await _bll.SaveChangesAsync();
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

        // GET: ExerciseCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseCategory = await _bll.ExerciseCategoryService
                .FindAsync(id.Value, User.GetUserId());
            if (exerciseCategory == null)
            { 
                return NotFound();
            }

            return View(exerciseCategory);
        }

        // POST: ExerciseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exerciseCategory = await _bll.ExerciseCategoryService.FindAsync(id);
            if (exerciseCategory != null)
            {
                _bll.ExerciseCategoryService.Remove(exerciseCategory);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseCategoryExists(Guid id)
        {
            return _bll.ExerciseCategoryService.Exists(id, User.GetUserId());
        }
    }
}
