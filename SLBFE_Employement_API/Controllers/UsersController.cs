using Microsoft.AspNetCore.Mvc;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;
using System.Net;

namespace SLBFE_Employement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(".{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<Users>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetAllCitizensAsync()
        {
            var complaints = await _repository.GetAllUsersAsync().ConfigureAwait(false);

            return Ok(complaints);
        }

        [HttpGet("[action]/{id}.{format}"), FormatFilter]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            return Ok(await _repository.GetUSerByIdAsync(id).ConfigureAwait(false));
        }

        [HttpGet("[action]/{email}/{password}.{format}"), FormatFilter]
        public async Task<IActionResult> VerifyUser([FromRoute] string email, [FromRoute] string password)
        {
            return Ok(await _repository.VerifyUser(email, password).ConfigureAwait(false));
        }

        [HttpPut("[action].{format}"), FormatFilter]
        public async Task<IActionResult> ChangeActivationStatus([FromBody] StatusChangeRequest request)
        {
            return Ok(await _repository.UserActivationStatusChange(request.Id, request.Status).ConfigureAwait(false));
        }

        [HttpPost(".{format}"), FormatFilter]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            return Ok(await _repository.CreateUser(request).ConfigureAwait(false));
        }

        [HttpPost("CompanyAccount.{format}"), FormatFilter]
        public async Task<IActionResult> CompanyAccount([FromBody] CreateCompanyUserRequest request)
        {
            return Ok(await _repository.CreateCompanyUser(request).ConfigureAwait(false));
        }

        [HttpDelete("{id}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            return Ok(await _repository.DeleteUser(id));
        }
    }
}
