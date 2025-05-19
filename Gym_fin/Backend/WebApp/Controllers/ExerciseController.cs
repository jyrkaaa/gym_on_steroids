using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.Domain.EF;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using Exercise = App.DAL.DTO.Exercise;

namespace WebApp.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IAppBLL _bll;

        public ExerciseController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Exercise
        public async Task<IActionResult> Index()
        {
            var exercises = (await _bll.ExerciseService.AllAsync(User.GetUserId())).AsQueryable().AsNoTracking().ToList();
            foreach (var exercise in exercises)
            {
                if (exercise.ExerciseCategoryId != null)
                {   
                    exercise.ExerciseCategory = (await _bll.ExerciseCategoryService.FindAsync(exercise.ExerciseCategoryId!.Value, User.GetUserId()));
                }
            }
            
            var res = new ExerciseIndexViewModel()
            {
                Exercises = exercises,
                
            };
            return View(res);
        }

        // GET: Exercise/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _bll.ExerciseService.FindAsync(id.Value, User.GetUserId());
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercise/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ExerciseCreateViewModel()
            {
                Exercise = new App.BLL.DTO.Exercise(),
                ExerciseCategories = new SelectList(await _bll.ExerciseCategoryService.AllAsync(), "Id", "Name"),
                ExerTargets = new SelectList(""),
                ExerGuides = new SelectList(""),
            };
            return View(vm);
        }

        // POST: Exercise/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Exercise.Id = Guid.NewGuid();
                _bll.ExerciseService.Add(vm.Exercise);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            {
                // Repopulate dropdowns if form validation failed
                vm.ExerciseCategories = new SelectList(await _bll.ExerciseCategoryService.AllAsync(), "Id", "Name");

                return View(vm);
            }
        }

        // GET: Exercise/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _bll.ExerciseService.FindAsync(id.Value);
            if (exercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseCategoryId"] = new SelectList(_bll.ExerciseService.All(), "Id", "Name", exercise.ExerciseCategoryId);
            ViewData["ExerGuideId"] = new SelectList(_bll.ExerciseService.All(), "Id", "Link", exercise.ExerGuideId);
            ViewData["ExerTargetId"] = new SelectList(_bll.ExerciseService.All(), "Id", "MuscleName", exercise.ExerTargetId);
            return View(exercise);
        }

        // POST: Exercise/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Desc,Date,ExerTargetId,ExerGuideId,ExerciseCategoryId,Id")] App.BLL.DTO.Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.ExerciseService.Update(exercise);
                    await _bll.SaveChangesAsync();
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
            ViewData["ExerciseCategoryId"] = new SelectList(_bll.ExerciseService.All(), "Id", "Name", exercise.ExerciseCategoryId);
            ViewData["ExerGuideId"] = new SelectList(_bll.ExerciseService.All(), "Id", "Link", exercise.ExerGuideId);
            ViewData["ExerTargetId"] = new SelectList(_bll.ExerciseService.All(), "Id", "MuscleName", exercise.ExerTargetId);
            return View(exercise);
        }

        // GET: Exercise/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _bll.ExerciseService.FindAsync(id.Value);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exercise = await _bll.ExerciseService.FindAsync(id);
            if (exercise != null)
            {
                _bll.ExerciseService.Remove(exercise, User.GetUserId());
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(Guid id)
        {
            return _bll.ExerciseService.Find(id) != null;
        }
    }
}
