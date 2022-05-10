using Microsoft.AspNetCore.Mvc;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;
using System.Net;

namespace SLBFE_Employement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacanciesRepository _repository;

        public VacanciesController(IVacanciesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(".{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<JobVacencies>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllVacanciesAsync()
        {
            var vacancies = await _repository.GetAllVacanciesAsync().ConfigureAwait(false);
            return Ok(vacancies);
        }

        [HttpGet("[action].{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<JobVacencies>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetApprovedVacancies()
        {
            var vacancies = await _repository.GetAllVerifiedVacanciesAsync().ConfigureAwait(false);
            return Ok(vacancies);
        }

        [HttpGet("[action].{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<JobVacencies>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetApprovalPendingVacancies()
        {
            var vacancies = await _repository.GetAllPendingVacanciesAsync().ConfigureAwait(false);
            return Ok(vacancies);
        }

        [HttpGet("[action]/{citizensId}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<JobVacencies>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllVacanciesForCitizensId([FromRoute]string citizensId)
        {
            var vacancies = await _repository.GetVacanciesByCitizenIdAsync(citizensId).ConfigureAwait(false);
            return Ok(vacancies);
        }

        [HttpGet("[action]/{vacancyId}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<Citizen>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCandidatesPerVacancy([FromRoute] string vacancyId)
        {
            var citizens = await _repository.GetCitizensByVacancyIdAsync(vacancyId).ConfigureAwait(false);

            return Ok(citizens);
        }

        [HttpGet("[action]/{companyId}.{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<JobVacencies>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVacanciesPerCompany([FromRoute] string companyId)
        {
            var citizens = await _repository.GetVacanciesByCompanyIdAsync(companyId).ConfigureAwait(false);

            return Ok(citizens);
        }

        [HttpPost(".{format}"), FormatFilter]
        public async Task<IActionResult> CreateVacancy([FromBody] CreateVacancyRequest request)
        {
            return Ok(await _repository.CreateVacancies(request).ConfigureAwait(false));
        }

        [HttpPut("[action].{format}"), FormatFilter]
        public async Task<IActionResult> ApproveVacancy([FromBody] ApproveVacancyRequest request)
        {
            return Ok(await _repository.ApproveVacancyAsync(request.VacancyId).ConfigureAwait(false));
        }

        [HttpPut("[action].{format}"), FormatFilter]
        public async Task<IActionResult> UnApproveVacancy([FromBody] ApproveVacancyRequest request)
        {
            return Ok(await _repository.UnApproveVacancyAsync(request.VacancyId).ConfigureAwait(false));
        }

        [HttpPut("[action].{format}"), FormatFilter]
        public async Task<IActionResult> UpdateVacancy([FromBody] UpdateVacancyRequest request)
        {
            return Ok(await _repository.UpdateVacancyAsync(request).ConfigureAwait(false));
        }

        [HttpPut("[action].{format}"), FormatFilter]
        public async Task<IActionResult> ApplyForJobVacancy([FromBody] ApplyForJobVacancy request)
        {
            return Ok(await _repository.ApplyForJob(request.VacancyId, request.CitizenId).ConfigureAwait(false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _repository.DeleteVacancies(id).ConfigureAwait(false));
        }
    }
}
