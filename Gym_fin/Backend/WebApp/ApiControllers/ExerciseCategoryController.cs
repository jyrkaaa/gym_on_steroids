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
using Microsoft.VisualBasic;
using ExerciseCategory = App.DTO.v1.ExerciseCategory;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExerciseCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ExerciseCategoryController> _logger;
        private readonly App.DTO.v1.Mappers.ExerciseCategoryV1Mapper _mapper = new ExerciseCategoryV1Mapper();

        public ExerciseCategoryController(IAppBLL bll, ILogger<ExerciseCategoryController> logger)
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
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ExerciseCategory>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<ExerciseCategory>> GetCategorys()
        {
            
            var data =( await _bll.ExerciseCategoryService.AllAsync()).ToList();
            
            return data.Select(c => _mapper.Map(c!)).ToList();
        }

        // GET: api/ExerciseCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseCategory>> GetCategory(Guid id)
        {
            var exercise = await _bll.ExerciseCategoryService.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }
            return _mapper.Map(exercise!);
            
        }

        // PUT: api/ExerciseCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, ExerciseCategory category)
        {
            if (id != category.Id) 
            {
                return BadRequest();
            }
            await _bll.ExerciseCategoryService.UpdateAsync(_mapper.Map(category!));
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        

        // POST: api/Exercise
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseCategory>> PostCategory(App.DTO.v1.ExerciseCategoryCreate exerciseCategory)
        {
            var bllEntity = _mapper.Map(exerciseCategory);
            if (bllEntity == null) return BadRequest();
            _bll.ExerciseCategoryService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new
            {
                // todo - get person id
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            });
        }


        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
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
