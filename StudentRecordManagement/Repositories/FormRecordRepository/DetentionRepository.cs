using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public class DetentionRepository : IFormRecordRepository<Detention>
    {
        private readonly ApplicationDBContext _dbContext;

        public DetentionRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Detention> CreateAsync(Detention entity)
        {
            await _dbContext.DetentionRecords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Detention?> DeleteAsync(Guid id)
        {
            var existingRecord = await _dbContext.DetentionRecords.FindAsync(id);

            if (existingRecord != null)
            {
                _dbContext.DetentionRecords.Remove(existingRecord);
                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }

        public async Task<IEnumerable<Detention>> GetAllAsync()
        {
            return await _dbContext.DetentionRecords
                .Include(s => s.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Detention?> GetAsync(Guid id)
        {
            return await _dbContext.DetentionRecords
                .Include(s => s.Student)
                .FirstOrDefaultAsync(record => record.Id == id);
        }

        public async Task<Detention?> UpdateAsync(Detention entity)
        {
            var existingRecord = await _dbContext.DetentionRecords.FindAsync(entity.Id);

            if (existingRecord != null)
            {
                existingRecord.RecordDate = entity.RecordDate;
                existingRecord.Modified = entity.Modified;
                // existingRecord.Student = entity.Student;

                existingRecord.Status = entity.Status;
                existingRecord.PredefinedReasons = entity.PredefinedReasons;
                existingRecord.BreachReason = entity.BreachReason;
                existingRecord.DetentionTime = entity.DetentionTime;

                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }
    }
}
