using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public class LatePassRepository : IFormRecordRepository<LatePass>
    {
        private readonly ApplicationDBContext _dbContext;

        public LatePassRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LatePass> CreateAsync(LatePass entity)
        {
            await _dbContext.LatePassRecords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<LatePass?> DeleteAsync(Guid id)
        {
            var existingRecord = await _dbContext.LatePassRecords.FindAsync(id);

            if (existingRecord != null)
            {
                _dbContext.LatePassRecords.Remove(existingRecord);
                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }

        public async Task<IEnumerable<LatePass>> GetAllAsync()
        {
            return await _dbContext.LatePassRecords
                .Include(s => s.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LatePass?> GetAsync(Guid id)
        {
            return await _dbContext.LatePassRecords
                .Include(s => s.Student)
                .FirstOrDefaultAsync(record => record.Id == id);
        }

        public async Task<LatePass?> UpdateAsync(LatePass entity)
        {
            var existingRecord = await _dbContext.LatePassRecords.FindAsync(entity.Id);

            if (existingRecord != null)
            {
                existingRecord.RecordDate = entity.RecordDate;
                existingRecord.Modified = entity.Modified;
                // existingRecord.Student = entity.Student;

                existingRecord.Reason = entity.Reason;
                existingRecord.Details = entity.Details;
                existingRecord.SignInDateTime = entity.SignInDateTime;

                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }
    }
}
