using MongoDB.Driver;
using SLBFE_Employement_API.Data.Interfaces;
using SLBFE_Employement_API.Extensions;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;

namespace SLBFE_Employement_API.Repository
{
    public class VacanciesRepository : IVacanciesRepository
    {
        private readonly ISLBFContext _context;
        private readonly ILogger<VacanciesRepository> _logger;

        public VacanciesRepository(ISLBFContext context, ILogger<VacanciesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<JobVacencies>> GetAllVacanciesAsync()
        {
            return await _context.vacancies.Find(p => true).ToListAsync();
        }

        public async Task<List<JobVacencies>> GetAllPendingVacanciesAsync()
        {
            return await _context.vacancies.Find(p => p.IsApproved == false).ToListAsync();
        }

        public async Task<List<JobVacencies>> GetAllVerifiedVacanciesAsync()
        {
            return await _context.vacancies.Find(p => p.IsApproved == true).ToListAsync();
        }

        public async Task<List<Citizen>> GetCitizensByVacancyIdAsync(string id)
        {
            try {
                var vacancy = await _context.vacancies.Find(p => p.Id == id).FirstOrDefaultAsync();
                var citizens = new List<Citizen>();
                if (vacancy != null)
                {
                    var candidatesList = vacancy.Candidates;
                    if (candidatesList != null)
                    {
                        foreach (var candidateCitizenId in candidatesList)
                        {
                            var citizen = await _context.citizen.Find(p => p.Id == candidateCitizenId).FirstOrDefaultAsync();

                            if (citizen != null)
                            {
                                citizens.Add(citizen);
                            }
                        }
                    }
                }

                return citizens;
            }catch (Exception ex) {
                _logger.LogError(ex.InnerException.ToString());

                return default;
            }
            
        }

        public async Task<List<JobVacencies>> GetVacanciesByCompanyIdAsync(string userId)
        {
            try
            {
                var vacancies = await _context.vacancies.Find(p => p.UserId == userId).ToListAsync();

                return vacancies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());

                return default;
            }
        }

        public async Task<List<JobVacencies>> GetVacanciesByCitizenIdAsync(string citizenId)
        {
            try
            {
                var vacancies = await _context.vacancies.Find(p => true).ToListAsync();

                var responseList = new List<JobVacencies>();

                if (vacancies != null)
                {
                    foreach (var vacancy in vacancies)
                    {
                        if(vacancy.Candidates != null && vacancy.Candidates.Contains(citizenId))
                        {
                            responseList.Add(vacancy);
                        }
                    }
                }

                return responseList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());

                return default;
            }
        }

        public async Task<bool> ApplyForJob(string vacancyId, string citizenId)
        {
            var vacancy = await _context.vacancies.Find(p => p.Id == vacancyId).FirstOrDefaultAsync();
            if (vacancy != null)
            {
                var candidates = vacancy.Candidates;

                if (candidates == null)
                {
                    candidates = new List<string> {
                        citizenId,
                    };
                }else if (candidates != null && !candidates.Contains(citizenId))
                {
                    candidates.Add(citizenId);
                }

                vacancy.Candidates = candidates;

                var result = await _context.vacancies.ReplaceOneAsync(filter: s => s.Id == vacancy.Id, replacement: vacancy).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<bool> ApproveVacancyAsync(string vacanyId)
        {
            var vacancy = await _context.vacancies.Find(p => p.Id == vacanyId).FirstOrDefaultAsync();
            if (vacancy != null)
            {
                vacancy.IsApproved = true;

                var result = await _context.vacancies.ReplaceOneAsync(filter: s => s.Id == vacancy.Id, replacement: vacancy).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<bool> UnApproveVacancyAsync(string vacanyId)
        {
            var vacancy = await _context.vacancies.Find(p => p.Id == vacanyId).FirstOrDefaultAsync();
            if (vacancy != null)
            {
                vacancy.IsApproved = false;

                var result = await _context.vacancies.ReplaceOneAsync(filter: s => s.Id == vacancy.Id, replacement: vacancy).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<JobVacencies> CreateVacancies(CreateVacancyRequest request)
        {
            var dataSet = request.ToModel();
            await _context.vacancies.InsertOneAsync(dataSet).ConfigureAwait(false);

            return await _context.vacancies.Find(p => p.Id == dataSet.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateVacancyAsync(UpdateVacancyRequest request)
        {
            var dataSet = request.ToModel();

            var vacancy = await _context.vacancies.Find(p => p.Id == request.Id).FirstOrDefaultAsync();
            if (vacancy != null)
            {
                dataSet.Candidates = vacancy.Candidates;

                var result = await _context.vacancies.ReplaceOneAsync(filter: s => s.Id == vacancy.Id, replacement: dataSet).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<bool> DeleteVacancies(string id)
        {
            FilterDefinition<JobVacencies> vacancyFilter = Builders<JobVacencies>.Filter.Eq(p => p.Id, id);

            DeleteResult vacancyDeleteResult = await _context.vacancies.DeleteOneAsync(vacancyFilter).ConfigureAwait(false);

            return vacancyDeleteResult.IsAcknowledged && vacancyDeleteResult.DeletedCount > 0;
            
        }

        
    }
}
