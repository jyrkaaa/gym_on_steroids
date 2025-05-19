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
using App.DTO.v1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExerInWorkoutController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ExerInWorkoutController> _logger;

        private readonly App.DTO.v1.Mappers.ExerInWorkoutV1Mapper _mapper = new ExerInWorkoutV1Mapper() ;

        public ExerInWorkoutController(IAppBLL bll, ILogger<ExerInWorkoutController> logger)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Gets all WorkoutShares.
        /// </summary>
        /// <returns>List of Workout DTOs.</returns>
        /// <response code="200">Returns the list of Workouts from User</response>
        /// <response code="404">If no entities are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.ExerInWorkout>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ExerInWorkout>>> GetExerInWorkout()
        {
            return (await _bll.ExerInWorkoutService.AllAsync(User.GetUserId())).Select(e => _mapper.Map(e)!).ToList();
        }

        // GET: api/ExerInWorkout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.ExerInWorkout>> GetExerInWorkout(Guid id)
        {
            var exerInWorkout = await _bll.ExerInWorkoutService.FindAsync(id);

            if (exerInWorkout == null)
            {
                return NotFound();
            }

            return _mapper.Map(exerInWorkout!);
        }

        // PUT: api/ExerInWorkout/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerInWorkout(Guid id, App.DTO.v1.ExerInWorkout exerInWorkout)
        {
            throw new NotImplementedException();
        }

        // POST: api/ExerInWorkout
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.ExerInWorkout>> PostExerInWorkout(App.DTO.v1.ExerInWorkoutCreate exerInWorkout)
        {
            var bllEntity = _mapper.Map(exerInWorkout);
            var workoutId = bllEntity!.WorkoutId;
            var checkIfAllowedawait = await _bll.UsersInWorkoutService.FindByWorkoutsAsync(workoutId, null, User.GetUserId(), false);
            if (checkIfAllowedawait)
            {
                _bll.ExerInWorkoutService.Add(bllEntity!, User.GetUserId());
                await _bll.SaveChangesAsync();

                return CreatedAtAction("GetExerInWorkout", new { id = bllEntity!.Id });
            }
            else return StatusCode(403);
        }

        // DELETE: api/ExerInWorkout/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerInWorkout(Guid id)
        {
            await _bll.ExerInWorkoutService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();

        }
        
    }
}
