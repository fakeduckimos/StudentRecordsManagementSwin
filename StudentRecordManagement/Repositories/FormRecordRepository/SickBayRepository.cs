using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public class SickBayRepository : IFormRecordRepository<SickBay>
    {
        private readonly ApplicationDBContext _dbContext;

        public SickBayRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SickBay> CreateAsync(SickBay entity)
        {
            await _dbContext.SickBayRecords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<SickBay?> DeleteAsync(Guid id)
        {
            var existingRecord = await _dbContext.SickBayRecords.FindAsync(id);

            if ( existingRecord != null )
            {
                _dbContext.SickBayRecords.Remove(existingRecord);
                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }

        public async Task<IEnumerable<SickBay>> GetAllAsync()
        {
            return await _dbContext.SickBayRecords
                .Include(s => s.Student)
                .Include(s => s.MedicalOfficer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SickBay?> GetAsync(Guid id)
        {
            return await _dbContext.SickBayRecords
                .Include(s => s.Student)
                .Include(s => s.MedicalOfficer)
                .FirstOrDefaultAsync(record => record.Id == id);
        }

        public async Task<SickBay?> UpdateAsync(SickBay entity)
        {
            var existingRecord = await _dbContext.SickBayRecords.FindAsync(entity.Id);

            if (existingRecord != null)
            {
                existingRecord.RecordDate = entity.RecordDate;
                existingRecord.Modified = entity.Modified;
                // existingRecord.Student = entity.Student;

                existingRecord.Status = entity.Status;
                existingRecord.TimeIn = entity.TimeIn;
                existingRecord.SickBayReason = entity.SickBayReason;
                existingRecord.OtherReasons = entity.OtherReasons;
                existingRecord.Treatment = entity.Treatment;
                existingRecord.TimeOut = entity.TimeOut;
                existingRecord.SickBayOutAction = entity.SickBayOutAction;
                existingRecord.ParentContacted = entity.ParentContacted;
                existingRecord.MedicalOfficer = entity.MedicalOfficer;

                await _dbContext.SaveChangesAsync();

                return existingRecord;
            }

            return null;
        }
    }
}
