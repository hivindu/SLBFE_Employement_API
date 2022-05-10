using Microsoft.AspNetCore.Mvc;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;
using System.Net;

namespace SLBFE_Employement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenRepositories _repository;

        public CitizensController(ICitizenRepositories repository)
        {
            _repository = repository;
        }

        [HttpGet(".{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<Citizen>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Citizen>>> GetAllCitizensAsync()
        {
            var citizens = await _repository.GetAllCitizensAsync().ConfigureAwait(false);

            return Ok(citizens);
        }

        [HttpGet("{nic}.{format}"), FormatFilter]
        public async Task<IActionResult> GetAsync([FromRoute]string nic)
        {
            return Ok(await _repository.GetCitizenByNicAsync(nic).ConfigureAwait(false));
        }

        [HttpGet("[action]/{quolification}.{format}"), FormatFilter]
        public async Task<IActionResult> FindAsync([FromRoute] string quolification)
        {
            return Ok(await _repository.GetCitizenByQuolificationAsync(quolification).ConfigureAwait(false));
        }

        [HttpPost(".{format}"), FormatFilter]
        public async Task<IActionResult> CreateCitizen([FromBody] AddCitizensRequest createCitizenRequest)
        {
            return Ok(await _repository.CreateCitizenAsync(createCitizenRequest).ConfigureAwait(false));
        }

        [HttpPost("Connection/{connectionId}/{ciitizenId}.{format}"), FormatFilter]
        public async Task<IActionResult> AddNewConnection([FromRoute] string connectionId, [FromRoute] string ciitizenId)
        {
            return Ok(await _repository.AddConnection(connectionId, ciitizenId).ConfigureAwait(false));
        }

        [HttpPut("Connection/{connectionId}/{ciitizenId}.{format}"), FormatFilter]
        public async Task<IActionResult> RemoveConnection([FromRoute] string connectionId, [FromRoute] string ciitizenId)
        {
            return Ok(await _repository.RemoveConnection(connectionId, ciitizenId).ConfigureAwait(false));
        }

        [HttpGet("[action]/{id}.{format}"), FormatFilter]
        public async Task<IActionResult> GetCitizenById([FromRoute] string id)
        {
            return Ok(await _repository.GetCitizenByIdAsync(id).ConfigureAwait(false));
        }

        [HttpGet("[action]/{nic}/{password}.{format}"), FormatFilter]
        public async Task<IActionResult> LoginUser([FromRoute] string nic,[FromRoute] string password)
        {
            var loginRequest = new LoginRequest() {
                Passsword = password,
                NIC = nic,
            };
            return Ok(await _repository.VerifyUserCredentialsAsync(loginRequest).ConfigureAwait(false));
        }

        [HttpPut("{nic}.{format}"), FormatFilter]
        public async Task<IActionResult> UpdateCitizen([FromRoute]string nic, [FromForm]UpdateCitizenRequest dataSet)
        {
            var qualification = new Qualification() {
                CertificateImage = dataSet.Image,
                QualificationName = dataSet.QuolificationName,
            };
            var citizen = await _repository.UpdateCitizen(nic, qualification).ConfigureAwait(false);

            return Ok(citizen);
        }

        [HttpPut("[action]/{nic}.{format}"), FormatFilter]
        public async Task<IActionResult> VerifyInformationAsync([FromRoute] string nic, [FromBody] VerifyInformationRequest verify)
        {
           return Ok(await _repository.VerifyCitizensAsync(nic, verify.IsValid).ConfigureAwait(false));
        }

        [HttpPut("ChangeActivationStatus/{citizenId}.{format}"), FormatFilter]
        public async Task<IActionResult> ChangeActivationStatus([FromRoute]string citizenId, [FromBody] CitizenStatusChangeRequest request)
        {
            return Ok(await _repository.CitizenActivationStatusChange(citizenId, request.IsActive).ConfigureAwait(false));
        }

        [HttpGet("GetDeatailCitizen/{id}.{format}"), FormatFilter]
        public async Task<IActionResult> GetCitizenFullDetailsByIdAsync([FromRoute] string id)
        {
            return Ok(await _repository.GetCitizenFullDetailsByIdAsync(id).ConfigureAwait(false));
        }

        [HttpGet("[action]/{nic}/.{format}"), FormatFilter]
        public async Task<IActionResult> GetConnectionsPerCitizen([FromRoute] string nic)
        {
            return Ok(await _repository.GetContactsByNicAsync(nic).ConfigureAwait(false));
        }

        [HttpDelete("{id}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete([FromRoute]string id)
        {
            return Ok(await _repository.DeleteCitizenAsync(id));
        }
    }
}
