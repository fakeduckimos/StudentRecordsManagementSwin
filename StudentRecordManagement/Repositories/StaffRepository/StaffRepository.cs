using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Repositories.StaffRepository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public StaffRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Staff>> GetAllMedicalStaffAsync()
        {
            return await _dbContext.Staff.Where(s => (s.StaffGroupMask & (int) StaffGroupBit.MedicalOfficer) != 0).ToListAsync();
        }
    }
}
