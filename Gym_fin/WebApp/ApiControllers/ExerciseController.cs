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
using ExerciseCategory = App.DTO.v1.ExerciseCategory;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Exercise>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<App.DTO.v1.Exercise>> GetExercise()
        {
            
            var data =( await _bll.ExerciseService.AllAsync(User.GetUserId())).ToList();

            var res = data.Select(x => new App.DTO.v1.Exercise()
            {
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                Date = x.Date,
                ExerciseCategory = new ExerciseCategory() 
                    { Id = x.ExerciseCategory!.Id,
                        Name = x.ExerciseCategory.Name,
                        Exercises = x.ExerciseCategory!.Exercises.ToList() },
            });
            return res;
        }

        // GET: api/Exercise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.Exercise>> GetExercise(Guid id)
        {
            var exercise = await _bll.ExerciseService.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return exercise;
        }

        // PUT: api/Exercise/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    }
}
