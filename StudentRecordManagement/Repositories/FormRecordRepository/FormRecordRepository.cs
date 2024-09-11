using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public class FormRecordRepository : IFormRecordRepository<FormRecord>
    {
        private readonly ApplicationDBContext _dbContext;

        public FormRecordRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<FormRecord> CreateAsync(FormRecord entity)
        {
            throw new NotImplementedException();
        }

        public Task<FormRecord?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FormRecord>> GetAllAsync()
        {
            return await _dbContext.FormRecords
                .Include(s => s.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<FormRecord?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FormRecord?> UpdateAsync(FormRecord entity)
        {
            throw new NotImplementedException();
        }
    }
}
