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
using App.DTO.v1;
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
    public class UserWeightController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<App.BLL.DTO.UserWeight> _logger;
        private readonly App.DTO.v1.Mappers.UserWeightV1Mapper _mapper = new App.DTO.v1.Mappers.UserWeightV1Mapper();

        /// <inheritdoc />

        public UserWeightController(IAppBLL bll, ILogger<App.BLL.DTO.UserWeight> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Weights for User.
        /// </summary>
        /// <returns>List of UserWeight DTOs.</returns>
        /// <response code="200">Returns the list of Weights from User</response>
        /// <response code="404">If no entities are found</response>
        /// <response code="401">Unauthorized Access</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.UserWeight>), 200)]
        [Produces("application/json")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.UserWeight>>> GetUserWeight()
        {
            return (await _bll.UserWeightService.AllAsync(User.GetUserId())).Select(w => _mapper.Map(w!)).ToList();
        }

        // GET: api/UserWeight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.UserWeight>> GetUserWeight(Guid id)
        {
            var entity = await _bll.UserWeightService.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return _mapper.Map(entity!);
        }

        // PUT: api/UserWeight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutUserWeight(Guid id, UserWeight userWeight)
        {
            if (id != userWeight.Id)
            {
                return BadRequest();
            }

            _context.Entry(userWeight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserWeightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/UserWeight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.UserWeight>> PostUserWeight(UserWeightCreate userWeight)
        {
            var bllEntity = _mapper.Map(userWeight, User.GetUserId());
            if (bllEntity == null) return BadRequest();
            _bll.UserWeightService.Add(bllEntity!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserWeight", new { id = bllEntity.Id });
        }

        // DELETE: api/UserWeight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserWeight(Guid id)
        {
            await _bll.UserWeightService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
