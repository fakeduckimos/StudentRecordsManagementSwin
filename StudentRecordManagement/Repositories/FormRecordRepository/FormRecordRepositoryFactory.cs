using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Repositories.FormRecordRepository
{
    public class FormRecordRepositoryFactory
    {
        private readonly ApplicationDBContext _dbContext;

        public FormRecordRepositoryFactory(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IFormRecordRepository<TEntity> CreateRepository<TEntity>() where TEntity : FormRecord
        {
            if (typeof(TEntity) == typeof(Detention))
            {
                return (IFormRecordRepository<TEntity>) new DetentionRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(SickBay))
            {
                return (IFormRecordRepository<TEntity>) new SickBayRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(LeavePass))
            {
                return (IFormRecordRepository<TEntity>)new LeavePassRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(LatePass))
            {
                return (IFormRecordRepository<TEntity>)new LatePassRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(FormRecord))
            {
                return (IFormRecordRepository<TEntity>)new FormRecordRepository(_dbContext);
            }
            else
            {
                throw new ArgumentException($"Invalid form type: {typeof(TEntity).Name}");
            }
        }
    }
}
