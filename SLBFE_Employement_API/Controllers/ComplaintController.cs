using Microsoft.AspNetCore.Mvc;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;
using System.Net;

namespace SLBFE_Employement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintRepository _repository;

        public ComplaintController(IComplaintRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(".{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<Complaint>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetAllCitizensAsync()
        {
            var complaints = await _repository.GetAllComplaintsAsync().ConfigureAwait(false);

            return Ok(complaints);
        }

        [HttpGet("[action]/{id}.{format}"), FormatFilter]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            return Ok(await _repository.GetComplaintByIdAsync(id).ConfigureAwait(false));
        }

        [HttpGet("[action]/{userId}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<Complaint>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetByUserIdAsync([FromRoute] string userId)
        {
            return Ok(await _repository.GetComplaintsByUserIdAsync(userId).ConfigureAwait(false));
        }

        [HttpPost(".{format}"), FormatFilter]
        public async Task<IActionResult> CreateComplaint([FromBody] CreateComplaintrequest request)
        {
            return Ok(await _repository.CreateComplaint(request).ConfigureAwait(false));
        }

        [HttpPut("[action].{format}"), FormatFilter]
        public async Task<IActionResult> ReplyToComplain([FromBody] ReplyToComplainRequest request)
        {
            return Ok(await _repository.ReplyRoComplaint(request).ConfigureAwait(false));
        }

        [HttpDelete("{id}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            return Ok(await _repository.DeleteComplaint(id));
        }
    }
}
