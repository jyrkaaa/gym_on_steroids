using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.Domain.EF;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using ExerciseCategory = App.DTO.v1.ExerciseCategory;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExerciseController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ExerciseController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Gets all exercises for the logged-in user.
        /// </summary>
        /// <returns>List of Exercise DTOs.</returns>
        /// <response code="200">Returns the list of exercises</response>
        /// <response code="404">If no exercises are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Exercise>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<App.DTO.v1.Exercise>> GetExercise()
        {
            
            var data =( await _bll.ExerciseService.AllAsync(User.GetUserId())).ToList();
            if (data.Count <= 0) return Array.Empty<App.DTO.v1.Exercise>();
            var res = data.Select(x => new App.DTO.v1.Exercise()
            {
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                Date = x.Date,
                ExerciseCategoryId = x.ExerciseCategoryId,
                ExerTargetId = x.ExerTargetId,
                ExerGuideId = x.ExerGuideId,
            }).ToList();
            foreach (App.DTO.v1.Exercise item in res)
            {
                var catId = item.ExerciseCategoryId;
                var targetId = item.ExerTargetId;
                var guideId = item.ExerGuideId;
                if (catId == null) continue;
                var cat = (await _bll.ExerciseCategoryService.FindAsync(catId.Value, User.GetUserId()));
                if (cat == null) continue;

                item.ExerciseCategory = new App.DTO.v1.ExerciseCategory()
                {
                    Id = cat.Id,
                    Name = cat.Name,
                };
            }

            return res;
        }

        /// <summary>
        /// Gets exercise for the logged-in user with the provided id (GUID).
        /// </summary>
        /// <returns>Exercise DTO.</returns>
        /// <response code="200">An exercises</response>
        /// <response code="404">If no exercises are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an Exercise by ID", Description = "Provide a Guid to look up a specific Exercise.")]
        [ProducesResponseType(typeof(App.DTO.v1.Exercise), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<App.DTO.v1.Exercise>> GetExercise(
        [FromRoute]
        [SwaggerParameter("Exercise ID (GUID format", Required = true)]
        Guid id)
        {
            var exercise = await _bll.ExerciseService.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseV1 = new App.DTO.v1.Exercise()
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Desc = exercise.Desc,
                Date = exercise.Date,
                ExerciseCategoryId = exercise.ExerciseCategoryId,
                ExerTargetId = exercise.ExerTargetId,
                ExerGuideId = exercise.ExerGuideId,
            };
            if (exercise.ExerciseCategoryId.HasValue)
            {
                var cat = await _bll.ExerciseCategoryService.FindAsync(exercise.ExerciseCategoryId.Value, User.GetUserId());
                if (cat != null)
                {
                    exercise.ExerciseCategory = cat;
                }
            };
            return exerciseV1;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Edits exercise for the logged-in user with the provided id (GUID).
        /// </summary>
        /// <returns>Exercise DTO.</returns>
        /// <response code="200">An exercises</response>
        /// <response code="404">If no exercises are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(Guid id, App.BLL.DTO.Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            _bll.ExerciseService.Update(exercise);

            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Exercise
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(App.BLL.DTO.Exercise exercise)
        {
            _bll.ExerciseService.Add(exercise, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new
            {
                // todo - get person id
                id = exercise.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, exercise);
        }


        // DELETE: api/Exercise/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var exercise = await _bll.ExerciseService.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _bll.ExerciseService.Remove(exercise);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(Guid id)
        {
            return _bll.ExerciseService.Exists(id);
        }
        /// <summary>
        /// Gets all exercises by category for the logged-in user.
        /// </summary>
        /// <param name="categoryId">The ID of the exercise category.</param>
        /// <returns>List of exercises that belong to the given category.</returns>
        /// <response code="200">Returns the list of exercises</response>
        /// <response code="404">If no exercises are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("by-category/{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Exercise>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<App.DTO.v1.Exercise>> GetExercisesByCategory(Guid categoryId)
        {
            var userId = User.GetUserId(); // Get currently logged-in user's ID

            var exercises = await _bll.ExerciseService.GetAllByCategoryIdAsync(categoryId, userId);

            if (!exercises.Any()) 
                return Array.Empty<App.DTO.v1.Exercise>();

            return exercises.Select(x => new App.DTO.v1.Exercise()
            {
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                Date = x.Date,
                ExerciseCategoryId = x.ExerciseCategoryId,
                ExerTargetId = x.ExerTargetId,
                ExerGuideId = x.ExerGuideId
            });
        }

    }
}
