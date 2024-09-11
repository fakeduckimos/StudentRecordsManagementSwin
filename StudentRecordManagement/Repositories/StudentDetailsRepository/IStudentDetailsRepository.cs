using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Repositories.StudentDetailsRepository
{
    public interface IStudentDetailsRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetAsync(Guid id);
        Task<Student> AddAsync(Student student);
        Task<Student?> UpdateAsync(Student student);
        Task<Student?> DeleteAsync(Guid id);
    }
}
