using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.People;
using System.Numerics;
using System.Reflection;

namespace StudentRecordManagement.Repositories.StudentDetailsRepository
{
    public class StudentDetailsRepository : IStudentDetailsRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public StudentDetailsRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return student;
        }

        public async Task<Student?> DeleteAsync(Guid id)
        {
            var existingStudent  = await _dbContext.Students.FindAsync(id);

            if (existingStudent != null)
            {
                _dbContext.Students.Remove(existingStudent);
                await _dbContext.SaveChangesAsync();

                return existingStudent;
            }

            return null;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetAsync(Guid id)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
        }

        public async Task<Student?> UpdateAsync(Student student)
        {
            var existingStudent = await _dbContext.Students.FindAsync(student.Id);

            if (existingStudent is not null)
            {
                existingStudent.Firstname = student.Firstname;
                existingStudent.Surname = student.Surname;
                existingStudent.PreferredName = student.PreferredName;
                existingStudent.Year = student.Year;
                existingStudent.DOB = student.DOB;
                existingStudent.Email = student.Email;
                existingStudent.Phone = student.Phone;
                existingStudent.Gender = student.Gender;
                existingStudent.Modified = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return existingStudent;
            }

            return null;
        }
    }
}
