using Domain.Entities;
using Infrastructure.Contracts;

/*
 * Created by   : Jahangir
 * Date created : 31-04-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Repositories
{
    public class ExpenseCategoryRepository : Repository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(ExpenseDbContext context) : base(context) 
        { 

        }

        public async Task<ExpenseCategory> GetExpenseCategoryInfoByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.Oid == key && c.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ExpenseCategory> GetExpenseCategoryByName(string ExpenseCategoryName)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.Description.ToLower().Trim() == ExpenseCategoryName.ToLower().Trim() && c.IsDeleted== false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> GetExpenseCategoryCount()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategories()
        {
            try
            {
                return await QueryAsync(p => p.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}