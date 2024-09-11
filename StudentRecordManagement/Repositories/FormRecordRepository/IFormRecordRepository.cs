using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public interface IFormRecordRepository<TEntity> where TEntity : FormRecord
    {
        Task<TEntity?> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> DeleteAsync(Guid id);
    }
}
