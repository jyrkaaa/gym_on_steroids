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

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersInWorkoutController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<UsersInWorkoutController> _logger;
        private readonly App.DTO.v1.Mappers.UsersInWorkoutV1Mapper _mapper = new App.DTO.v1.Mappers.UsersInWorkoutV1Mapper();

        /// <inheritdoc />
        public UsersInWorkoutController(IAppBLL bll, ILogger<UsersInWorkoutController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Gets all WorkoutShares.
        /// </summary>
        /// <returns>List of UsersInWorkouts DTOs.</returns>
        /// <response code="200">Returns the list of entities</response>
        /// <response code="404">If no entities are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.UsersInWorkout>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<App.DTO.v1.UsersInWorkout>> GetUsersInWorkout()
        {
            var data = (await _bll.UsersInWorkoutService.AllAsync(User.GetUserId())).ToList();
            if (data.Count <= 0) return Array.Empty<App.DTO.v1.UsersInWorkout>();
            var res = data.Select(x => _mapper.Map(x)!).OrderBy(x => x.Id).ToList();
            
            return res;
        }

        // GET: api/UsersInWorkout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.UsersInWorkout>> GetUsersInWorkout(Guid id)
        {
            var usersInWorkout = await _bll.UsersInWorkoutService.FindAsync(id);

            if (usersInWorkout == null)
            {
                return NotFound();
            }

            return _mapper.Map(usersInWorkout)!;
        }

        // PUT: api/UsersInWorkout/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersInWorkout(Guid id, App.DTO.v1.UsersInWorkout usersInWorkout)
        {
            if (id != usersInWorkout.Id)
            {
                return BadRequest();
            }

            await _bll.UsersInWorkoutService.UpdateAsync(_mapper.Map(usersInWorkout)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersInWorkoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersInWorkout
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.UsersInWorkout>> PostUsersInWorkout(App.DTO.v1.UsersInWorkoutCreate usersInWorkout)
        {
            var workoutId = usersInWorkout.WorkoutId;
            var bllEntity = new App.BLL.DTO.UsersInWorkout() {Id = Guid.NewGuid(), NetUserId = User.GetUserId(), WorkoutId = workoutId};
            var check = await _bll.UsersInWorkoutService.FindByWorkoutsAsync(workoutId, null, User.GetUserId(), true);
            if (!check) return BadRequest(404);
            _bll.UsersInWorkoutService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetUsersInWorkout", new { id = bllEntity.Id });
        }

        /// <summary>
        /// Delete UsersInWorkout by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersInWorkout(Guid id)
        {
            await _bll.UsersInWorkoutService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private bool UsersInWorkoutExists(Guid id)
        {
            return _bll.UsersInWorkoutService.Exists(id);
        }
    }

}
