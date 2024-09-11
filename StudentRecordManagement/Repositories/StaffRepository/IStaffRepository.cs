using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Repositories.StaffRepository
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllMedicalStaffAsync();
    }
}
