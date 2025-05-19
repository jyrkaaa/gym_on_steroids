using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.DAL.Contracts;
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
    public class SetInExercController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly App.DTO.v1.Mappers.SetInExercV1Mapper _mapper = new App.DTO.v1.Mappers.SetInExercV1Mapper();
        private readonly ILogger<SetInExercController> _logger;

        /// <inheritdoc />
        public SetInExercController(IAppBLL bll, ILogger<SetInExercController> logger)
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
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.SetInExerc>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.SetInExerc>>> GetSetInExerc()
        {
            return (await _bll.SetInExercService.AllAsync(User.GetUserId())).Select(s => _mapper.Map(s!)).ToList();
        }

        /// <summary>
        /// Gets set with the biggest weight by exercise id.
        /// </summary>
        /// <param name="id">The ID of the exercise to search the max weight for</param>
        /// <returns>Set Dto</returns>
        /// <response code="200">Returns the Set</response>
        /// <response code="404">If no entities are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("max/{id}")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.SetInExerc>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.SetInExerc>>> GetBiggestWeight(Guid id)
        {
            var entity =(await _bll.SetInExercService.FindBiggestWeight(id, User.GetUserId()));
            if (entity == null) return NotFound();
            {
                return Ok(_mapper.Map(entity));
            }
        }
        // GET: api/SetInExerc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.SetInExerc>> GetSetInExerc(Guid id)
        {
            var setInExerc = await _bll.SetInExercService.FindAsync(id, User.GetUserId());

            if (setInExerc == null)
            {
                return NotFound();
            }

            return _mapper.Map(setInExerc!);
        }

        // PUT: api/SetInExerc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetInExerc(Guid id, App.DTO.v1.SetInExerc setInExerc)
        {
            throw new NotImplementedException();
        }

        // POST: api/SetInExerc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.SetInExerc>> PostSetInExerc(App.DTO.v1.SetInExercCreate setInExerc)
        {
            var bllEntity = _mapper.Map(setInExerc);
            var eiwId = bllEntity!.ExerInWorkoutId;
            var check = await _bll.UsersInWorkoutService.FindByWorkoutsAsync(null, eiwId, User.GetUserId(), false);
            if (check == false) return BadRequest(400);
            _bll.SetInExercService.Add(bllEntity!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSetInExerc", new { id = bllEntity!.Id });

        }

        // DELETE: api/SetInExerc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetInExerc(Guid id)
        {
            await _bll.SetInExercService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
