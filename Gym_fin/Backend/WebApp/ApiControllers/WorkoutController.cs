using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
using UsersInWorkout = App.BLL.DTO.UsersInWorkout;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<WorkoutController> _logger;
        private readonly App.DTO.v1.Mappers.WorkoutV1Mapper _mapper = new App.DTO.v1.Mappers.WorkoutV1Mapper();

        /// <inheritdoc />
        public WorkoutController(IAppBLL bll, ILogger<WorkoutController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Gets all WorkoutShares.
        /// </summary>
        /// <returns>List of Workout DTOs.</returns>
        /// <response code="200">Returns the list of Workouts from User</response>
        /// <response code="404">If no entities are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Workout>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Workout>>> GetWorkout(string? name, DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            return (await _bll.WorkoutService.AllAsync(User.GetUserId(), name, fromDate, toDate)).Select(w => _mapper.Map(w)!).ToList();
        }

        // GET: api/Workout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Workout>> GetWorkout(Guid id)
        {
            var workout = await _bll.WorkoutService.FindAsync(id, User.GetUserId());
            return _mapper.Map(workout!);
        }

        // PUT: api/Workout/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutWorkout(Guid id, App.DTO.v1.WorkoutEdit workout)
        {
            try
            {
                var publicValue = workout.Public;
                var success = await _bll.WorkoutService.PatchWorkoutAsync(id, User.GetUserId(), publicValue);
                return success ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    
        }

        // POST: api/Workout
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Workout>> PostWorkout(App.DTO.v1.WorkoutCreate workout)
        {
            var bllEntity = _mapper.Map(workout);
            bllEntity!.Users = new List<UsersInWorkout>()
            {
                new UsersInWorkout()
                {
                    Id = Guid.NewGuid(),
                    WorkoutId =  bllEntity.Id,
                    NetUserId = User.GetUserId(),
                }
            };
            _bll.WorkoutService.Add(bllEntity!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = bllEntity!.Id });
        }

        // DELETE: api/Workout/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(Guid id)
        {
            await _bll.WorkoutService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
