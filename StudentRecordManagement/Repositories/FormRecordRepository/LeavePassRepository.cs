using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public class LeavePassRepository : IFormRecordRepository<LeavePass>
    {
        private readonly ApplicationDBContext _dbContext;

        public LeavePassRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeavePass> CreateAsync(LeavePass entity)
        {
            await _dbContext.LeavePassRecords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<LeavePass?> DeleteAsync(Guid id)
        {
            var existingRecord = await _dbContext.LeavePassRecords.FindAsync(id);

            if (existingRecord != null)
            {
                _dbContext.LeavePassRecords.Remove(existingRecord);
                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }

        public async Task<IEnumerable<LeavePass>> GetAllAsync()
        {
            return await _dbContext.LeavePassRecords
                .Include(s => s.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LeavePass?> GetAsync(Guid id)
        {
            return await _dbContext.LeavePassRecords
                .Include(s => s.Student)
                .FirstOrDefaultAsync(record => record.Id == id);
        }

        public async Task<LeavePass?> UpdateAsync(LeavePass entity)
        {
            var existingRecord = await _dbContext.LeavePassRecords
                .Include(s => s.Student)
                .FirstOrDefaultAsync(record => record.Id == entity.Id);

            if (existingRecord != null)
            {
                existingRecord.RecordDate = entity.RecordDate;
                existingRecord.Modified = entity.Modified;
                // existingRecord.Student = entity.Student;

                existingRecord.Reason = entity.Reason;
                existingRecord.Details = entity.Details;
                existingRecord.SignOutDateTime = entity.SignOutDateTime;

                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }
    }
}
