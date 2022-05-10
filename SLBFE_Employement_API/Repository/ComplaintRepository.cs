using MongoDB.Driver;
using SLBFE_Employement_API.Data.Interfaces;
using SLBFE_Employement_API.Extensions;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;

namespace SLBFE_Employement_API.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ISLBFContext _context;

        public ComplaintRepository(ISLBFContext context)
        {
            _context = context;
        }

        public async Task<Complaint> CreateComplaint(CreateComplaintrequest reaquest)
        {
            var dataSet = reaquest.ToModel();
            await _context.complaints.InsertOneAsync(dataSet).ConfigureAwait(false);

            return await GetComplaintByIdAsync(dataSet.Id).ConfigureAwait(false);
        }

        public async Task<bool> DeleteComplaint(string id)
        {
            FilterDefinition<Complaint> complaintsFilter = Builders<Complaint>.Filter.Eq(p => p.Id, id);

            DeleteResult compaintDeleteResult = await _context.complaints.DeleteOneAsync(complaintsFilter).ConfigureAwait(false);

            return compaintDeleteResult.IsAcknowledged && compaintDeleteResult.DeletedCount > 0;
        }

        public async Task<List<Complaint>> GetAllComplaintsAsync()
        {
            return await _context.complaints.Find(p => true).ToListAsync();
        }

        public async Task<Complaint> GetComplaintByIdAsync(string id)
        {
            return await _context.complaints.Find(c => c.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<List<Complaint>> GetComplaintsByUserIdAsync(string userId)
        {
            return await _context.complaints.Find(c => c.UserId == userId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> ReplyRoComplaint(ReplyToComplainRequest request)
        {
            var complaint = await _context.complaints.Find(p => p.Id == request.ComplainId).FirstOrDefaultAsync();
            if (complaint != null)
            {
                complaint.RespondedUserId = request.UserId;
                complaint.ResponseMessage = request.Reply;

                var result = await _context.complaints.ReplaceOneAsync(filter: s => s.Id == complaint.Id, replacement: complaint).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }
    }
}
