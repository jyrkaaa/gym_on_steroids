using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain.EF;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExerciseController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ExerciseController> _logger;
        private readonly App.DTO.v1.Mappers.ExerciseV1Mapper _mapper = new App.DTO.v1.Mappers.ExerciseV1Mapper();
        
        /// <inheritdoc />
        public ExerciseController(IAppBLL bll, ILogger<ExerciseController> logger)
        {
            _bll = bll;
            _logger = logger;
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
        public async Task<IEnumerable<App.DTO.v1.Exercise>> GetExercise(Guid? categoryId)
        {
            var data = (await _bll.ExerciseService.AllAsync(null, categoryId)).ToList();


            if (data.Count <= 0) return Array.Empty<App.DTO.v1.Exercise>();
            var res = data.Select(x => _mapper.Map(x)!).OrderBy(x => x.Name).ToList();
            
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
            return _mapper.Map(exercise)!;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutExercise(Guid id, App.DTO.v1.Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            await _bll.ExerciseService.UpdateAsync(_mapper.Map(exercise)!, User.GetUserId());

            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Exercise
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Exercise>> PostExercise(App.DTO.v1.ExerciseCreate exercise)
        {
            var bllEntity = _mapper.Map(exercise);
            _bll.ExerciseService.Add(bllEntity!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new
            {
                // todo - get person id
                id = bllEntity!.Id,
            });
        }


        // DELETE: api/Exercise/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            await _bll.ExerciseService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();

        }

        private bool ExerciseExists(Guid id)
        {
            return _bll.ExerciseService.Exists(id);
        }

    }
}
